using System;
using System.Collections.Generic;
using AvatarsMustDie.PoseDetection;
using UnityEngine;

namespace AvatarsMustDie.Spells
{
    [CreateAssetMenu(fileName = "SpellsConfig", menuName = "Configs/SpellsConfig", order = 0)]
    public class SpellsConfig : ScriptableObject
    {
        [SerializeField] private SpellData[] spellProtocols;
        [NonSerialized] private bool _isInited;

        private Dictionary<Poses, SpellData> _dict = new Dictionary<Poses, SpellData>();

        private void Init()
        {
            foreach (var spellData in spellProtocols)
            {
                _dict.Add(spellData.Pose, spellData);
            }

            _isInited = true;
        }

        public SpellData Get(Poses pose)
        {
            if (!_isInited)
            {
                Init();
            }

            if (_dict.ContainsKey(pose))
            {
                return _dict[pose];
            }

            return new SpellData();
        }
    }
    
    [Serializable]
    public struct SpellData
    {
        public Poses Pose;
        public SpellType SpellType;
        public float CountdownTime;
        public int Charges;
        public Spell Prefab;
    }
    
    public enum SpellType
    {
        Fireball,
        Iceball,
        Lightning,
        Shield,
        MeteorRain,
        God,
        TimeWarp
    }
}