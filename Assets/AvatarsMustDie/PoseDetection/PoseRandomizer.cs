using System;
using System.Collections.Generic;
using Random = System.Random;

namespace AvatarsMustDie.PoseDetection
{
    public class PoseRandomizer
    {
        private Random _random = new Random();
        
        public List<Poses> GetPoseList(bool isOneHandSpells)
        {
            Poses handPoses;
            if (isOneHandSpells)
            {
                handPoses = Poses.OneHandSpellPoses;
            }
            else
            {
                handPoses = Poses.TwoHandSpellPoses;
            }
            
            var values = Enum.GetValues(typeof(Poses));
            var bufList = new List<Poses>();
            for (int i = 0; i < values.Length; i++)
            {
                var value = (Poses)values.GetValue(i);

                if (handPoses.HasFlag(value)
                    && value != handPoses)
                {
                    bufList.Add(value);  
                }
            }

            var list = new List<Poses>();
            while (bufList.Count > 0)
            {
                var randomIndex = _random.Next(0, bufList.Count);
                list.Add(bufList[randomIndex]);
                bufList.RemoveAt(randomIndex);
            }

            return list;
        }
    }
}