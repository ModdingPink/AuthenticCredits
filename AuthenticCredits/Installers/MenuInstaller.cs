using AuthenticCredits.UI;
using Zenject;

namespace AuthenticCredits.Installers
{
    class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModSettings>().FromNewComponentOnRoot().AsSingle();
        }
    }
}
