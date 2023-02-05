using SignalJump.Utils;
using UnityEngine;

namespace SignalJump
{
    public sealed class GameProgressProvider
    {
        private const string KEY = "progress";
        private readonly Serializer<Progress> _serializer;
        private readonly Settings _settings;

        private Progress _progress = new Progress();

        public bool HasSavedProgress { get; private set; }
        public bool HasCurrentProgress { get; private set; }
        public Progress Progress => _progress;

        public GameProgressProvider(Serializer<Progress> serializer, Settings settings)
        {
            _serializer = serializer;
            _settings = settings;
            _progress.LevelsCount = _settings.LevelSequence.Levels.Count;
            HasSavedProgress = PlayerPrefs.HasKey(KEY);
        }

        public void SaveProgress()
        {
            HasSavedProgress = true;

            _progress.LevelsCount = _settings.LevelSequence.Levels.Count;
            
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
            _progress.LevelsCount = _settings.LevelSequence.Levels.Count;
        }

        public void ResetProgressForNewGame()
        {
            _progress = new Progress();
            _progress.LevelsCount = _settings.LevelSequence.Levels.Count;
            SaveProgress();
            HasCurrentProgress = false;
            HasSavedProgress = false;
        }
    }
}