using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChatSystem
{
    public class FriendUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI nameText;
        [SerializeField]
        Image pic;

        FriendData friendData;

        public void Initialize(FriendData data)
        {
            friendData = data;
            SetName(data.Name);
            SetPic(data.ImageURL);
        }

        void SetName(string name)
        {
            nameText.SetText(name);
        }

        void SetPic(string url)
        {
            //Get from url
        }

        public void OnClick()
        {
            EventManager.Instance.TriggerEvent(new ClickFriendEvent(friendData));
        }
    } 
}
