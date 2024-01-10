using AvatarsMustDie.Application.Installers;
using AvatarsMustDie.Enemy;
using Zenject;

namespace AvatarsMustDie.Base
{
    public class BaseController
    {
        private readonly DevelopSettings _developSettings;
        private readonly IInstantiator _instantiator;
        
        private BaseView _baseView;

        public BaseController
        (
            DevelopSettings developSettings,
            IInstantiator instantiator
        )
        {
            _developSettings = developSettings;
            _instantiator = instantiator;
        }

        public void SetupBaseView(string key)
        {
            var command = _instantiator.Instantiate<BaseViewCreateCommand>(new []{key});
            _baseView = (BaseView) command.Execute().Body;

            _baseView.EnterTriggerEvent += (sender, collider) =>
            {
                // temporarily made enemyView cast
                EnemyView enemy = collider.transform.GetComponent<EnemyView>();
                // todo: take damage from enemy logic
            };
        }

        public void TakeDamage(float damagePoints)
        {
            _baseView.Hp -= damagePoints;
        }
    }
}