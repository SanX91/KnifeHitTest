using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The UI manager class.
    /// Responsible for initializing and toggling of UI panels.
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private List<UIPanel> uiPanels;
        private UIPanel currentPanel;

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
            foreach (UIPanel panel in uiPanels)
            {
                panel.Close();
            }

            OpenPanel<CommonPanel>();
            OpenPanel<MenuPanel>();
        }

        public void OpenPanel<T>() where T : UIPanel
        {
            if (currentPanel != null)
            {
                currentPanel.Close();
            }

            currentPanel = GetPanel<T>();
            currentPanel.Open();
        }
    }
}
