using System.Threading;
using Cysharp.Threading.Tasks;

namespace SignalJump
{
    internal class RestartLevelState : ILevelState
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly LevelHolder _levelHolder;
        private readonly LevelStateMachine _levelStateMachine;

        public RestartLevelState(LevelHolder levelHolder, LevelStateMachine levelStateMachine)
        {
            _levelHolder = levelHolder;
            _levelStateMachine = levelStateMachine;
        }
        
        public void Enter()
        {
            PlayOutro().Forget();
        }

        private async UniTask PlayOutro()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _levelHolder.HideCells(_cancellationTokenSource.Token);
            _levelHolder.ClearLevel();
            // await _levelHolder.ShowPlayer();
            _levelStateMachine.EnterToState<IntroLevelState>();
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