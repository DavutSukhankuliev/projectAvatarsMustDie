using System;
using VGCore;
using VGUIService;
using Command = VGCore.Command;

namespace AvatarsMustDie.Application
{
    public class UIServiceInitCommand : Command
    {
        private readonly IUIService _uiService;

        private const string WindowsSource = "UIWindows";
        
        public UIServiceInitCommand(
            IUIService uiService,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _uiService = uiService;
        }

        public override CommandResult Execute()
        {
            _uiService.LoadWindows(WindowsSource);
            _uiService.InitWindows();

            Done?.Invoke(this, EventArgs.Empty);
            return base.Execute(); 
        }
    }
}