using BountyHunter.Utils;
using UnityEngine;
using Zenject;

namespace BountyHunter
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public Settings Settings;
        public Camera Camera;
        public SpaceTexture SpaceTexture;

        public override void InstallBindings()
        {
            BootstrapInstaller.Install(Container);
            ProgressInstaller.Install(Container);
            GameStateMachineInstaller.Install(Container);
            MissionInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<TickInvoker>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShipSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<BackgroundMover>().AsSingle();

            Container.Bind<Settings>().FromInstance(Settings).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera).AsSingle();
            Container.Bind<SpaceTexture>().FromInstance(SpaceTexture).AsSingle();
        }
    }
}