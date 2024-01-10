using AvatarsMustDie.UI;
using TMPro;
using UnityEngine;

namespace AvatarsMustDie.Levels.Realization
{
    public class UIStatisticWindow : UISimpleWindow
    {
        [SerializeField] private TextMeshProUGUI statisticText;

        public override void Show()
        {
            transform.localPosition = Vector3.zero;
            
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void SetParameters(StatisticData data)
        {
            statisticText.SetText(statisticText.text + data.Points);
        }
    }
}
