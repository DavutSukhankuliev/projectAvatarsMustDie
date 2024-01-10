using System;
using AvatarsMustDie.Application;
using UnityEngine;

namespace AvatarsMustDie.Base
{
    public class BaseView : MonoBehaviour, ISceneObject
    { 
        [NonSerialized] public float Hp;
        
        public event EventHandler<Collider> EnterTriggerEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnterTriggerEvent?.Invoke(this,other);
            }
        }
    }
}
