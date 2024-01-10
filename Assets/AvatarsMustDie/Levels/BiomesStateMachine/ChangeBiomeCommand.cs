using AvatarsMustDie.Wave;
using Cysharp.Threading.Tasks;
using VGCore;

namespace AvatarsMustDie.Levels
{
    public class ChangeBiomeCommand : Command
    {
        private readonly BiomesStateMachine _biomesStateMachine;

        public ChangeBiomeCommand(
            BiomesStateMachine biomesStateMachine,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _biomesStateMachine = biomesStateMachine;
        }

        public UniTask Execute(BiomeType type)
        {
            return _biomesStateMachine.ChangeOfState(type);
        }
    }
}