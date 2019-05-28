using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Log : MonoBehaviour, ISpriteLoader
    {
        [SerializeField]
        private new SpriteRenderer renderer;
        [SerializeField]
        private LogSettings logSettings;
        [SerializeField]
        private PooledAttachableFactory fruitFactory, knifeFactory;
        [SerializeField]
        float radius = 2f;

        private IRotation rotation;
        private IAttacher attacher;

        public void UpdateSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        private void Start()
        {
            attacher = new LogAttacher(transform, radius);
            attacher.AttachItems(fruitFactory, logSettings.FruitAngles, -1);
            attacher.AttachItems(knifeFactory, logSettings.KnifeAngles);

            rotation = new CurveRotation(this, logSettings.RotationSettings);
            rotation.ToggleRotate(true);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Attachable attachable = collider.GetComponent<Attachable>();
            if (attachable == null)
            {
                return;
            }

            attacher.AttachItem(attachable);
        }
    } 
}
