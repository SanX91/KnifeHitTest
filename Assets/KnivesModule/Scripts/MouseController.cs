using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class MouseController : IController
    {
        public bool HasTapped()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }
    }
}
