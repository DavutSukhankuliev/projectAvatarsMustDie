using System;
using AvatarsMustDie.Application;
using AvatarsMustDie.Player;
using AvatarsMustDie.Wave;
using UniRx;

namespace AvatarsMustDie.PoseDetection
{
    public readonly struct OnDetectPoseMessage
    {
        public readonly bool IsLeft;
        public readonly Poses DetectedPose;
        public readonly bool IsSelected;
        
        public OnDetectPoseMessage(bool isLeft, Poses detectedPose, bool isSelected)
        {
            IsLeft = isLeft;
            DetectedPose = detectedPose;
            IsSelected = isSelected;
            
            MessageBroker.Default.Publish(new OnPoseDetectrionMessage(detectedPose.ToString()));
        }
    } 
    
    public class PoseDetectionController
    {
        private readonly SceneHolder _sceneHolder;

        private PlayerView _playerView;
        private PoseDetectionView _poseDetectionView;
        private bool _isActive;
        
        public PoseDetectionController(
            SceneHolder sceneHolder) 
        {
            _sceneHolder = sceneHolder;
        }

        public void SetupPoseDetection()
        {
            _poseDetectionView = _sceneHolder.Get<PoseDetectionView>();
            _playerView = _sceneHolder.Get<PlayerView>();

            _poseDetectionView.LeftHandPoseOne.Hand = _playerView.LeftHand.GetHand();
            _poseDetectionView.RightHandPoseTwo.Hand = _playerView.RightHand.GetHand();

            for (int i = 0; i < _poseDetectionView.RightHandState.Length; i++)
            {
                _poseDetectionView.RightHandState[i].OnSelectPose += RightHandSelectPose;
                _poseDetectionView.RightHandState[i].OnUnSelectPose += RightHandUnSelectPose;
            }

            for (int i = 0; i < _poseDetectionView.LeftHandState.Length; i++)
            {
                _poseDetectionView.LeftHandState[i].OnSelectPose += LeftHandSelectPose;
                _poseDetectionView.LeftHandState[i].OnUnSelectPose += LeftHandUnSelectPose;
            }

            SetActiveDetection(true);
        }
        
        public void SetActiveDetection(bool value)
        {
            _isActive = value;
        }
        

        private void RightHandSelectPose(Poses pose)
        {
            if (!_isActive)
            {
                return;
            }
            
            MessageBroker.Default.Publish(new OnDetectPoseMessage(false, pose, true));
        }
        
        private void RightHandUnSelectPose(Poses pose)
        {
            if (!_isActive)
            {
                return;
            }
            
            MessageBroker.Default.Publish(new OnDetectPoseMessage(false, pose, false));
        }
        
        private void LeftHandSelectPose(Poses pose)
        {
            if (!_isActive)
            {
                return;
            }
            
            MessageBroker.Default.Publish(new OnDetectPoseMessage(true, pose, true));
        }
        
        private void LeftHandUnSelectPose(Poses pose)
        {
            if (!_isActive)
            {
                return;
            }
            
            MessageBroker.Default.Publish(new OnDetectPoseMessage(true, pose, false));
        }
    }

    [Flags]
    public enum Poses
    {
        None = 1 << 0,
        Ok = 1 << 1,
        ThumbToPalm = 1 << 2,
        SplayedFingers = 1 << 3,
        Peace = 1 << 4,
        ThreeFingers = 1 << 5,
        Fist = 1 << 6,
        GoatWithOpenThumb = 1 << 7,
        IndexFingerForward = 1 << 8,
        ThumbUp = 1 << 9,
        
        OneHandSpellPoses = ThumbToPalm
                            | SplayedFingers
                            | Peace
                            | ThreeFingers,
        
        TwoHandSpellPoses = Fist
                            | GoatWithOpenThumb
                            | IndexFingerForward
                            | ThumbUp
    }
}