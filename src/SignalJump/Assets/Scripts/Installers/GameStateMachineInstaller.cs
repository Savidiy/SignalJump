using Zenject;

namespace SignalJump
{
    public sealed class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShelterGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExitGameState>().AsSingle();
            
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}