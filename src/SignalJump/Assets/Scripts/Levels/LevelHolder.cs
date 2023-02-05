using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SignalJump
{
    public sealed class LevelHolder : IDisposable
    {
        private readonly Settings _settings;

        private BasicLevelCell[,] _cells;
        private int _width;
        private int _height;

        public Vector2Int StartPosition { get; private set; }

        public LevelHolder(Settings settings)
        {
            _settings = settings;
        }

        public void StartLevel(int index)
        {
            LevelData levelData = _settings.LevelSequence.Levels[index];

            _width = levelData.LevelSize.x;
            _height = levelData.LevelSize.y;
            _cells = new BasicLevelCell[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    ELevelCellType cellType = levelData.LevelCells[x, y];
                    if (cellType == ELevelCellType.Start)
                        StartPosition = new Vector2Int(x, y);

                    CreateCell(x, y, cellType);
                }
            }
        }

        public void ClearLevel()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    BasicLevelCell levelCell = _cells[x, y];
                    if (levelCell != null)
                    {
                        levelCell.Dispose();
                    }
                }
            }
        }

        public async UniTask ShowCells(CancellationToken cancellationToken)
        {
            List<UniTask> tasks = new();

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    BasicLevelCell levelCell = _cells[x, y];
                    if (levelCell != null)
                    {
                        var delay = (int) ((_width - x - 1 + y) * _settings.ShowCellsDelay * 1000);
                        UniTask task = levelCell.ShowAsync(delay, _settings, cancellationToken);
                        tasks.Add(task);
                    }
                }
            }

            await UniTask.WhenAll(tasks);
        }

        public void HideCell(Vector2Int cellPosition)
        {
            BasicLevelCell basicLevelCell = _cells[cellPosition.x, cellPosition.y];
            basicLevelCell.HideAsync(0, _settings, CancellationToken.None).Forget();
        }

        public async UniTask HideCells(CancellationToken token)
        {
            List<UniTask> tasks = new();

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    BasicLevelCell levelCell = _cells[x, y];
                    if (levelCell != null)
                    {
                        var delay = (int) ((x + _height - y - 1) * _settings.ShowCellsDelay * 1000);
                        UniTask task = levelCell.HideAsync(delay, _settings, token);
                        tasks.Add(task);
                    }
                }
            }

            await UniTask.WhenAll(tasks);
        }

        private void CreateCell(int x, int y, ELevelCellType cellType)
        {
            switch (cellType)
            {
                case ELevelCellType.None:
                    break;
                case ELevelCellType.Start:
                case ELevelCellType.Basic:
                    CreateBasicCell(x, y);
                    break;
                case ELevelCellType.Finish:
                    CreateBasicCell(x, y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null);
            }
        }

        private void CreateBasicCell(int x, int y)
        {
            BasicLevelCell basicLevelCell = Object.Instantiate(_settings._basicLevelCellPrefab);
            basicLevelCell.transform.position = ConvertCellToPosition(x, y);
            basicLevelCell.SetCell(x, y, _settings.AvailableCellColor, _settings.UnavailableCellColor);
            basicLevelCell.SetAvailable(false, 0);
            _cells[x, y] = basicLevelCell;
        }

        public Vector3 ConvertCellToPosition(int x, int y)
        {
            float positionX = x * _settings.CellStep - _width / 2f;
            float positionZ = _height / 2f - y * _settings.CellStep;
            return new Vector3(positionX, 0, positionZ);
        }

        public void Dispose()
        {
        }

        public async UniTask SetCellAvailabilityFrom(Vector2Int cellPosition)
        {
            BasicLevelCell fromCell = _cells[cellPosition.x, cellPosition.y];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    BasicLevelCell levelCell = _cells[x, y];
                    if (levelCell != null)
                    {
                        var isAvailable = !levelCell.IsCompleted && fromCell.CanMoveTo(x, y);
                        levelCell.SetAvailable(isAvailable, _settings.AvailabilitySwapDuration);
                    }
                }
            }

            int duration = (int) ( _settings.AvailabilitySwapDuration * 1000);
            await UniTask.Delay(duration);
        }
    }
}