using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    public class IntroLevelState : ILevelState, IStateWithPayload<int>, IStateWithExit, IDisposable
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private CancellationTokenSource _cancellationTokenSource;

        public IntroLevelState(LevelStateMachine levelStateMachine, LevelHolder levelHolder, PlayerHolder playerHolder)
        {
            _levelStateMachine = levelStateMachine;
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
        }

        public void Enter(int levelIndex)
        {
            _levelHolder.StartLevel(levelIndex);
            _playerHolder.CreatePlayer();
            PlayIntro().Forget();
        }

        private async UniTask PlayIntro()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _levelHolder.ShowCells(_cancellationTokenSource.Token);
            await _playerHolder.ShowPlayer(_levelHolder.StartPosition, _cancellationTokenSource.Token);
            await _levelHolder.SetCellAvailabilityFrom(_levelHolder.StartPosition);
            _levelStateMachine.EnterToState<WaitInputState>();
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