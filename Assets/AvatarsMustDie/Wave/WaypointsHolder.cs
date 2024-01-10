using System.Collections.Generic;
using UnityEngine;

namespace AvatarsMustDie.Waypoints
{
    public class WaypointsHolder : MonoBehaviour
    {
        [SerializeField] private List<Transform> transforms;

        public List<Transform> Transforms => transforms;
    }
}