using UnityEngine;
using VGBootstrapService;
using Zenject;

namespace AvatarsMustDie.Application
{
    public class ApplicationLauncher
    {
        public ApplicationLauncher(IBootstrap bootstrap, IInstantiator instantiator)
        {
            bootstrap.ProgressUpdate += (sender, f) =>
            {
                Debug.Log($"Progress : {Mathf.Min(Mathf.Floor(f * 100f), 100)}");
            };

            bootstrap.AddCommand(instantiator.Instantiate<UIServiceInitCommand>());
            bootstrap.AddCommand(instantiator.Instantiate<LaunchCommand>());
            
            bootstrap.StartExecute();
        }
    }
}
