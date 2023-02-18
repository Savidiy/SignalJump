using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    public sealed class ExitGameState : IGameState, IState
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