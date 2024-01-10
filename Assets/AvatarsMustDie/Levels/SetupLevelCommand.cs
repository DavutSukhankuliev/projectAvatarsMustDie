using System;
using AvatarsMustDie.Wave;
using TMPro;
using UnityEngine;
using VGCore;
using Zenject;

namespace AvatarsMustDie.Levels
{
    public class SetupLevelCommand : Command
    {
        private readonly LevelConfig _levelConfig;
        private readonly IInstantiator _instantiator;
        private readonly LevelController _levelController;

        public SetupLevelCommand(
            LevelConfig levelConfig,
            IInstantiator instantiator,
            CommandStorage commandStorage,
            LevelController levelController) 
            : base(commandStorage)
        {
            _levelConfig = levelConfig;
            _instantiator = instantiator;
            _levelController = levelController;
        }

        public override CommandResult Execute()
        {
            var view = _instantiator.InstantiatePrefabResourceForComponent<LevelView>(_levelConfig.LevelObjSource);
            _levelController.StartLevel(view, _levelConfig);
            
            Done?.Invoke(this, EventArgs.Empty);
            return base.Execute();
        }
    }
}