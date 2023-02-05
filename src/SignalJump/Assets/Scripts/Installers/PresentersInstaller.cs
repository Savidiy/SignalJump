using Zenject;

namespace BountyHunter
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