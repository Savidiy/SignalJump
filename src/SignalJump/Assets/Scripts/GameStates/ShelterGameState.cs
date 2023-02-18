using SignalJump.Shelter;
using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    public sealed class ShelterGameState : IGameState, IState, IStateWithExit
    {
        private readonly ShelterPresenter _shelterPresenter;

        public ShelterGameState(ShelterPresenter shelterPresenter)
        {
            _shelterPresenter = shelterPresenter;
        }

        public void Enter()
        {
            _shelterPresenter.ShowWindow();
        }

        public void Exit()
        {
            _shelterPresenter.HideWindow();
        }
    }
}