namespace BountyHunter
{
    public sealed class ExitGameState : IGameState
    {
        private readonly GameProgressProvider _gameProgressProvider;

        public ExitGameState(GameProgressProvider gameProgressProvider)
        {
            _gameProgressProvider = gameProgressProvider;
        }

        public void Enter()
        {
            _gameProgressProvider.SaveProgress();
            QuitGame();
        }

        public void Exit()
        {
        }

        public void Dispose()
        {
        }

        private static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}