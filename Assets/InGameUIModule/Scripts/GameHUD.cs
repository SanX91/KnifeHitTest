using System;
using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    public class GameHUD : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI knivesText, stageText;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<StageIdEvent>(OnStageIdUpdate);
            EventManager.Instance.AddListener<KnivesUpdateEvent>(OnKnivesUpdate);
        }

        private void OnDisable()
        {
            EventManager.Instance.AddListener<StageIdEvent>(OnStageIdUpdate);
            EventManager.Instance.AddListener<KnivesUpdateEvent>(OnKnivesUpdate);
        }

        private void OnStageIdUpdate(StageIdEvent evt)
        {
            stageText.SetText($"Stage {evt.GetData()}");
        }

        void OnKnivesUpdate(KnivesUpdateEvent evt)
        {
            knivesText.SetText(evt.GetData().ToString());
        }
    }
}
