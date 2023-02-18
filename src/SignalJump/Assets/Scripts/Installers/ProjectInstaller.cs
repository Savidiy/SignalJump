using SignalJump.Utils;
using UnityEngine;
using Zenject;

namespace SignalJump
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public Settings Settings;
        public Camera Camera;
        public LevelWindow LevelWindow;

        public override void InstallBindings()
        {
            BootstrapInstaller.Install(Container);
            ProgressInstaller.Install(Container);
            GameStateMachineInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<TickInvoker>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelHolder>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelUpdater>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHolder>().AsSingle();

            Container.Bind<IntroLevelState>().AsSingle();
            Container.Bind<OutroLevelState>().AsSingle();
            Container.Bind<RestartLevelState>().AsSingle();
            Container.Bind<WaitInputState>().AsSingle();
            Container.Bind<LevelStateMachine>().AsSingle();

            Container.Bind<LevelWindow>().FromInstance(LevelWindow).AsSingle();
            Container.Bind<Settings>().FromInstance(Settings).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera).AsSingle();
        }
    }
}