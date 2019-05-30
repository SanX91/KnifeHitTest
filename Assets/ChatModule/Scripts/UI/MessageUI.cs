using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChatSystem
{
    /// <summary>
    /// The class responsible for displaying a message in the UI.
    /// </summary>
    public class MessageUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;
        [SerializeField]
        private MessageBox userBox, friendBox;
        [SerializeField]
        private int maxBoxWidth = 900;

        private void Start()
        {
            Initialize();
        }

        public void SetText(string message, bool isUser = false)
        {
            StartCoroutine(SetTextRoutine(message, isUser));
        }

        /// <summary>
        /// Chooses to display separate UI depending on the sender of the message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isUser"></param>
        /// <returns></returns>
        private IEnumerator SetTextRoutine(string message, bool isUser = false)
        {
            Initialize();
            yield return null;

            if (isUser)
            {
                ToggleBoxShow(userBox.Box, false);
                userBox.Box.gameObject.SetActive(true);
                userBox.Text.SetText(message);
                yield return null;

                RestrictBoxSizes(userBox);
                yield break;
            }

            ToggleBoxShow(friendBox.Box, false);
            friendBox.Box.gameObject.SetActive(true);
            friendBox.Text.SetText(message);
            yield return null;

            RestrictBoxSizes(friendBox);
        }

        private void Initialize()
        {
            SetBox(userBox);
            SetBox(friendBox);
        }

        private void SetBox(MessageBox messageBox)
        {
            messageBox.Box.gameObject.SetActive(false);
            messageBox.SizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        /// <summary>
        /// Allows horizontal autosizing of the message box upto a certain width.
        /// </summary>
        /// <param name="messageBox"></param>
        private void RestrictBoxSizes(MessageBox messageBox)
        {
            Vector2 boxSize = messageBox.Box.sizeDelta;
            if (boxSize.x > maxBoxWidth)
            {
                messageBox.SizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                messageBox.Box.sizeDelta = new Vector2(maxBoxWidth, boxSize.y);
            }

            StartCoroutine(ResizeRectTransform(messageBox.Box));
        }

        /// <summary>
        /// Resizing the rect transform attached to this gameobject, based on the size of it's message box child.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private IEnumerator ResizeRectTransform(RectTransform box)
        {
            yield return null;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, box.sizeDelta.y);
            yield return null;
            ToggleBoxShow(box);
        }

        /// <summary>
        /// Scaling to show/hide the message boxes.
        /// This is done to avoid any jerkiness while generating the message boxes.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="canShow"></param>
        private void ToggleBoxShow(RectTransform box, bool canShow = true)
        {
            box.localScale = canShow ? Vector3.one : Vector3.zero;
        }
    }

    /// <summary>
    /// The MessageBox class.
    /// Stores the different sub components of a chat message box.
    /// </summary>
    [Serializable]
    public class MessageBox
    {
        [SerializeField]
        private ContentSizeFitter sizeFitter;
        [SerializeField]
        private RectTransform box;
        [SerializeField]
        private TextMeshProUGUI text;

        public ContentSizeFitter SizeFitter => sizeFitter;
        public RectTransform Box => box;
        public TextMeshProUGUI Text => text;
    }
}
