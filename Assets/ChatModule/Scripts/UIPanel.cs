using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSystem
{
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
