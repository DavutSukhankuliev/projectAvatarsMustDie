using AvatarsMustDie.Application;
using VGCore;
using Zenject;

namespace AvatarsMustDie.Base
{
    public class BaseViewCreateCommand : Command
    {
        private readonly IInstantiator _instantiator;
        private readonly SceneHolder _sceneHolder;
        private readonly BaseConfig _config;
        private readonly string _key;

        public BaseViewCreateCommand(
            CommandStorage commandStorage,
            IInstantiator instantiator,
            SceneHolder sceneHolder,
            BaseConfig config,
            string key
            ) : base(commandStorage)
        {
            _instantiator = instantiator;
            _sceneHolder = sceneHolder;
            _config = config;
            _key = key;
        }

        public override CommandResult Execute()
        {
            var result = base.Execute();
            result.Body = CreateBaseView();
            return result;
        }

        private BaseView CreateBaseView()
        {
            var view = _instantiator.InstantiatePrefabResourceForComponent<BaseView>("BaseView");
            
            view.transform.position = _config.Get(_key).PosOffset;
            view.transform.rotation = _config.Get(_key).RotationOffset;
            view.transform.localScale = _config.Get(_key).Scale;
            view.Hp = _config.Get(_key).Hp;
            
            _sceneHolder.Add<BaseView>(view);
            return view;
        }
    }
}