using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    internal class RestartLevelState : ILevelState, IStateWithPayload<int>, IStateWithExit, IDisposable
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
        
        public void Enter(int levelIndex)
        {
            PlayOutro(levelIndex).Forget();
        }

        private async UniTask PlayOutro(int levelIndex)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _playerHolder.HidePlayer();
            await _levelHolder.HideCells(_cancellationTokenSource.Token);
            _levelHolder.ClearLevel();
            _levelStateMachine.EnterToState<IntroLevelState, int>(levelIndex);
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