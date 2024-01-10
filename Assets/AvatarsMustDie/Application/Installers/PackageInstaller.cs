using VGBootstrapService;
using VGCore;
using VGUIService;
using Zenject;

namespace AvatarsMustDie.Application.Installers
{
    public class PackageInstaller : Installer<PackageInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IBootstrap>()
                .To<Bootstrap>()
                .AsSingle();
            
            Container
                .Bind<IUIRoot>()
                .To<UIRoot>()
                .FromComponentInNewPrefabResource("UIWindows/UIRoot")
                .AsSingle();

            Container
                .Bind<CommandStorage>()
                .AsSingle();
            
            Container
                .Bind<IUIService>()
                .To<UIService>()
                .AsSingle();
        }
    }
}