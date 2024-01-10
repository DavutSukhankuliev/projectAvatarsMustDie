using System;
using DG.Tweening;
using Oculus.Interaction;
using TMPro;
using UnityEngine;

namespace AvatarsMustDie.UI
{
    public class UIButton : MonoBehaviour
    {
        public event Action OnClickEvent;
        
        [SerializeField] private InteractableUnityEventWrapper interactableUnityEventWrapper;
        [SerializeField] private InteractableColorVisual interactableColorVisual;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color disabledColor;
        [SerializeField] private Color normalColor;
        
        private bool _isActive = true;
        
        public void SetActive(bool value)
        {
            _isActive = value;
            
            if (value)
            {
                interactableColorVisual.InjectOptionalNormalColorState(new InteractableColorVisual.ColorState(normalColor));
                text.DOFade(0f, 0.3f);
                return;
            }
            interactableColorVisual.InjectOptionalNormalColorState(new InteractableColorVisual.ColorState(disabledColor));
            text.DOFade(0.9f, 0.3f);
        }
        
        private void Awake()
        {
            if (interactableUnityEventWrapper != null)
            {
                interactableUnityEventWrapper.WhenSelect.AddListener(OnClick);
            }
        }

        public void OnClick()
        {
            if (_isActive)
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}
