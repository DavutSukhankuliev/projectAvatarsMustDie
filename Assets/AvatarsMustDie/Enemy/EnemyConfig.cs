using UnityEngine;
using System;
using AvatarsMustDie.Wave;
using UnityEditor.Animations;

namespace AvatarsMustDie.Enemy
{
    public enum EnemyType
    {
        Base,
        NotBase
    }
    
    [Serializable]
    public struct EnemyModel
    {
        public int Name;
        public EnemyType EnemyType;
        public BiomeType BiomeType;
        public GameObject MeshPrefab;
        public Avatar Avatar;
        public AnimatorController AnimationController;
        public string BaseAnimationClip;
        public float Speed;
        public int Health;
        public string Controller;
        public string ExtraArg;
        public int LimitPerLevel;
        public int CountEnemyInArmy;
        public float DelayBetweenSpawnEnemyInArmy ;
    }

    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : ScriptableObject
    {           
        [SerializeField] private EnemyModel[] models;

        public EnemyModel Get(int id)
        {
            if (id < models.Length)
            {
                return models[id];
            }
            return new EnemyModel();
        }

        public int GetCountModels()
        {
            return models.Length;
        }
    }
}