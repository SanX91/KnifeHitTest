using System.Collections.Generic;
using UnityEngine;

namespace ChatSystem
{
    /// <summary>
    /// A class for making dummy friend data.
    /// </summary>
    [CreateAssetMenu(fileName = "FriendMaker", menuName = "Chat System/Friend Maker")]
    public class FriendMaker : ScriptableObject
    {
        [SerializeField]
        private List<FriendData> friends;

        public IEnumerable<FriendData> Friends => friends;
    }
}
