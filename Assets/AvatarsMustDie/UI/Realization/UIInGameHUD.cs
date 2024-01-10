using AvatarsMustDie.Application.Installers;
using AvatarsMustDie.Levels;
using UnityEngine;
using VGUIService;
using Zenject;

// TODO: Just a demo script. Further must be used to show in game HUD (above rocks and game map)

namespace AvatarsMustDie.UI
{
    public class UIInGameHUD : UISimpleWindow
    {
        [SerializeField] private UIButton pauseGameButton;

        private IUIService _uiService;
        private DevelopSettings _developSettings;
        private GameController _gameController;
        
        public override void Show()
        {
            base.Show();
            
            pauseGameButton.OnClickEvent += OnPauseGameButtonClickEvent;
            
            pauseGameButton.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();
            
            pauseGameButton.OnClickEvent -= OnPauseGameButtonClickEvent;
        }

        [Inject]
        private void Inject(
            IUIService uiService,
            DevelopSettings developSettings,
            GameController gameController)
        {
            _uiService = uiService;
            _developSettings = developSettings;
            _gameController = gameController;
        }
        
        private void OnPauseGameButtonClickEvent()
        {
            // TODO: add pause logic
        }

        private void Update()
        {
            if (!_developSettings.IsOn)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                OnPauseGameButtonClickEvent();
            }
        }
    }
}