using SignalJump.Shelter;

namespace SignalJump
{
    public sealed class ShelterGameState : IGameState
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

        public void Dispose()
        {
        }
    }
}