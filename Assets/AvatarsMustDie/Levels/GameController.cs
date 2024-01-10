using System.Collections.Generic;
using AvatarsMustDie.Wave;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AvatarsMustDie.Levels
{
    public class GameController
    {
        private readonly IInstantiator _instantiator;
        private readonly BiomesConfig _biomesConfig;

        private List<BiomeType> _biomeStack = new List<BiomeType>();
        private bool _isFirstStart = true;

        public GameController(
            IInstantiator instantiator,
            BiomesConfig biomesConfig)
        {
            _instantiator = instantiator;
            _biomesConfig = biomesConfig;

            MessageBroker.Default.Receive<OnEndBiomeMessage>()
                .Subscribe((message => SetUpBiome()));
        }
        
        public void StartGame()
        {
            if (_biomesConfig.BiomeConfigs.Length <= 0)
            {
                Debug.LogError("BiomesConfig is empty");
                return;
            }

            if (_isFirstStart)
            {
                var command = _instantiator.Instantiate<InitGameCommand>();
                command.Done += (sender, args) =>
                {
                    for (int i = 0; i < _biomesConfig.BiomeConfigs.Length; i++)
                    {
                        _biomeStack.Add(_biomesConfig.BiomeConfigs[i].Type);
                    }
        
                    SetUpBiome();
                    
                    _isFirstStart = false;
                };
                
                command.Execute();
            }
            else
            {
                for (int i = 0; i < _biomesConfig.BiomeConfigs.Length; i++)
                {
                    _biomeStack.Add(_biomesConfig.BiomeConfigs[i].Type);
                }
            
                SetUpBiome();
            }
        }

        private void SetUpBiome()
        {
            if (_biomeStack.Count > 0)
            {
                var rand = Random.Range(0, _biomeStack.Count);
                var command = _instantiator.Instantiate<ChangeBiomeCommand>();
                command.Execute(_biomeStack[rand])
                    .Forget();
                
                _biomeStack.RemoveAt(rand);
            }
            else
            {
                StartGame();
            }
        }
    }
}