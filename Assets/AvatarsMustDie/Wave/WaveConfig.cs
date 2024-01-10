using UnityEngine;

namespace AvatarsMustDie.Wave
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Configs/Wave/WaveConfig", order = 0)]
    public class WaveConfig : ScriptableObject
    {
        public BiomeType BiomeType => biomeType;
        public int EnemyCount => enemyCount;
        
        [SerializeField] private BiomeType biomeType;
        [SerializeField] private int enemyCount;
    }
}