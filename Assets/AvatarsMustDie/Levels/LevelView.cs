using System.Collections.Generic;
using AvatarsMustDie.Waypoints;
using UnityEngine;

namespace AvatarsMustDie.Levels
{
    public class LevelView : MonoBehaviour
    {
        public List<WaypointsHolder> Ways => ways;

        [SerializeField] private List<WaypointsHolder> ways;
    }
}