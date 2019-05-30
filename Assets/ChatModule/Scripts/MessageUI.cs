using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace ChatSystem
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField]
        RectTransform rectTransform;
        [SerializeField]
        MessageBox userBox, friendBox;
        [SerializeField]
        int maxBoxWidth = 900;

        private void Start()
        {
            Initialize();
        }

        public void SetText(string message, bool isUser = false)
        {
            StartCoroutine(SetTextRoutine(message, isUser));
        }

        IEnumerator SetTextRoutine(string message, bool isUser = false)
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

        void Initialize()
        {
            SetBox(userBox);
            SetBox(friendBox);
        }

        void SetBox(MessageBox messageBox)
        {
            messageBox.Box.gameObject.SetActive(false);
            messageBox.SizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        void RestrictBoxSizes(MessageBox messageBox)
        {
            Vector2 boxSize = messageBox.Box.sizeDelta;
            if (boxSize.x > maxBoxWidth)
            {
                messageBox.SizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                messageBox.Box.sizeDelta = new Vector2(maxBoxWidth, boxSize.y);
            }

            StartCoroutine(ResizeRectTransform(messageBox.Box));
        }

        IEnumerator ResizeRectTransform(RectTransform box)
        {
            yield return null;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, box.sizeDelta.y);
            yield return null;
            ToggleBoxShow(box);
        }

        void ToggleBoxShow(RectTransform box, bool canShow = true)
        {
            box.localScale = canShow?Vector3.one:Vector3.zero;
        }
    } 

    [Serializable]
    public class MessageBox
    {
        [SerializeField]
        ContentSizeFitter sizeFitter;
        [SerializeField]
        RectTransform box;
        [SerializeField]
        TextMeshProUGUI text;

        public ContentSizeFitter SizeFitter => sizeFitter;
        public RectTransform Box => box;
        public TextMeshProUGUI Text => text; 
    }
}
