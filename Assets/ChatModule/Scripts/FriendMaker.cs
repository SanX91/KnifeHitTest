using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSystem
{
    [CreateAssetMenu(fileName = "FriendMaker", menuName = "Chat System/Friend Maker")]
    public class FriendMaker : ScriptableObject
    {
        [SerializeField]
        List<FriendData> friends;

        public IEnumerable<FriendData> Friends => friends;
    }

    [Serializable]
    public class FriendData
    {
        [SerializeField]
        string name;
        [SerializeField]
        uint userId;
        [SerializeField]
        string imageURL;

        public string Name => name;
        public uint UserId => userId;
        public string ImageURL => imageURL;
    }
}
