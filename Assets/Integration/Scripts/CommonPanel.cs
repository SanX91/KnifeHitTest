using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    public class CommonPanel : UIPanel
    {
        [SerializeField]
        private TextMeshProUGUI currencyText;

        public override void Close() { }

        private void OnEnable()
        {
            EventManager.Instance.AddListener<CurrencyUpdateEvent>(OnCurrencyUpdate);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<CurrencyUpdateEvent>(OnCurrencyUpdate);
        }

        void OnCurrencyUpdate(CurrencyUpdateEvent evt)
        {
            currencyText.SetText(evt.GetData().ToString());
        }
    } 
}
