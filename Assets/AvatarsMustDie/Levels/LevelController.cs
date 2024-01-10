using System;
using AvatarsMustDie.Application.Installers;
using AvatarsMustDie.Wave;
using UniRx;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Levels
{
    public readonly struct OnStartWavesMessage
    { } 
    
    public class LevelController : ITickable
    {
        public event Action<LevelView> OnEndLevel;
        
        private readonly WaveController _waveController;
        private readonly DevelopSettings _developSettings;
        private readonly WaveControllerConfig _waveControllerConfig;

        private const float DifficultModifier = 0.1f;
        private const float DeltaBaseCreepsModifier = 0.05f;
        private const float MinBasePercentOfCreeps = 0.1f;

        private bool _isStartedLevel;
        private LevelConfig _levelConfig;
        private LevelView _levelView;
        private float _difficult = 0.9f;
        private float _baseCreepsPercents;

        private int _levelCounter;

        public LevelController(
            TickableManager tickableManager,
            WaveController waveController,
            DevelopSettings developSettings,
            WaveControllerConfig waveControllerConfig)
        {
            _waveController = waveController;
            _developSettings = developSettings;
            _waveControllerConfig = waveControllerConfig;

            _waveController.OnEndAllWavesEvent += () => Debug.Log("All waves has been spawned");

            MessageBroker
                .Default
                .Receive<OnStartWavesMessage>()
                .Subscribe(message => OnStartWaves());
            
            _baseCreepsPercents = _waveControllerConfig.PercentBaseTypeOfEnemy;

            tickableManager.Add(this);
        }

        public void StartLevel(LevelView view, LevelConfig config)
        {
            _difficult += DifficultModifier;
            _levelView = view;
            _levelConfig = config;

            _isStartedLevel = true;
        }

        private void OnStartWaves()
        {
            _waveController.StartSpawn(_levelView.Ways, _levelConfig.WaveConfig, _difficult, _baseCreepsPercents);
        }
        
        private void EndLevel()
        {
            if (_isStartedLevel)
            {
                OnEndLevel?.Invoke(_levelView);
                _isStartedLevel = false;
                
                _baseCreepsPercents -= DeltaBaseCreepsModifier;
                
                if (_baseCreepsPercents <= MinBasePercentOfCreeps)
                {
                    _baseCreepsPercents = MinBasePercentOfCreeps;
                }
            }
        }
        
        public void Tick()
        {
            if (!_developSettings.IsOn)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)
                || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                EndLevel();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MessageBroker.Default.Publish(new OnStartWavesMessage());
            }
        }
    }
}