using BountyHunter.Utils;
using UnityEngine;

namespace BountyHunter
{
    internal sealed class ClearPrefs
    {
        public void Clear()
        {
            
        }
    }
    
    public sealed class GameProgressProvider
    {
        private const string KEY = "progress";
        private readonly Serializer<Progress> _serializer;

        private Progress _progress = new Progress();

        public bool HasSavedProgress { get; private set; }
        public bool HasCurrentProgress { get; private set; }
        public Progress Progress => _progress;

        public GameProgressProvider(Serializer<Progress> serializer)
        {
            _serializer = serializer;
            HasSavedProgress = PlayerPrefs.HasKey(KEY);
        }

        public void SaveProgress()
        {
            HasSavedProgress = true;

            string serialize = _serializer.Serialize(_progress);

            PlayerPrefs.SetString(KEY, serialize);
        }

        public void LoadProgress()
        {
            HasSavedProgress = true;

            if (!PlayerPrefs.HasKey(KEY))
            {
                Debug.LogError($"Haven't saved progress with key '{KEY}'");
            }

            string text = PlayerPrefs.GetString(KEY);

            Progress progress = _serializer.Deserialize(text);
            _progress = progress;
        }

        public void ResetProgressForNewGame()
        {
            HasCurrentProgress = true;
            _progress = new Progress();
        }
    }
}