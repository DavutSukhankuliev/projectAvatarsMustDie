using System;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarsMustDie.Base
{
    [Serializable]
    public struct BaseModel
    {
        [SerializeField] private string key;
        [SerializeField] private Vector3 posOffset;
        [SerializeField] private Quaternion rotationOffset;
        [SerializeField] private Vector3 scale;
        [SerializeField] private float hp;

        public string Key => key;
        public Vector3 PosOffset => posOffset;
        public Quaternion RotationOffset => rotationOffset;
        public Vector3 Scale => scale;
        public float Hp => hp;
    }
    
    [CreateAssetMenu(fileName = "BaseConfig", menuName = "Configs/BaseConfig", order = 0)]
    public class BaseConfig : ScriptableObject
    {
        [SerializeField] private BaseModel[] baseModels;

        private readonly Dictionary<string, BaseModel> _dictionary = new Dictionary<string, BaseModel>();

        private void OnValidate()
        {
            if (_dictionary.Count != 0)
            {
                return;
            }

            foreach (var baseModel in baseModels)
            {
                _dictionary.Add(baseModel.Key, baseModel);
            }
        }

        public BaseModel Get(string id)
        {
            if (_dictionary.Count != 0)
            {
                return _dictionary.ContainsKey(id) ? _dictionary[id] : new BaseModel();
            }
            Debug.LogError($"The BaseConfig dictionary is empty");
            return default;
        }
    }
}