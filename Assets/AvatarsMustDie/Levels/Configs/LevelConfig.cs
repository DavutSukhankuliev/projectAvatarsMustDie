using AvatarsMustDie.Levels;
using UnityEngine;

namespace AvatarsMustDie.Wave
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private WaveConfig[] waveConfig;
        [SerializeField] private string levelObjSource;
            
        public WaveConfig[] WaveConfig => waveConfig;
        public int ID => id;
        public string LevelObjSource => levelObjSource;
    }
}