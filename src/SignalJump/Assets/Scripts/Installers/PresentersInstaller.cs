using SignalJump.MainMenu;
using SignalJump.Shelter;
using Zenject;

namespace SignalJump
{
    public sealed class PresentersInstaller : MonoInstaller
    {
        public MainMenuPresenter MainMenuPresenter;
        
        public override void InstallBindings()
        {
            Container.Bind<MainMenuPresenter>().FromInstance(MainMenuPresenter).AsSingle();
            Container.Bind<ShelterPresenter>().AsSingle();
        }
    }
}