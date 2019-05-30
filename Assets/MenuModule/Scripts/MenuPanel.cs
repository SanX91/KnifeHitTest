using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
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
