using System;
using UnityEngine;

namespace ChatSystem
{
    [Serializable]
    public class FriendData
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private uint userId;
        [SerializeField]
        private string imageURL;

        public string Name => name;
        public uint UserId => userId;
        public string ImageURL => imageURL;
    }
}
