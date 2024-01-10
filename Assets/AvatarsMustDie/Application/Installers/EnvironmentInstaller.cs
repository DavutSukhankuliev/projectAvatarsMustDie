using AvatarsMustDie.Levels;
using AvatarsMustDie.Wave;
using Zenject;

namespace AvatarsMustDie.Application.Installers
{
    public class EnvironmentInstaller : Installer<EnvironmentInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneHolder>()
                .AsSingle();

            Container
                .Bind<BiomesConfig>()
                .FromScriptableObjectResource("GameBiomesConfig")
                .AsSingle();

            Container
                .Bind<BiomesStateMachine>()
                .AsSingle();
        }
    }
}