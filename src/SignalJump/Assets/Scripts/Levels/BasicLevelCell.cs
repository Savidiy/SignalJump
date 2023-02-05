using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SignalJump
{
    public class BasicLevelCell : MonoBehaviour, IDisposable
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        private Color _availableColor;
        private Color _unavailableColor;
        public Vector2Int CellPosition { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsAvailable { get; private set; }

        public void SetCell(int x, int y, Color availableColor, Color unavailableColor)
        {
            _unavailableColor = unavailableColor;
            _availableColor = availableColor;
            CellPosition = new Vector2Int(x, y);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public async UniTask ShowAsync(int delay, Settings settings, CancellationToken cancellationToken)
        {
            Vector3 targetPosition = transform.position;
            transform.position = targetPosition + settings.CellShowOffset;

            Material material = _meshRenderer.material;
            Color materialColor = material.color;
            materialColor.a = 0;
            material.color = materialColor;

            await UniTask.Delay(delay, cancellationToken: cancellationToken);
            material
                .DOFade(1f, settings.CellFadeDuration)
                .SetEase(settings.CellFadeEasing);

            await transform
                .DOMove(targetPosition, settings.CellShowDuration)
                .SetEase(settings.CellShowEasing)
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public async UniTask HideAsync(int delay, Settings settings, CancellationToken token)
        {
            IsCompleted = true;
            await UniTask.Delay(delay, cancellationToken: token);

            Material material = _meshRenderer.material;
            material
                .DOFade(0f, settings.CellFadeDuration)
                .SetEase(settings.CellFadeEasing);

            Vector3 targetPosition = transform.position + settings.CellShowOffset;
            await transform
                .DOMove(targetPosition, settings.CellShowDuration)
                .SetEase(settings.CellShowEasing)
                .ToUniTask(cancellationToken: token);
        }

        public void SetAvailable(bool isAvailable, float duration)
        {
            IsAvailable = isAvailable;

            Material material = _meshRenderer.material;

            Color color = IsAvailable ? _availableColor : _unavailableColor;
            color.a = material.color.a;

            if (duration == 0)
                material.color = color;
            else
                material.DOColor(color, duration);
        }

        public bool CanMoveTo(int x, int y)
        {
            int myX = CellPosition.x;
            int myY = CellPosition.y;

            if (myX == x && myY == y + 1) return true;
            if (myX == x && myY == y + 2) return true;
            if (myX == x && myY == y - 1) return true;
            if (myX == x && myY == y - 2) return true;
            if (myX == x + 1 && myY == y) return true;
            if (myX == x + 2 && myY == y) return true;
            if (myX == x - 1 && myY == y) return true;
            if (myX == x - 2 && myY == y) return true;

            return false;
        }
    }
}