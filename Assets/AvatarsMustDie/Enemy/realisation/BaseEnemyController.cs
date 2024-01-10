using System;
using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Enemy
{
    public readonly struct BaseEnemyControllerProtocol
    {
        public readonly GameObject MeshPrefab;
        public readonly Avatar Avatar;
        public readonly string BaseAnimationClip;
        public readonly float Speed;
        public readonly int Health;
        public readonly string ExtraArgs;
        
        public BaseEnemyControllerProtocol(
            int health,
            float speed,
            GameObject meshPrefab,
            string extraArgs, 
            Avatar avatar,
            string baseAnimationClip)
        {
            ExtraArgs = extraArgs;
            Health = health;
            Speed = speed;
            MeshPrefab = meshPrefab;
            Avatar = avatar;
            BaseAnimationClip = baseAnimationClip;
        }
    }

    public class BaseEnemyController : IEnemyController
    {
        public EnemyView View => _view;
        public IEnemyMovingController MoveController => _moving;
        public IHealthHandler Health => _health;
        public IEnemyAnimationController Animation => _animation;
        
        protected EnemyView _view;
        protected IEnemyAnimationController _animation;
        protected IEnemyMovingController _moving;
        protected IHealthHandler _health;

        public BaseEnemyController(
            EnemyView.Factory viewFactory,
            IInstantiator instantiator,
            BaseEnemyControllerProtocol protocol)
        {
            _view = viewFactory.Create(
                new EnemyViewProtocol(
                    protocol.MeshPrefab,
                    protocol.Avatar));
            
            _view.HealthBar.SetMaxValue(protocol.Health);
            _view.HealthBar.SetCurrentValue(protocol.Health);

            _moving = instantiator.Instantiate<EnemyMovingController>(new object[]
            {
                new EnemyMovingControllerProtocol(
                    protocol.Speed,
                    _view.BodyTransform)
            });
            
            _health = instantiator.Instantiate<EnemyHealthHandler>(new object[]
            {
                new EnemyHealthHandlerProtocol(protocol.Health)
            });

            _health.HealthChangeEvent += (sender, i) =>
            {
                _view.HealthBar.SetCurrentValue(i);
            };
            
            _animation = instantiator.Instantiate<EnemyAnimationController>(new object[]
            {
                new EnemyAnimationControllerProtocol(
                    _view.CharacterAnimator)
            });
            _animation.PlayAnimation(protocol.BaseAnimationClip);

            _moving.StandUpEvent += (object sender, EventArgs e) =>
            {
                //_animation.PlayAnimation(EnemyAnimationConsts.StandUp);
            };
            _moving.WalkEvent += (object sender, EventArgs e) =>
            {
                //_animation.PlayAnimation(EnemyAnimationConsts.Walk);
            };            
        }

        public void Dispose()
        {
            _moving.Dispose();
            _health.Dispose();
            _animation.Dispose();
            GameObject.Destroy(_view.gameObject);
        }
    }
}