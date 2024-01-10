using System;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarsMustDie.Application
{
    public class SceneHolder
    {
        private Dictionary<Type, ISceneObject> _sceneObjectsStorage = new Dictionary<Type, ISceneObject>();

        public void Add<T>(ISceneObject obj) where T : ISceneObject
        {
            var type = typeof(T);

            if (_sceneObjectsStorage.ContainsKey(type))
            {
                Debug.LogError($"Object with type \"{type}\" has already been added");
                return;
            }
            
            _sceneObjectsStorage.Add(type, obj);
        }

        public T Get<T>() where T : ISceneObject
        {
            var type = typeof(T);

            if (_sceneObjectsStorage.ContainsKey(type))
            {
                return (T)_sceneObjectsStorage[type];
            }

            Debug.LogError($"Object with type \"{type}\" is not found for getting");
            return default;
        }

        public bool Contains<T>() where T : ISceneObject
        {
            var type = typeof(T);

            if (_sceneObjectsStorage.ContainsKey(type))
            {
                return true;
            }
            
            return false;
        }

        public void Remove<T>() where T : ISceneObject
        {
            var type = typeof(T);

            if (_sceneObjectsStorage.ContainsKey(type))
            {
                _sceneObjectsStorage.Remove(type);
            }

            Debug.LogError($"Object with type \"{type}\" is not found for removing");
        }
    }
}
