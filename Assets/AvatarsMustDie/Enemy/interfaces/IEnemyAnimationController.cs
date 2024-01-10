using System;

namespace AvatarsMustDie.Enemy
{
    public interface IEnemyAnimationController : IDisposable
    {
        public void PlayAnimation(string animName);
    }
}