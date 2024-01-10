using System.Collections.Generic;
using AvatarsMustDie.PoseDetection;
using UniRx;

namespace AvatarsMustDie.Spells
{
    public class SpellsController
    {
        private readonly Spell.Factory _factory;
        private readonly SpellsConfig _spellsConfig;
        private readonly SpellTimer _timer;
        private Dictionary<Poses, int> _charegesDict = new Dictionary<Poses, int>();
        private Dictionary<Poses, bool> _spellAviableDict = new Dictionary<Poses, bool>();

        public SpellsController(
            Spell.Factory factory,
            SpellsConfig spellsConfig,            
            SpellTimer timer)
        {
            _timer = timer;
            _factory = factory;
            _spellsConfig = spellsConfig;           

            MessageBroker.Default.Receive<OnDetectPoseMessage>()
                .Subscribe(DetectPose);
        }        

        private void DetectPose(OnDetectPoseMessage message)
        {
            if (message.IsSelected)
            {
                if(!_spellAviableDict.ContainsKey(message.DetectedPose))
                {
                    _spellAviableDict.Add(message.DetectedPose, true);
                }

                if(_spellAviableDict[message.DetectedPose])
                {
                    _factory.Create(_spellsConfig.Get(message.DetectedPose).Prefab).Cast();

                    if (!_charegesDict.ContainsKey(message.DetectedPose))
                    {
                        _charegesDict.Add(message.DetectedPose, 0);
                    }
                    
                    _charegesDict[message.DetectedPose]++;

                    if(_charegesDict[message.DetectedPose] >= _spellsConfig.Get(message.DetectedPose).Charges)
                    {
                        _spellAviableDict[message.DetectedPose] = false;
                        _timer.AddSpell(_spellsConfig.Get(message.DetectedPose).CountdownTime,()=> { _spellAviableDict[message.DetectedPose] = true; });
                    }
                }                
            }
        }
    }
}
