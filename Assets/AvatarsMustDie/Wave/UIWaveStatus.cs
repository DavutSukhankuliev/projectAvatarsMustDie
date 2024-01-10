using TMPro;
using UniRx;
using UnityEngine;

namespace AvatarsMustDie.Wave
{
    public readonly struct OnWaveStatusChangeMessage
    {
        public readonly int NumberOfWave;
        public OnWaveStatusChangeMessage(int numberOfWave)
        {
            NumberOfWave = numberOfWave;
        }
    } 
    
    public readonly struct OnPoseDetectrionMessage
    {
        public readonly string PoseInformation;
        public OnPoseDetectrionMessage(string poseInformation)
        {
            PoseInformation = poseInformation;
        }
    } 
    public class UIWaveStatus : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI uiWaveStatusText;
        [SerializeField] private TextMeshProUGUI poseInformation;

        private void Start()
        {
            MessageBroker
                .Default
                .Receive<OnWaveStatusChangeMessage>()
                .Subscribe(message => WaveStatusUpdate(message.NumberOfWave))
                .AddTo(this);
            
            MessageBroker
                .Default
                .Receive<OnPoseDetectrionMessage>()
                .Subscribe(message => PoseStatusUpdate(message.PoseInformation))
                .AddTo(this);
        }

        private void WaveStatusUpdate(int waveInfo)
        {
            uiWaveStatusText.text = $"Wave {waveInfo}";
        }
        
        private void PoseStatusUpdate(string poseInfo)
        {
            poseInformation.text = $"Pose {poseInfo}";
        }
    }
}