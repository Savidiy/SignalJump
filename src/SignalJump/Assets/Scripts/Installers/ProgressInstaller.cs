using BountyHunter.Utils;
using Zenject;

namespace BountyHunter
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