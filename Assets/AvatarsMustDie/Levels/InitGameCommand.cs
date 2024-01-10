using System;
using AvatarsMustDie.Application;
using AvatarsMustDie.PoseDetection;
using AvatarsMustDie.Spells;
using AvatarsMustDie.UI;
using VGCore;
using VGUIService;
using Zenject;
using Command = VGCore.Command;

namespace AvatarsMustDie.Levels
{
    public class InitGameCommand : Command
    {
        private readonly SceneHolder _sceneHolder;
        private readonly IInstantiator _instantiator;
        private readonly PoseDetectionController _poseDetectionController;
        private readonly IUIService _uiService;

        public InitGameCommand( 
            SceneHolder sceneHolder,
            IInstantiator instantiator,
            PoseDetectionController poseDetectionController,           
            IUIService uiService,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _sceneHolder = sceneHolder;
            _instantiator = instantiator;
            _poseDetectionController = poseDetectionController;
            _uiService = uiService;
        }

        public override CommandResult Execute()
        {
            var poseDetectionSceneObject = _instantiator.InstantiatePrefabResourceForComponent<PoseDetectionView>("PoseDetectionView");
            _sceneHolder.Add<PoseDetectionView>(poseDetectionSceneObject);                        
            
            _poseDetectionController.SetupPoseDetection();

            var uiGameCanvas = _sceneHolder.Get<UIGameCanvas>();
            
            _uiService.Show<UIInGameHUD>(uiGameCanvas.transform);

            Done?.Invoke(this, EventArgs.Empty);
            
            return base.Execute();
        }
    }
}