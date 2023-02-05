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

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public async UniTask ShowAsync(int delay, Settings settings, CancellationToken cancellationToken)
        {
            Vector3 targetPosition = transform.position;
            transform.position = targetPosition + settings.CellShowOffset;

            await UniTask.Delay(delay, cancellationToken: cancellationToken);

            Material material = _meshRenderer.material;
            Color materialColor = material.color;
            materialColor.a = 0;
            material.color = materialColor;

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
    }
}