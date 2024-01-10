using UnityEngine;

namespace AvatarsMustDie.Wave
{
    [CreateAssetMenu(fileName = "BiomesConfig", menuName = "Configs/Biomes/BiomesConfig", order = 0)]
    public class BiomesConfig : ScriptableObject
    {
        [SerializeField] private BiomeConfig[] biomeConfig;

        public BiomeConfig[] BiomeConfigs => biomeConfig;
    }
}