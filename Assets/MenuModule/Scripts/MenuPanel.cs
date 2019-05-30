using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The menu panel.
    /// The panel which contains the play button, and displays the high score.
    /// </summary>
    public class MenuPanel : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI highScoreText;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<HighScoreUpdateEvent>(OnHigScoreUpdate);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<HighScoreUpdateEvent>(OnHigScoreUpdate);
        }

        private void OnHigScoreUpdate(HighScoreUpdateEvent evt)
        {
            highScoreText.SetText($"High Score: {evt.GetData()}");
        }

        public void OnPlay()
        {
            EventManager.Instance.TriggerEvent(new GameStartEvent());
        }
    }
}
