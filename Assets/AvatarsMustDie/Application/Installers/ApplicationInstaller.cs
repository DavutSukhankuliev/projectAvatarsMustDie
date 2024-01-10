using AvatarsMustDie.Enemy;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Application.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private float maxFrameRate;
        [SerializeField] private bool developMode;
        
        public override void InstallBindings()
        {
            // To clear RAM
            Resources.UnloadUnusedAssets();
            
            // Set frame rate
            OVRPlugin.systemDisplayFrequency = maxFrameRate;
            
            PackageInstaller.Install(Container);

            EnvironmentInstaller.Install(Container);
            
            GameInstaller.Install(Container);
            
            EnemySystemInstaller.Install(Container);

            Container
                .Bind<DevelopSettings>()
                .FromInstance(new DevelopSettings(developMode))
                .AsSingle();
            
            Container
                .Bind<ApplicationLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }

    public class DevelopSettings
    {
        public readonly bool IsOn;
        
        public DevelopSettings(bool isOn)
        {
            IsOn = isOn;
        }
    }
}
