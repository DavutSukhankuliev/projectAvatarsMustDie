using Zenject;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AvatarsMustDie.Enemy
{
    public class EnemyStorage
    {
        private readonly EnemyConfig _config;
        private readonly IInstantiator _instantiator;
        
        private Dictionary<int, IEnemyController> _dict = new Dictionary<int, IEnemyController>();

        public EnemyStorage(
            EnemyConfig config,
            IInstantiator instantiator)
        {
            _config = config;
            _instantiator = instantiator;
        }
        public IEnemyController Spawn(int modelID, float difModifier)
        {
            var model = _config.Get(modelID);
            if (model.Health != 0)
            {
                if (string.IsNullOrEmpty(model.Controller))
                {
                    var controller = _instantiator.Instantiate<BaseEnemyController>(new object[] {
                        new BaseEnemyControllerProtocol(
                            (int)(model.Health * difModifier),
                            model.Speed * difModifier,
                            model.MeshPrefab,
                            model.ExtraArg,
                            model.Avatar,
                            model.BaseAnimationClip
                        )
                    });
                    _dict.Add(controller.View.GetInstanceID(), controller);
                    
                    controller.Health.DeathEvent += (object sender, EventArgs e) =>
                    {
                        Clear(controller.View.GetInstanceID());
                    };

                    return controller;
                }
                var type = Type.GetType($"AvatarsMustDie.Enemy.Controllers.{model.Controller}, Assembly-CSharp");

                if (type == null)
                {
                    Debug.LogError($"Required command type not found. ID = {model.Controller}");
                    return null;
                }

                var anotherController = _instantiator.Instantiate(type, new object[]
                {
                    new BaseEnemyControllerProtocol(
                        (int)(model.Health * difModifier),
                        model.Speed * difModifier,
                        model.MeshPrefab,
                        model.ExtraArg,
                        model.Avatar,
                        model.BaseAnimationClip)
                }) as IEnemyController;
                
                _dict.Add(anotherController.View.GetInstanceID(), anotherController);
                
                anotherController.Health.DeathEvent += (object sender, EventArgs e) =>
                {
                    Clear(anotherController.View.GetInstanceID());
                };

                return anotherController;
            }
            return null;
        }

        public IEnemyController Get(int instanceID)
        {
            if (_dict.ContainsKey(instanceID))
            {
                return _dict[instanceID];
            }

            return null;
        }

        public void Clear(int instanceID)
        {
            if (_dict.ContainsKey(instanceID))
            {
                _dict[instanceID].Dispose();
                _dict.Remove(instanceID);
            }
        }

        public void ClearAll()
        {
            var keys = new List<int>(_dict.Keys);
            foreach (int key in keys)
            {
                Clear(key);
            }
        }
    }
}