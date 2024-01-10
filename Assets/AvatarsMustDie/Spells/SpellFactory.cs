using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Spells
{
    public class SpellFactory: IFactory<Object, Spell>
    {
        private readonly DiContainer _container;
        
        public SpellFactory(DiContainer container)
        {
            _container = container;
        }

        public Spell Create(Object obj)
        {
            var spell = _container.InstantiatePrefabForComponent<Spell>(obj);
            spell.Cast();
            return spell;
        }
    }
}