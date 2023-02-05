using SignalJump.Utils;
using Zenject;

namespace SignalJump
{
    public sealed class ProgressInstaller : Installer<ProgressInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Serializer<Progress>>().AsSingle();
            Container.Bind<GameProgressProvider>().AsSingle();
        }
    }
}