using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvatarsMustDie.Application;
using AvatarsMustDie.Player;
using AvatarsMustDie.Wave;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AvatarsMustDie.Levels
{
    public readonly struct OnEndBiomeMessage
    { }
    
    public class BiomeState : IBiomeState
    {
        private BiomeConfig _biomeConfig;
        private OVRScreenFade _screenFade;
        private List<LevelConfig> _levelsList = new List<LevelConfig>();

        private const float FadeTime = 4f;

        private readonly IInstantiator _instantiator;
        private readonly SceneHolder _sceneHolder;
        private readonly LevelController _levelController;
        private readonly StatisticController _statisticController;

        public BiomeState(
            IInstantiator instantiator,
            SceneHolder sceneHolder,
            LevelController levelController,
            StatisticController statisticController)
        {
            _instantiator = instantiator;
            _sceneHolder = sceneHolder;
            _levelController = levelController;
            _statisticController = statisticController;
        }
        
        public void Init(BiomeConfig biomeConfig)
        {
            var player = _sceneHolder.Get<PlayerView>();
            _screenFade = player.ScreenFade;
            
            _biomeConfig = biomeConfig;
        }

        public Task OnEntry()
        {
            for (int i = 0; i < _biomeConfig.LevelConfig.Length; i++)
            {
                _levelsList.Add(_biomeConfig.LevelConfig[i]);
            }

            _levelController.OnEndLevel += EndLevel;
            
            _screenFade.FadeOut(FadeTime, () =>
            {
                StartNewLevel();
            });

            return Task.CompletedTask;
        }

        public Task OnExit()
        {
            _levelController.OnEndLevel -= EndLevel;

            return Task.CompletedTask;
        }

        private void EndLevel(LevelView view)
        {
            if (_levelsList.Count <= 0)
            {
                var timeToShowStatistic = _statisticController.CalculateAndShowStatisticByTime();

                DOVirtual.DelayedCall(timeToShowStatistic, () =>
                {
                    SwitchLevel(view);
                });
            }
            else
            {
                SwitchLevel(view);
            }
        }
        
        private void SwitchLevel(LevelView levelView)
        {
            _screenFade.FadeOut(FadeTime, () =>
            {
                Object.Destroy(levelView.gameObject);

                Resources.UnloadUnusedAssets();
                StartNewLevel();
            });
        }
        
        private void StartNewLevel()
        {
            var levelSettings = GetLevel();

            if (levelSettings == null)
            {
                MessageBroker.Default.Publish(new OnEndBiomeMessage());
                return;
            }
            
            var command = _instantiator.Instantiate<SetupLevelCommand>(new object[]
            {
                levelSettings
            });
            command.Done += (sender, args) =>
            {
                _screenFade.FadeIn(FadeTime);
            };
            
            command.Execute();
        }

        private LevelConfig GetLevel()
        {
            if (_levelsList.Count > 0)
            {
                var rand = Random.Range(0, _levelsList.Count);
                var config = _levelsList[rand];
                _levelsList.RemoveAt(rand);

                return config;
            }
            
            return null;
        }
    }
}