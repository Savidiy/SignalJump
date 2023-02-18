using Zenject;

namespace SignalJump
{
    public sealed class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuGameState>().AsSingle();
            Container.Bind<ShelterGameState>().AsSingle();
            Container.Bind<LevelGameState>().AsSingle();
            Container.Bind<ExitGameState>().AsSingle();
            
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}