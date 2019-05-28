using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public interface IAttacher
    {
        void AttachItem(Attachable item);
        void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, float upDir = 1);
    } 
}
