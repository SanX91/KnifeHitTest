using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatSystem
{
    /// <summary>
    /// The class responsible for displaying a friend in the UI.
    /// </summary>
    public class FriendUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameText;
        [SerializeField]
        private Image pic;
        private FriendData friendData;

        public void Initialize(FriendData data)
        {
            friendData = data;
            SetName(data.Name);
            SetPic(data.ImageURL);
        }

        private void SetName(string name)
        {
            nameText.SetText(name);
        }

        private void SetPic(string url)
        {
            //Get pic from url
        }

        public void OnClick()
        {
            EventManager.Instance.TriggerEvent(new ClickFriendEvent(friendData));
        }
    }
}
