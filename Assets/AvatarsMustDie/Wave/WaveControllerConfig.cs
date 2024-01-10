using UnityEngine;

namespace AvatarsMustDie.Wave
{
    [CreateAssetMenu(fileName = "WaveControllerConfig", menuName = "Configs/Wave/WaveControllerConfig", order = 0)]
    
    public class WaveControllerConfig : ScriptableObject
    {
        public float DelayBetweenSpawnEnemyOnWave => delayBetweenSpawnEnemyOnWave;
        public float DelayBetweenSpawnWaves => delayBetweenSpawnWaves;
        public float PercentBaseTypeOfEnemy => percentBaseTypeOfEnemy;
        public float DynamicStatsModifier => dynamicStatsModifier;
        
        [SerializeField] private float delayBetweenSpawnEnemyOnWave;
        [SerializeField] private float delayBetweenSpawnWaves;
        [SerializeField] private float percentBaseTypeOfEnemy;
        [SerializeField] private float dynamicStatsModifier;
    }
}