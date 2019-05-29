using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class MenuPanel : UIPanel
    {
        public void OnPlay()
        {
            EventManager.Instance.TriggerEvent(new GameStartEvent());
        }
    } 
}
