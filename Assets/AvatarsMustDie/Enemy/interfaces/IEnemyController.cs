using System;
namespace AvatarsMustDie.Enemy
{
    public interface IEnemyController : IDisposable
    {
        public EnemyView View { get; }
        public IEnemyMovingController MoveController { get; }
        public IHealthHandler Health { get; }
        public IEnemyAnimationController Animation { get; }
    }
}
