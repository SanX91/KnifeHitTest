using UnityEngine;

namespace ChatSystem
{
    /// <summary>
    /// The base UI panel class.
    /// </summary>
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void Open(object param = null)
        {
            gameObject.SetActive(true);
        }
    }
}
