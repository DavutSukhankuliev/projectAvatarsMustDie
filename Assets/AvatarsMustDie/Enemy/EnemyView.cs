using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Enemy
{
    public readonly struct EnemyViewProtocol
    {
        public readonly GameObject MeshPrefab;
        public readonly Avatar Avatar;

        public EnemyViewProtocol(
            GameObject meshPrefab,
            Avatar avatar)
        {
            MeshPrefab = meshPrefab;
            Avatar = avatar;
        }
    }

    public class EnemyView : MonoBehaviour
    {
        public Transform BodyTransform => bodyTransform;
        public Animator CharacterAnimator => charAnimator;
        public HealthbarView HealthBar => hpBar;

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Animator charAnimator;
        [SerializeField] private HealthbarView hpBar;

        private GameObject _enemyObj;

        [Inject]
        public void Constructor(EnemyViewProtocol protocol)
        {
            _enemyObj = Instantiate(protocol.MeshPrefab);
            _enemyObj.transform.SetParent(transform);
            _enemyObj.transform.localPosition = Vector3.zero;
            charAnimator.avatar = protocol.Avatar;
        }
        
        public class Factory : PlaceholderFactory<EnemyViewProtocol, EnemyView>
        {

        }
    }
}
