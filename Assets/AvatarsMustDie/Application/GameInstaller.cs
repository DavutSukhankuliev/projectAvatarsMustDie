using AvatarsMustDie.Base;
using AvatarsMustDie.Levels;
using AvatarsMustDie.PoseDetection;
using AvatarsMustDie.Spells;
using AvatarsMustDie.Wave;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Application
{
    public class GameInstaller : Installer<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameController>()
                .AsSingle();

            Container
                .Bind<LevelController>()
                .AsSingle();

            Container
                .Bind<StatisticController>()
                .AsSingle();
            
            Container
                .Bind<WaveControllerConfig>()
                .FromScriptableObjectResource("WaveControllerConfig")
                .AsSingle();
            
            Container
                .Bind<WaveController>()
                .AsSingle();

            Container
                .Bind<PoseDetectionController>()
                .AsSingle();

            Container
                .Bind<PoseRandomizer>()
                .AsSingle();
            
            Container
                .BindFactory<Object, Spell, Spell.Factory>()
                .FromFactory<SpellFactory>();

            Container
                .Bind<SpellsConfig>()
                .FromScriptableObjectResource("SpellsConfig")
                .AsSingle();

            Container.Bind<SpellsController>()
                .AsSingle();
            
            Container
                .Bind<BaseConfig>()
                .FromScriptableObjectResource("BaseConfig")
                .AsSingle();

            Container
                .Bind<BaseController>()
                .AsSingle()
                .NonLazy();
        }
    }
}