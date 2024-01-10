using System;
using System.Threading.Tasks;
using AvatarsMustDie.Wave;

namespace AvatarsMustDie.Levels
{
    public interface IBiomeState
    {
        void Init(BiomeConfig biomeConfig);
        Task OnEntry();
        Task OnExit();
    }
}