using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Log Piece class.
    /// Implements the ILogData and ILogPiece interfaces.
    /// This class is responsible for initializing all the sub systems of the log piece.
    /// </summary>
    public class LogPiece : MonoBehaviour, ILogData, ILogPiece
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
        private float radius, explosionForce;

        private IRotation rotation;
        private IAttacher attacher;
        private IBreaker breaker;
        private const string AttachedLayer = "NonThrowables";

        public float Radius => radius;

        public float ExplosionForce => explosionForce;

        public Collider2D Collider => collider;

        public SpriteRenderer Renderer => renderer;

        public Vector3 Position => transform.position;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.AddListener<StageSuccessEvent>(OnStageSuccess);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.RemoveListener<StageSuccessEvent>(OnStageSuccess);
        }

        private void OnStageSuccess(StageSuccessEvent obj)
        {
            rotation.ToggleRotate(false);
            StartCoroutine(Break());
        }

        /// <summary>
        /// Attaches all the items from the various Attachable factories to the log piece based on certain settings.
        /// Initializes the rotation of the log piece.
        /// </summary>
        /// <param name="evt"></param>
        private void OnStageStart(StageStartEvent evt)
        {
            IStageSettings stageSettings = (IStageSettings)evt.GetData();

            attacher = new LogAttacher(gameObject, radius);
            attacher.AttachItems(fruitFactory, stageSettings.LogSettings.FruitAngles, true);
            attacher.AttachItems(knifeFactory, stageSettings.LogSettings.KnifeAngles);

            rotation = new CurveRotation(this, rigidbody, stageSettings.LogSettings.RotationSettings);
            rotation.ToggleRotate(true);
        }

        /// <summary>
        /// Instantiates the factories for generating fruits and knives, which are attachable to the log piece.
        /// </summary>
        private void Start()
        {
            fruitFactory = Instantiate(fruitFactory);
            knifeFactory = Instantiate(knifeFactory);
        }

        /// <summary>
        /// Triggered when an Attachable of type ThrowableKnife enters the trigger.
        /// Updates the layer of the Attachable to a layer which cannot interact with the layer of this gameobject.
        /// Updates the Attachable rigidbody to kinematic type, to avoid any unwanted physics behaviours.
        /// Finally the attacher attaches the item to the log piece, so that it can rotate along with the log piece.
        /// </summary>
        /// <param name="collider"></param>
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

        private IEnumerator Break()
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

            if (breaker != null)
            {
                breaker.Reset();
            }
        }
    }
}
