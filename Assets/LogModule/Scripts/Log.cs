using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Log : MonoBehaviour, ILogData, ILogPiece
    {
        [SerializeField]
        private new Rigidbody2D rigidbody;
        [SerializeField]
        private new Collider2D collider;
        [SerializeField]
        private new SpriteRenderer renderer;
        [SerializeField]
        private PooledAttachableFactory fruitFactory, knifeFactory;
        [SerializeField]
        float radius, explosionForce;

        private IRotation rotation;
        private IAttacher attacher;
        private IBreaker breaker;

        const string AttachedLayer = "NonThrowables";

        public float Radius => radius;

        public float ExplosionForce => explosionForce;

        public Collider2D Collider => collider;

        public SpriteRenderer Renderer => renderer;

        public Vector3 Position => transform.position;

        void OnEnable()
        {
            EventManager.Instance.AddListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.AddListener<StageSuccessEvent>(OnStageSuccess);
        }

        void OnDisable()
        {
            EventManager.Instance.RemoveListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.RemoveListener<StageSuccessEvent>(OnStageSuccess);
        }

        private void OnStageSuccess(StageSuccessEvent obj)
        {
            rotation.ToggleRotate(false);
            StartCoroutine(Break());
        }

        private void OnStageStart(StageStartEvent evt)
        {
            IStageSettings stageSettings = (IStageSettings)evt.GetData();

            attacher = new LogAttacher(gameObject, radius);
            attacher.AttachItems(fruitFactory, stageSettings.LogSettings.FruitAngles, true);
            attacher.AttachItems(knifeFactory, stageSettings.LogSettings.KnifeAngles);

            rotation = new CurveRotation(this, rigidbody, stageSettings.LogSettings.RotationSettings);
            rotation.ToggleRotate(true);
        }

        void Start()
        {
            fruitFactory = Instantiate(fruitFactory);
            knifeFactory = Instantiate(knifeFactory);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Attachable attachable = collider.GetComponent<Attachable>();
            if (attachable == null)
            {
                return;
            }

            attachable.UpdateLayer(AttachedLayer);
            attachable.AdjustRigidbodyType(RigidbodyType2D.Kinematic);
            attacher.AttachItem(attachable);

            EventManager.Instance.TriggerEvent(new TurnEndEvent());
        }

        IEnumerator Break()
        {
            breaker = new LogBreaker(this, attacher.AttachedItems, this);
            breaker.Break();
            yield return new WaitForSeconds(1);
            EventManager.Instance.TriggerEvent(new StageEndEvent());
        }

        public void Reset()
        {
            rotation.ToggleRotate(false);
            attacher.Reset();
            fruitFactory.Reset();
            knifeFactory.Reset();

            if(breaker!=null)
            {
                breaker.Reset();
            }          
        }
    } 
}
