using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public interface IAttacher
    {
        IEnumerable<Attachable> AttachedItems { get; }
        void AttachItem(Attachable item);
        void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, bool isFlipped = false);
        void Reset();
    } 
}
