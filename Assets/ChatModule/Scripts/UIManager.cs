using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChatSystem
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        List<UIPanel> uiPanels;

        UIPanel currentPanel;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<ClickFriendEvent>(OnClickFriend);
            EventManager.Instance.AddListener<ExitMessagePanelEvent>(OnExitMessagePanel);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<ClickFriendEvent>(OnClickFriend);
            EventManager.Instance.RemoveListener<ExitMessagePanelEvent>(OnExitMessagePanel);
        }

        private void OnExitMessagePanel(ExitMessagePanelEvent evt)
        {
            OpenPanel<FriendsPanel>();
        }

        private void OnClickFriend(ClickFriendEvent evt)
        {
            OpenPanel<MessagePanel>(evt.GetData());
        }


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
            foreach (var panel in uiPanels)
            {
                panel.Close();
            }

            OpenPanel<FriendsPanel>();
        }

        public void OpenPanel<T>(object param = null) where T : UIPanel
        {
            if (currentPanel != null)
            {
                currentPanel.Close();
            }

            currentPanel = GetPanel<T>();
            currentPanel.Open(param);
        }
    } 
}
