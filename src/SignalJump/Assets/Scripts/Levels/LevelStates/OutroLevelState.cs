using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    internal class OutroLevelState : ILevelState, IState, IStateWithExit, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private readonly GameStateMachine _gameStateMachine;

        public OutroLevelState(LevelHolder levelHolder, PlayerHolder playerHolder, GameStateMachine gameStateMachine)
        {
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
            _gameStateMachine = gameStateMachine;
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