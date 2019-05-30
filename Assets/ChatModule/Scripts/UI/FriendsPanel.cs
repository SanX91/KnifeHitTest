using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChatSystem
{
    /// <summary>
    /// A class which creates the friend list from a list of friend data.
    /// Currently doesn't implement a table view for best performance.
    /// </summary>
    public class FriendsPanel : UIPanel
    {
        [SerializeField]
        private FriendMaker dummyFriends;
        [SerializeField]
        private FriendUI friendUIPrefab;
        [SerializeField]
        private RectTransform content;
        private List<FriendUI> friendUIList;

        private void Start()
        {
            CreateFriendList();
        }

        private void CreateFriendList()
        {
            dummyFriends = Instantiate(dummyFriends);
            IEnumerable<FriendData> friends = dummyFriends.Friends.OrderBy(x => x.Name);

            friendUIList = new List<FriendUI>();

            foreach (FriendData friend in friends)
            {
                FriendUI friendUI = Instantiate(friendUIPrefab, content);
                friendUI.Initialize(friend);
                friendUIList.Add(friendUI);
            }
        }
    }
}
