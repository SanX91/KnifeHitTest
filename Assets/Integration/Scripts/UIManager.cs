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

        UIPanel currentPanel;

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

            OpenPanel<CommonPanel>();
            OpenPanel<MenuPanel>();
        }

        public void OpenPanel<T>() where T : UIPanel
        {
            if(currentPanel != null)
            {
                currentPanel.Close();
            }

            currentPanel = GetPanel<T>();
            currentPanel.Open();
        }
    } 
}
