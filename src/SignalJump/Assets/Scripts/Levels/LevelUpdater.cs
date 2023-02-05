using Cysharp.Threading.Tasks;
using SignalJump.Utils;
using UnityEngine;

namespace SignalJump
{
    internal sealed class LevelUpdater
    {
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private readonly LevelWindow _levelWindow;
        private readonly LevelStateMachine _levelStateMachine;
        private readonly TickInvoker _tickInvoker;
        private readonly Camera _camera;
        private readonly GameProgressProvider _gameProgressProvider;

        public LevelUpdater(LevelHolder levelHolder, PlayerHolder playerHolder, LevelWindow levelWindow,
            LevelStateMachine levelStateMachine, TickInvoker tickInvoker, Camera camera,
            GameProgressProvider gameProgressProvider)
        {
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
            _levelWindow = levelWindow;
            _levelStateMachine = levelStateMachine;
            _tickInvoker = tickInvoker;
            _camera = camera;
            _gameProgressProvider = gameProgressProvider;
        }

        public void Activate()
        {
            _levelWindow.ActivateButtons();
            _levelWindow.BackClicked += OnBackClicked;
            _levelWindow.RestartClicked += OnRestartClicked;
            _tickInvoker.Updated += OnUpdated;
        }

        private void OnUpdated()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (raycastHit.collider.gameObject.TryGetComponent(out BasicLevelCell cell))
                    {
                        if (cell.IsAvailable)
                        {
                            Vector2Int cellPosition = cell.CellPosition;
                            MakeTurnAsync(cellPosition, cell.IsFinish).Forget();
                        }
                    }
                }
            }
        }

        private async UniTask MakeTurnAsync(Vector2Int cellPosition, bool isFinish)
        {
            _levelHolder.HideCell(_playerHolder.PlayerPosition);
            _levelWindow.DeactivateButtons();
            _tickInvoker.Updated -= OnUpdated;

            await _playerHolder.MovePlayerTo(cellPosition);
            await _levelHolder.SetCellAvailabilityFrom(cellPosition);

            if (isFinish)
            {
                if (_levelHolder.IsAllCompleted())
                {
                    _gameProgressProvider.Progress.CompleteSelectedLevel();
                    _gameProgressProvider.SaveProgress(); 
                    _levelStateMachine.EnterToState<OutroLevelState>();
                }
                else
                {
                    _levelStateMachine.EnterToState<RestartLevelState>();
                }
            }
            else
            {
                _levelWindow.ActivateButtons();
                _tickInvoker.Updated += OnUpdated;
            }
        }

        private void OnRestartClicked()
        {
            _levelStateMachine.EnterToState<RestartLevelState>();
        }

        private void OnBackClicked()
        {
            _levelStateMachine.EnterToState<OutroLevelState>();
        }

        public void Deactivate()
        {
            _levelWindow.DeactivateButtons();
            _levelWindow.BackClicked -= OnBackClicked;
            _levelWindow.RestartClicked -= OnRestartClicked;
            _tickInvoker.Updated -= OnUpdated;
        }
    }
}