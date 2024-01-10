using AvatarsMustDie.Waypoints;
using System;
namespace AvatarsMustDie.Enemy
{
    public interface IEnemyMovingController : IDisposable
    {
        public event EventHandler WalkEvent;
        public event EventHandler StandUpEvent;

        public void SetSetting(WaypointsHolder way, float modifier);
        public void StartRunning();
        public void Fall();
    }
}
