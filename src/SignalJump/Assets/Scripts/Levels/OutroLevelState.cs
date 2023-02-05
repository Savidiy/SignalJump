using System.Threading;
using Cysharp.Threading.Tasks;

namespace SignalJump
{
    internal class OutroLevelState : ILevelState
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly LevelHolder _levelHolder;
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameProgressProvider _gameProgressProvider;

        public OutroLevelState(LevelHolder levelHolder, GameStateMachine gameStateMachine, GameProgressProvider gameProgressProvider)
        {
            _levelHolder = levelHolder;
            _gameStateMachine = gameStateMachine;
            _gameProgressProvider = gameProgressProvider;
        }
        
        public void Enter()
        {
            _gameProgressProvider.Progress.CompleteSelectedLevel();
            _gameProgressProvider.SaveProgress();
            PlayOutro().Forget();
        }

        private async UniTask PlayOutro()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _levelHolder.HideCells(_cancellationTokenSource.Token);
            _levelHolder.ClearLevel();
            // await _levelHolder.ShowPlayer();
            _gameStateMachine.EnterToState<ShelterGameState>();
        }
        
        public void Exit()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}