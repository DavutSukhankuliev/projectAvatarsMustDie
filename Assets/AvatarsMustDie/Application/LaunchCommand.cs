using AvatarsMustDie.Player;
using AvatarsMustDie.UI;
using UnityEngine;
using VGCore;
using VGUIService;
using Zenject;
using Command = VGCore.Command;

namespace AvatarsMustDie.Application
{
    public class LaunchCommand : Command
    {
        private readonly IInstantiator _instantiator;
        private readonly SceneHolder _sceneHolder;
        private readonly IUIService _uiService;

        private Vector3 _gameCanvasPosition = new (0f, 0f, 0.2f);
        
        public LaunchCommand(
            IInstantiator instantiator,
            SceneHolder sceneHolder,
            IUIService uiService,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _instantiator = instantiator;
            _sceneHolder = sceneHolder;
            _uiService = uiService;
        }

        public override CommandResult Execute()
        {
            var player = _instantiator.InstantiatePrefabResourceForComponent<PlayerView>("PlayerView");
            _sceneHolder.Add<PlayerView>(player);

            var uiGameCanvas = _instantiator.InstantiatePrefabResourceForComponent<UIGameCanvas>("UIGameCanvas");
            uiGameCanvas.transform.position = _gameCanvasPosition;
            
            _sceneHolder.Add<UIGameCanvas>(uiGameCanvas);

            _uiService.Show<UIStartMenuWindow>(uiGameCanvas.transform);

            return base.Execute();
        }
    }
}