using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SignalJump
{
    public sealed class LevelWindow : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;

        public event Action BackClicked;
        public event Action RestartClicked;

        [Inject]
        public void Construct()
        {
            DeactivateButtons();
            _backButton.onClick.AddListener(OnBackClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
            HideWindow();
        }

        public void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }

        private void OnBackClicked()
        {
            BackClicked?.Invoke();
        }

        public void ActivateButtons()
        {
            _backButton.interactable = true;
            _restartButton.interactable = true;
        }

        public void DeactivateButtons()
        {
            _backButton.interactable = false;
            _restartButton.interactable = false;
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(OnBackClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }
    }
}