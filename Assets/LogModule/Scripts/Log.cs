using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Log : MonoBehaviour, ISpriteLoader
    {
        [SerializeField]
        private new SpriteRenderer renderer;
        [SerializeField]
        private RotationSettings rotationSettings;
        private IRotation rotation;

        private List<Attachable> attachedObjects;

        public void UpdateSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        private void Start()
        {
            rotation = new CurveRotation(this, rotationSettings);
            rotation.ToggleRotate(true);

            attachedObjects = new List<Attachable>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Attachable attachable = collider.GetComponent<Attachable>();
            if (attachable == null)
            {
                return;
            }

            attachedObjects.Add(attachable);
        }
    } 
}
