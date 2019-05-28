using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnifeHitTest
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        List<UIPanel> uiPanels;

        /// <summary>
        /// Gets any panel derived from UIPanel class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetPanel<T>() where T : UIPanel
        {
            return (T)uiPanels.Single(x => x.GetType() == typeof(T));
        }

        private void Start()
        {
            foreach(var panel in uiPanels)
            {
                panel.Close();
            }
        }

        public void Initialize()
        {
            GetPanel<MenuPanel>().Open();
        }
    } 
}
