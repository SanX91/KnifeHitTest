using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// Implements the IController interface.
    /// Uses mouse controls.
    /// </summary>
    public class MouseController : IController
    {
        public bool HasTapped()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }
    }
}
