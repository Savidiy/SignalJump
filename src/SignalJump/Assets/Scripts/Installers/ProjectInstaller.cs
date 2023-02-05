using SignalJump.Utils;
using UnityEngine;
using Zenject;

namespace SignalJump
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public Settings Settings;
        public Camera Camera;

        public override void InstallBindings()
        {
            BootstrapInstaller.Install(Container);
            ProgressInstaller.Install(Container);
            GameStateMachineInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<TickInvoker>().AsSingle();

            Container.Bind<Settings>().FromInstance(Settings).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera).AsSingle();
        }
    }
}