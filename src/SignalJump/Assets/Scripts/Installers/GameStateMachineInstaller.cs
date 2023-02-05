using Zenject;

namespace BountyHunter
{
    public sealed class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShelterGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MissionGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExitGameState>().AsSingle();
            
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}