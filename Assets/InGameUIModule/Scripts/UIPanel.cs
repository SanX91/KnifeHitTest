using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The abstract UI panel class.
    /// Contains default methods for opening and closing the panel.
    /// </summary>
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
    }
}
