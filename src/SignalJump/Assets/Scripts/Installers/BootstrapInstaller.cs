using Zenject;

namespace SignalJump
{
    internal sealed class BootstrapInstaller : Installer<BootstrapInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrapper>().AsSingle();
        }
    }
}