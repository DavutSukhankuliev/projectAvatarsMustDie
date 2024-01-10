using UnityEngine;
using Zenject;

namespace AvatarsMustDie.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        public abstract void Cast();
        
        public class Factory : PlaceholderFactory<Object, Spell>
        { }
    }
}