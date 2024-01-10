using AvatarsMustDie.Application;
using Oculus.Interaction;
using UnityEngine;

namespace AvatarsMustDie.Player
{
    public class PlayerView : MonoBehaviour, ISceneObject
    {
        public OVRScreenFade ScreenFade => screenFade;
        public OVRCameraRig CameraRig => cameraRig;
        public HandActiveState LeftHand => leftHand;
        public HandActiveState RightHand => rightHand;


        [SerializeField] private OVRScreenFade screenFade;
        [SerializeField] private OVRCameraRig cameraRig;

        [SerializeField] private HandActiveState leftHand;
        [SerializeField] private HandActiveState rightHand;
    }
}