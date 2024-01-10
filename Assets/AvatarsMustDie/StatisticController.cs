using AvatarsMustDie.Application;
using AvatarsMustDie.Levels;
using AvatarsMustDie.Levels.Realization;
using AvatarsMustDie.UI;
using UniRx;
using VGUIService;

namespace AvatarsMustDie
{
    public class StatisticController
    {
        private readonly IUIService _uiService;
        private readonly SceneHolder _sceneHolder;

        private const float StatisticsDisplayTime = 10f;
        
        public StatisticController(
            IUIService uiService,
            SceneHolder sceneHolder)
        {
            _uiService = uiService;
            _sceneHolder = sceneHolder;

            MessageBroker.Default.Receive<OnEndBiomeMessage>()
                .Subscribe(message => HideStatisticWindow());
        }
        
        public float CalculateAndShowStatisticByTime()
        {
            //TODO: ADD statistic calculaton
            var data = new StatisticData();            
            
            var window = _uiService.Show<UIStatisticWindow>(_sceneHolder.Get<UIGameCanvas>().transform);
            window.SetParameters(data);

            return StatisticsDisplayTime;
        }

        private void HideStatisticWindow()
        {
            _uiService.Hide<UIStatisticWindow>();
        }
    }

    public readonly struct StatisticData
    {
        public readonly string Points;

        public StatisticData(string points)
        {
            Points = points;
        }
    }
}