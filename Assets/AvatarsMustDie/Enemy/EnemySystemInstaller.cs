using Zenject;
using UnityEngine;

namespace AvatarsMustDie.Enemy
{
    public class EnemySystemInstaller : Installer<EnemySystemInstaller>
    {
        public override void InstallBindings()
        {
            var config = Resources.Load<EnemyConfig>("Enemy/EnemyConfig");
            var viewPrefab = Resources.Load<EnemyView>("Enemy/EnemyViewPrefab");
            
            Container
                .BindFactory<EnemyViewProtocol, EnemyView, EnemyView.Factory>()
                .FromComponentInNewPrefab(viewPrefab)
                .AsSingle();

            Container
                .Bind<EnemyConfig>()
                .FromScriptableObject(config)
                .AsSingle();

            Container
                .Bind<EnemyStorage>()
                .AsSingle();
        }
    }
}