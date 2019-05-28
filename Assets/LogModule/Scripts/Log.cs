using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Log : MonoBehaviour, ISpriteLoader, ILogData
    {
        [SerializeField]
        private new SpriteRenderer renderer;
        [SerializeField]
        private StageSettings stageSettings;
        [SerializeField]
        private PooledAttachableFactory fruitFactory, knifeFactory;
        [SerializeField]
        float radius, explosionForce;

        private IRotation rotation;
        private IAttacher attacher;
        private IBreaker breaker;

        public float Radius => radius;

        public float ExplosionForce => explosionForce;

        public float ExplosionRadius => radius * 2;

        public void UpdateSprite(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        private void Start()
        {
            attacher = new LogAttacher(transform, radius);
            attacher.AttachItems(fruitFactory, stageSettings.LogSettings.FruitAngles, -1);
            attacher.AttachItems(knifeFactory, stageSettings.LogSettings.KnifeAngles);

            rotation = new CurveRotation(this, stageSettings.LogSettings.RotationSettings);
            rotation.ToggleRotate(true);

            //StartCoroutine(Break());
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Attachable attachable = collider.GetComponent<Attachable>();
            if (attachable == null)
            {
                return;
            }

            attachable.StopMotion();
            attacher.AttachItem(attachable);
        }

        IEnumerator Break()
        {
            yield return new WaitForSeconds(5);
            breaker = new LogBreaker(renderer, attacher.AttachedItems, this);
            breaker.Break();
        }
    } 
}
