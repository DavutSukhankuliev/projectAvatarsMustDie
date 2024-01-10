using System;
using System.Collections.Generic;
using System.Linq;
using AvatarsMustDie.Enemy;
using Cysharp.Threading.Tasks;
using VGCore;
using Random = System.Random;

namespace AvatarsMustDie.Wave
{
    public class SpawnEnemyOnWaveCommand : Command
    {
        private List<int> _enemyListID = new List<int>();
        private List<int> _baseEnemyListID = new List<int>();
        private List<int> _enemyListToSpawnID = new List<int>();

        private readonly WaveProtocol _waveProtocol;
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyStorage _enemyStorage;
        private readonly Random _random;
        
        public SpawnEnemyOnWaveCommand(
            CommandStorage commandStorage,
            WaveProtocol waveProtocol,
            EnemyConfig enemyConfig,
            EnemyStorage enemyStorage) 
            : base(commandStorage)
        {
            _waveProtocol = waveProtocol;
            _enemyConfig = enemyConfig;
            _enemyStorage = enemyStorage;
            _random = new Random();
        }

        public override CommandResult Execute()
        {
            _enemyListID.Clear();

            for (int i = 0; i < _enemyConfig.GetCountModels(); i++)
            {
                var model = _enemyConfig.Get(i);
                
                if (model.BiomeType == _waveProtocol.Config.BiomeType)
                {
                    if (model.EnemyType == EnemyType.Base)
                    {
                        _baseEnemyListID.Add(i);
                    }
                    _enemyListID.Add(i);
                }
            }
            
            var countEnemyPerWave = _waveProtocol.Config.EnemyCount;
            var countBaseEnemy = (int) (countEnemyPerWave * _waveProtocol.PercentBase);

            for (int i = 0; i < countBaseEnemy; i++)
            {
                _enemyListToSpawnID.Add(_baseEnemyListID[UnityEngine.Random.Range(0, _baseEnemyListID.Count)]);
            }

            for (int i = 0; i < countEnemyPerWave - countBaseEnemy; i++)
            {
                var modelID = _enemyListID[UnityEngine.Random.Range(0, _enemyListID.Count)];
                var model = _enemyConfig.Get(modelID);
                
                if (model.LimitPerLevel != 0 && _enemyListToSpawnID.Count(x => x == modelID) > model.LimitPerLevel)
                {
                    _enemyListToSpawnID.Add(modelID);
                }

                if (model.LimitPerLevel == 0)
                {
                    _enemyListToSpawnID.Add(modelID);
                }
            }

            _enemyListToSpawnID.Shuffle();

            SpawnEnemy()
                .Forget();
            return base.Execute();
        }

        private async UniTaskVoid SpawnEnemy()
        {
            var numberOfWay = _random.Next(0, _waveProtocol.WaypointsHolders.Count);
            
            var modelID = _enemyListToSpawnID[UnityEngine.Random.Range(0, _enemyListID.Count)];
            var enemyPrefab = _enemyConfig.Get(modelID);
            
           
            if (enemyPrefab.CountEnemyInArmy > 0)
            {
                for (int i = 0; i < enemyPrefab.CountEnemyInArmy; i++)
                {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(enemyPrefab.DelayBetweenSpawnEnemyInArmy), 
                        ignoreTimeScale: false);

                   var mob = _enemyStorage.Spawn(modelID,_waveProtocol.DifficultyModifier);
                   mob.View.BodyTransform.SetParent(_waveProtocol.WaypointsHolders[numberOfWay]
                       .Transforms[_random.Next(0,_waveProtocol.WaypointsHolders[numberOfWay].Transforms.Count)],false);
                }
            }
            else
            {
                var mob = _enemyStorage.Spawn(modelID,_waveProtocol.DifficultyModifier);
                mob.View.BodyTransform.SetParent(_waveProtocol.WaypointsHolders[numberOfWay]
                    .Transforms[_random.Next(0,_waveProtocol.WaypointsHolders[numberOfWay].Transforms.Count)],false);
            }

            _enemyListToSpawnID.RemoveAt(0);
            
            await UniTask.Delay(
                TimeSpan.FromSeconds(_waveProtocol.DelayBetweenSpawnEnemyOnWave), 
                ignoreTimeScale: false);

            if (_enemyListToSpawnID.Count == 0)
            {
                Done?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SpawnEnemy()
                    .Forget();
            }
        }
    }
}