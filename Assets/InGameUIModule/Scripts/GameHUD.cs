using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Game HUD class.
    /// Responsible for displaying the stage number, the score and the knives remaining to the user.
    /// </summary>
    public class GameHUD : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI knivesText, stageText, scoreText;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<ScoreUpdateEvent>(OnScoreUpdate);
            EventManager.Instance.AddListener<StageIdEvent>(OnStageIdUpdate);
            EventManager.Instance.AddListener<KnivesUpdateEvent>(OnKnivesUpdate);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<ScoreUpdateEvent>(OnScoreUpdate);
            EventManager.Instance.RemoveListener<StageIdEvent>(OnStageIdUpdate);
            EventManager.Instance.RemoveListener<KnivesUpdateEvent>(OnKnivesUpdate);
        }

        private void OnScoreUpdate(ScoreUpdateEvent evt)
        {
            scoreText.SetText($"Score: {evt.GetData()}");
        }

        private void OnStageIdUpdate(StageIdEvent evt)
        {
            stageText.SetText($"Stage {evt.GetData()}");
        }

        private void OnKnivesUpdate(KnivesUpdateEvent evt)
        {
            knivesText.SetText(evt.GetData().ToString());
        }
    }
}
