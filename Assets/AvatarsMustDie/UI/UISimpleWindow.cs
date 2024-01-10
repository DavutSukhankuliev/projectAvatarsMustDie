using UnityEngine;
using VGUIService;

namespace AvatarsMustDie.UI
{
    public class UISimpleWindow : UIWindow
    {
        [SerializeField] private UIWindowAppearAnimation animation;
        
        public override void Show()
        {
            if (animation != null)
                animation.ResetValues();
            
            gameObject.SetActive(true);
            
            if (animation != null)
                animation.Play();
        }

        public override void Hide()
        {
            if (animation != null)
            {
                animation.Backward(() =>
                {
                    gameObject.SetActive(false);
                });
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}