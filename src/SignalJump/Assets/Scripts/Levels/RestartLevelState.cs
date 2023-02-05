using System.Threading;
using Cysharp.Threading.Tasks;

namespace SignalJump
{
    internal class RestartLevelState : ILevelState
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private readonly LevelStateMachine _levelStateMachine;

        public RestartLevelState(LevelHolder levelHolder, PlayerHolder playerHolder, LevelStateMachine levelStateMachine)
        {
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
            _levelStateMachine = levelStateMachine;
        }
        
        public void Enter()
        {
            PlayOutro().Forget();
        }

        private async UniTask PlayOutro()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _playerHolder.HidePlayer();
            await _levelHolder.HideCells(_cancellationTokenSource.Token);
            _levelHolder.ClearLevel();
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