using TMPro;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Common UI Panel class.
    /// This panel is always switched on.
    /// Currently it displays the total fruits (in game currency) collected.
    /// </summary>
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

        private void OnCurrencyUpdate(CurrencyUpdateEvent evt)
        {
            currencyText.SetText(evt.GetData().ToString());
        }
    }
}
