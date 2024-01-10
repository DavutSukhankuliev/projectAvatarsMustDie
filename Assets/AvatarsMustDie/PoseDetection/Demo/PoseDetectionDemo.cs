using AvatarsMustDie.Application;
using AvatarsMustDie.Player;
using TMPro;
using UniRx;
using UnityEngine;

namespace AvatarsMustDie.PoseDetection.Demo
{
    public class PoseDetectionDemo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rightHandText;
        [SerializeField] private TextMeshProUGUI leftHandText;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PoseDetectionView poseDetectionView;

        private const string RightHandText = "Right hand is : ";
        private const string LeftHandText = "Left hand is : ";
        
        private void Awake()
        {
            var sceneHolder = new SceneHolder();
            sceneHolder.Add<PlayerView>(playerView);
            sceneHolder.Add<PoseDetectionView>(poseDetectionView);
            
            var poseController = new PoseDetectionController(sceneHolder);
            poseController.SetupPoseDetection();
            
            MessageBroker.Default.Receive<OnDetectPoseMessage>()
                .Subscribe(message => ShowMessage(message))
                .AddTo(this);
        }

        private void ShowMessage(OnDetectPoseMessage message)
        {
            if (message.IsLeft)
            {
                leftHandText.SetText(LeftHandText + message.DetectedPose);
                return;
            }

            rightHandText.SetText(RightHandText + message.DetectedPose);
        }
    }
}
