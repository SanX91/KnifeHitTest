using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ChatSystem
{
    public class FriendsPanel : UIPanel
    {
        [SerializeField]
        FriendMaker dummyFriends;
        [SerializeField]
        FriendUI friendUIPrefab;
        [SerializeField]
        RectTransform content;

        List<FriendUI> friendUIList;

        private void Start()
        {
            CreateFriendList();
        }

        void CreateFriendList()
        {
            dummyFriends = Instantiate(dummyFriends);
            IEnumerable<FriendData> friends = dummyFriends.Friends.OrderBy(x => x.Name);

            friendUIList = new List<FriendUI>();

            foreach (var friend in friends)
            {
                FriendUI friendUI = Instantiate(friendUIPrefab, content);
                friendUI.Initialize(friend);
                friendUIList.Add(friendUI);
            }
        }

    } 
}
