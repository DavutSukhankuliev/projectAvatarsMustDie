using System;
using Oculus.Interaction;
using UnityEngine;

namespace AvatarsMustDie.PoseDetection
{
    public class ActiveStateHelper : MonoBehaviour
    {
        public Action<Poses> OnSelectPose;
        public Action<Poses> OnUnSelectPose;
        
        [SerializeField] private ActiveStateSelector activeStateSelector;
        [SerializeField] private Poses pose;

        private void Awake()
        {
            activeStateSelector.WhenSelected += () =>
            {
                OnSelectPose?.Invoke(pose);
            };
            
            activeStateSelector.WhenUnselected += () =>
            {
                OnUnSelectPose?.Invoke(pose);
            };
        }
    }
}
