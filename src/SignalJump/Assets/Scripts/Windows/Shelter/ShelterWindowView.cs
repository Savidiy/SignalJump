using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BountyHunter
{
    public sealed class ShelterWindowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyLabel;
        [SerializeField] private Button _testButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _startMissionButton;
        
        private Progress _progress;
        
        public event Action MenuClicked;
        public event Action StartMissionClicked;

        public void ShowWindow(Progress progress)
        {
            _progress = progress;
            UpdateFields();
            AddListeners();
            
            gameObject.SetActive(true);
            
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
            _testButton.onClick.RemoveListener(OnTestClick);
            _menuButton.onClick.RemoveListener(OnMenuClick);
            _startMissionButton.onClick.RemoveListener(OnStartMissionClick);
        }

        private void AddListeners()
        {
            _testButton.onClick.AddListener(OnTestClick);
            _menuButton.onClick.AddListener(OnMenuClick);
            _startMissionButton.onClick.AddListener(OnStartMissionClick);
        }

        private void OnMenuClick()
        {
            MenuClicked?.Invoke();
        }

        private void UpdateFields()
        {
            _moneyLabel.text = _progress.Money.ToString();
        }

        private void OnStartMissionClick()
        {
            StartMissionClicked?.Invoke();
        }

        private void OnTestClick()
        {
            _progress.Money++;
            UpdateFields();
        }
    }
}