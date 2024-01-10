using AvatarsMustDie.Application.Installers;
using AvatarsMustDie.Levels;
using UnityEngine;
using VGUIService;
using Zenject;

namespace AvatarsMustDie.UI
{
    public class UIStartMenuWindow : UISimpleWindow
    {
        [SerializeField] private UIButton resumeGameButton;
        [SerializeField] private UIButton startGameButton;
        [SerializeField] private UIButton tutorialButton;
        [SerializeField] private UIButton statisticButton;
        
        private IUIService _uiService;
        private DevelopSettings _developSettings;
        private GameController _gameController;
        
        public override void Show()
        {
            base.Show();
            
            resumeGameButton.OnClickEvent += OnResumeGameButtonClickEvent;
            startGameButton.OnClickEvent += OnStartGameButtonClickEvent;
            tutorialButton.OnClickEvent += OnTutorButtonClickEvent;
            statisticButton.OnClickEvent += OnStatisticsButtonClickEvent;
            
            resumeGameButton.SetActive(false);
            tutorialButton.SetActive(false);
            statisticButton.SetActive(false);
        }

        public override void Hide()
        {
            base.Hide();
            
            resumeGameButton.OnClickEvent -= OnResumeGameButtonClickEvent;
            startGameButton.OnClickEvent -= OnStartGameButtonClickEvent;
            tutorialButton.OnClickEvent -= OnTutorButtonClickEvent;
            statisticButton.OnClickEvent -= OnStatisticsButtonClickEvent;
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
        
        private void OnResumeGameButtonClickEvent()
        {
            // TODO: add logic after adding save progress
        }
        
        private void OnStartGameButtonClickEvent()
        {
            _uiService.Hide<UIStartMenuWindow>();
            
            _gameController.StartGame();
        }

        private void OnTutorButtonClickEvent()
        {
            // TODO: add video
        }

        private void OnStatisticsButtonClickEvent()
        {
            // TODO: add statistics
        }

        private void Update()
        {
            if (!_developSettings.IsOn)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                startGameButton.OnClick();
            }
        }
    }
}
