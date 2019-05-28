using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    public class GameHUD : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI fruitText, knivesText, stageText;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        void OnFruitUpdate(int fruitCount)
        {
            fruitText.SetText(fruitCount.ToString());
        }

        void OnKnivesUpdate(int knivesCount)
        {
            knivesText.SetText(knivesCount.ToString());
        }

        void OnStageUpdate(int stageCount)
        {
            stageText.SetText($"Stage {stageCount}");
        }
    }
}
