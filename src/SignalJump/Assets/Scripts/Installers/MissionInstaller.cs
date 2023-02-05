using Zenject;

namespace BountyHunter
{
    public sealed class MissionInstaller : Installer<MissionInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerHolder>().AsSingle();
            Container.Bind<ShipFactory>().AsSingle();
        }
    }
}