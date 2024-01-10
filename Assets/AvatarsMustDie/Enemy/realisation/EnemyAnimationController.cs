using UnityEngine;
namespace AvatarsMustDie.Enemy
{
    public readonly struct EnemyAnimationControllerProtocol
    {
        public readonly Animator Animator;

        public EnemyAnimationControllerProtocol(Animator animator)
        {
            Animator = animator;
        }
    }

    public class EnemyAnimationController : IEnemyAnimationController
    {
        private Animator _animator;
        public EnemyAnimationController(EnemyAnimationControllerProtocol protocol)
        {
            _animator = protocol.Animator;
        }

        public void PlayAnimation(string animName)
        {
            _animator.Play(animName);
        }

        public void Dispose()
        {
            
        }
    }
}