﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SignalJump
{
    public sealed class PlayerHolder : IDisposable
    {
        private readonly Settings _settings;
        private readonly LevelHolder _levelHolder;
        private PlayerView _playerView;
        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

        public PlayerHolder(Settings settings, LevelHolder levelHolder)
        {
            _settings = settings;
            _levelHolder = levelHolder;
        }

        public void CreatePlayer()
        {
            if (_playerView != null)
                return;

            _playerView = Object.Instantiate(_settings.PlayerViewPrefab);
            _playerView.gameObject.SetActive(false);
        }

        public async UniTask ShowPlayer(Vector2Int startPosition, CancellationToken cancellationToken)
        {
            _playerView.gameObject.SetActive(true);
            _tween?.Kill();

            Vector3 toPosition = _levelHolder.ConvertCellToPosition(startPosition.x, startPosition.y);
            Vector3 fromPosition = toPosition + _settings.PlayerIntroOffset;

            Transform transform = _playerView.transform;
            transform.position = fromPosition;
            await transform
                .DOMove(toPosition, _settings.PlayerIntroDuration)
                .SetEase(_settings.PlayerIntroEasing)
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public async UniTask HidePlayer()
        {
            Transform transform = _playerView.transform;
            Vector3 fromPosition = transform.position;
            Vector3 toPosition = fromPosition + _settings.PlayerOutroOffset;
            _tween = transform.DOMove(toPosition, _settings.PlayerIntroDuration).SetEase(_settings.PlayerOutroEasing);
        }

        public void Dispose()
        {
        }
    }
}