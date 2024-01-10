using System;
using DG.Tweening;
using UnityEngine;

namespace AvatarsMustDie.UI
{
    [Serializable]
    public struct DoSettings
    {
        public float duration;
        public Ease ease;
        public float delay;
    }
    
    public class UIWindowAppearAnimation : MonoBehaviour
    {
        public CanvasGroup CanvasGroup => canvasGroup;
        
        [SerializeField] private DoSettings settings;
        [SerializeField] private Vector2 @from;
        [SerializeField] private Vector2 to;
        [SerializeField] private RectTransform body;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private Tween _tween;
        private Tween _tween2;

        public void Play(Action onEnd = null)
        {
            Stop();

            _tween = body
                .DOAnchorPos(to, settings.duration)
                .SetDelay(settings.delay)
                .SetEase(settings.ease)
                .OnComplete(() =>
                {
                    onEnd?.Invoke();
                });

            if (canvasGroup != null)
            {
                _tween2 = canvasGroup.DOFade(1.0f, settings.duration)
                    .SetDelay(settings.delay)
                    .SetEase(settings.ease);
                _tween2.SetAutoKill(false);
            }

            _tween.SetAutoKill(false);
        }

        public void Backward(Action onEnd = null)
        {
            if (_tween == null)
            {
                return;
            }
            
            Stop();

            if (canvasGroup != null)
            {
                _tween2.PlayBackwards();
            }

            _tween.PlayBackwards();
            _tween.OnRewind(() =>
            {
                onEnd?.Invoke();
            });
        }

        public void ResetValues()
        {
            Stop();
            
            if (_tween != null)
            {
                _tween.Kill();
            }

            if (canvasGroup != null)
            {
                _tween2.Kill();
                canvasGroup.alpha = 0f;
            }
            
            body.anchoredPosition = @from;
            _tween = null;
            _tween2 = null;
        }

        private void Stop()
        {
            if (_tween != null)
            {
                _tween.Pause();
            }

            if (_tween2 != null)
            {
                _tween2.Pause();
            }
        }
    }
}