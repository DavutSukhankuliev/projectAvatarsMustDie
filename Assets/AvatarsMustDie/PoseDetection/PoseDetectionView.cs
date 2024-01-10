using AvatarsMustDie.Application;
using Oculus.Interaction.Input;
using UnityEngine;

namespace AvatarsMustDie.PoseDetection
{
    public class PoseDetectionView : MonoBehaviour, ISceneObject
    {
        public HandRef LeftHandPoseOne => leftHandPoseOne;
        public HandRef RightHandPoseTwo => rightHandPoseTwo;
        
        public ActiveStateHelper[] RightHandState => rightHandState;
        public ActiveStateHelper[] LeftHandState => leftHandState;

        [SerializeField] private HandRef leftHandPoseOne;
        [SerializeField] private HandRef rightHandPoseTwo;
        
        [SerializeField] private ActiveStateHelper[] rightHandState;
        [SerializeField] private ActiveStateHelper[] leftHandState;
    }
}