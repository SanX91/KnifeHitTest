using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public interface IEventListener<T> where T:IEvent
    {
        bool HandleEvent(T evt);
    } 
}
