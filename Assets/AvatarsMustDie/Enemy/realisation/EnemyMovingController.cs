using AvatarsMustDie.Waypoints;
using DG.Tweening;
using System;
using UnityEngine;

namespace AvatarsMustDie.Enemy
{
    public readonly struct EnemyMovingControllerProtocol
    {
        public readonly float Speed;
        public readonly Transform View;

        public EnemyMovingControllerProtocol(
            float speed,
            Transform view)
        {
            Speed = speed;
            View = view;
        }
    }

    public class EnemyMovingController : IEnemyMovingController
    {
        public event EventHandler WalkEvent;
        public event EventHandler StandUpEvent;
        
        private readonly Transform _view;
        private readonly float _speed;

        private Tween _walkTween;
        private float _modifier;
        private int _currentIndex;
        private WaypointsHolder _way;

        public EnemyMovingController(EnemyMovingControllerProtocol protocol)
        {
            _speed = protocol.Speed;
            _view = protocol.View;
        }

        public void SetSetting(WaypointsHolder way, float modifier)
        {
            _currentIndex = 0;
            _way = way;
            _modifier = modifier;
        }

        public void StartRunning()
        {
            if (_way != null)
            {
                Walk(_way.Transforms[_currentIndex].position, () =>
                {
                    _currentIndex++;
                    if (_currentIndex < _way.Transforms.Count)
                    {
                        StartRunning();
                    }
                });
            }
        }

        private void Walk(Vector3 point, Action onEnd = null)
        {
            _walkTween.Kill();
            _walkTween = _view
                    .DOMove(point, _speed * Mathf.Max(1f, _modifier) * Vector3.Distance(_view.position, point)).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        onEnd?.Invoke();
                    });
            WalkEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Fall()
        {
            StandUpEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {

        }
    }
}