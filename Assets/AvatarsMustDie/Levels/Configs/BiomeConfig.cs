using System;
using UnityEngine;

namespace AvatarsMustDie.Wave
{
    [CreateAssetMenu(fileName = "BiomeConfig", menuName = "Configs/Biomes/BiomeConfig", order = 0)]
    public class BiomeConfig : ScriptableObject
    {
        [SerializeField] private BiomeType type;
        [SerializeField] private LevelConfig[] levelConfig;

        public LevelConfig[] LevelConfig => levelConfig;
        public BiomeType Type => type;
    }
    
    public enum BiomeType
    {
        Empty,
        Tropical,
        Forest,
        Sand
    }
}