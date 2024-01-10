using System.Collections.Generic;
using AvatarsMustDie.Wave;
using Cysharp.Threading.Tasks;
using Stateless;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Levels
{
    internal struct StateData
    {
        public readonly BiomeType Type;
        public readonly IBiomeState State;

        public StateData(BiomeType type, IBiomeState state)
        {
            Type = type;
            State = state;
        }
    }
    
    public class BiomesStateMachine
    {
        private readonly IInstantiator _instantiator;
        private readonly BiomesConfig _biomesConfig;
        
        private StateMachine<IBiomeState,BiomeType> _stateMachine;

        public BiomesStateMachine(
            IInstantiator instantiator,
            BiomesConfig biomesConfig)
        {
            _instantiator = instantiator;
            _biomesConfig = biomesConfig;

            var stateDataList = new List<StateData>();

            var empty = _instantiator.Instantiate<BiomeState>();
            
            _stateMachine = new StateMachine<IBiomeState, BiomeType>(empty);

            stateDataList.Add(new StateData(BiomeType.Empty, empty));
            
            for (int i = 0; i < _biomesConfig.BiomeConfigs.Length; i++)
            {
                var biomeConfig = _biomesConfig.BiomeConfigs[i];
                var state = CreateState<BiomeState>(biomeConfig);

                for (int j = 0; j < stateDataList.Count; j++)
                {
                    if (stateDataList[i].Type == biomeConfig.Type)
                    {
                        Debug.LogError("There are several configs with the same biome type");
                        break;
                    }
                }

                stateDataList.Add(new StateData(biomeConfig.Type, state));

                _stateMachine.Configure(state)
                    .OnEntryAsync(state.OnEntry)
                    .OnExitAsync(state.OnExit);
            }
            
            for (int i = 0; i < stateDataList.Count; i++)
            {
                _stateMachine.Configure(stateDataList[i].State)
                    .PermitReentry(stateDataList[i].Type);
                
                for (int j = 0; j < stateDataList.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    _stateMachine.Configure(stateDataList[i].State)
                        .Permit(stateDataList[j].Type, stateDataList[j].State);
                }
            }
        }

        public UniTask ChangeOfState(BiomeType type)
        {
            return _stateMachine.FireAsync(type)
                .AsUniTask();
        }

        private IBiomeState CreateState<T>(BiomeConfig biomeConfig) where T : IBiomeState
        {
            var state = _instantiator.Instantiate<T>();
            state.Init(biomeConfig);
            return state;
        }
    }
}