using System.Threading;
using Cysharp.Threading.Tasks;

namespace SignalJump
{
    public class IntroLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly GameProgressProvider _gameProgressProvider;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private CancellationTokenSource _cancellationTokenSource;

        public IntroLevelState(LevelStateMachine levelStateMachine, GameProgressProvider gameProgressProvider,
            LevelHolder levelHolder, PlayerHolder playerHolder)
        {
            _levelStateMachine = levelStateMachine;
            _gameProgressProvider = gameProgressProvider;
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
        }

        public void Enter()
        {
            _levelHolder.StartLevel(_gameProgressProvider.Progress.SelectedLevelIndex);
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