using System;
using System.Collections.Generic;
using AvatarsMustDie.Levels;
using AvatarsMustDie.Waypoints;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Wave
{
    public readonly struct WaveProtocol
    {
        public readonly WaveConfig Config;
        public readonly float DifficultyModifier;
        public readonly float DelayBetweenSpawnEnemyOnWave;
        public readonly List<WaypointsHolder> WaypointsHolders;
        public readonly float PercentBase;

        public WaveProtocol(
            WaveConfig config,
            float difficultyModifier,
            float delayBetweenSpawnEnemyOnWave,
            List<WaypointsHolder> waypointsHolders,
            float percentBase)
            
        {
            Config = config;
            DifficultyModifier = difficultyModifier;
            WaypointsHolders = waypointsHolders;
            DelayBetweenSpawnEnemyOnWave = delayBetweenSpawnEnemyOnWave;
            PercentBase = percentBase;
        }
    }
    
    public class WaveController
    {
        public event Action OnEndAllWavesEvent;
        
        private readonly IInstantiator _instantiator;
        private readonly WaveControllerConfig _waveControllerConfig;
        private readonly TickableManager _tickableManager;

        private List<WaypointsHolder> _waypointsHolders = new List<WaypointsHolder>();
        private WaveConfig[] _waveConfigs;
        
        private float _dynamicStatModifier;
        private int _iterator;

        public WaveController(
            IInstantiator instantiator,
            WaveControllerConfig waveControllerConfig)
        {
            _instantiator = instantiator;
            _waveControllerConfig = waveControllerConfig;
            
            MessageBroker.Default.Receive<OnEndBiomeMessage>()
                .Subscribe((message => UpdateModifier()));
        }

        public void StartSpawn(List<WaypointsHolder> waypointsHolders, WaveConfig[] waveConfigs, float difficult, float percentBaseCreeps)
        {
            _iterator = 0;
            _waypointsHolders = waypointsHolders;
            _waveConfigs = waveConfigs;
            _dynamicStatModifier = difficult;

            if (waveConfigs.Length == 0)
            {
                Debug.LogError("WaveConfigs is empty!");
                return;
            }

            SpawnWave(waveConfigs[_iterator], percentBaseCreeps);
        }

        private void SpawnWave(WaveConfig waveConfig, float percentBaseCreeps)
        {
            MessageBroker.Default.Publish(new OnWaveStatusChangeMessage(_iterator++));
            
            var protocol = new WaveProtocol(
                waveConfig, 
                _dynamicStatModifier,
                _waveControllerConfig.DelayBetweenSpawnEnemyOnWave, 
                _waypointsHolders,
                _waveControllerConfig.PercentBaseTypeOfEnemy - percentBaseCreeps);
            
            var command = _instantiator.Instantiate<SpawnEnemyOnWaveCommand>(new object[] {protocol});
            command.Done += (sender, args) =>
            {
                DOVirtual.DelayedCall(_waveControllerConfig.DelayBetweenSpawnWaves, (() =>
                {
                    if (_iterator >= _waveConfigs.Length)
                    {
                        OnEndAllWavesEvent?.Invoke();
                        return;
                    }
                    
                    SpawnWave(_waveConfigs[_iterator], percentBaseCreeps);
                }));
            };
            command.Execute();
        }

        private void UpdateModifier()
        {
            _dynamicStatModifier += _waveControllerConfig.DynamicStatsModifier;
        }
    }
}