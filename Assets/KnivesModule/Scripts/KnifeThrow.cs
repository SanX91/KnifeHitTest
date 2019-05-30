using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Knife Throw class.
    /// This class is responsible for handling the knife throwing system of the game.
    /// Also responsible for adjusting the score on every turn end.
    /// </summary>
    public class KnifeThrow : MonoBehaviour
    {
        [SerializeField]
        private int scorePerTurn = 2;
        [SerializeField]
        private float turnEndMultiplier = 1.2f;
        [SerializeField]
        private PooledAttachableFactory knifeFactory;
        [SerializeField]
        private Transform knifePlaceholder;
        private IController controller;
        private ScoreData scoreData;
        private int stageKnives;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.AddListener<TurnEndEvent>(OnTurnEndEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.RemoveListener<TurnEndEvent>(OnTurnEndEvent);
        }

        /// <summary>
        /// Setting up a mouse controller.
        /// </summary>
        private void Start()
        {
            controller = new MouseController();
        }

        /// <summary>
        /// Restarts the stage routine if there are remaining knives.
        /// Otherwise, triggers the stage success event.
        /// Adjusts the game score as well.
        /// </summary>
        /// <param name="evt"></param>
        private void OnTurnEndEvent(TurnEndEvent evt)
        {
            if (stageKnives > 0)
            {
                StartCoroutine(StageRoutine());
                scoreData.AdjustScore(scorePerTurn);
                EventManager.Instance.TriggerEvent(new ScoreUpdateEvent(scoreData.Score));
                return;
            }

            scoreData.AdjustScore(scorePerTurn, turnEndMultiplier);
            EventManager.Instance.TriggerEvent(new ScoreUpdateEvent(scoreData.Score));
            EventManager.Instance.TriggerEvent(new StageSuccessEvent());
        }

        private void OnStageStart(StageStartEvent evt)
        {
            IStageSettings stageSettings = (IStageSettings)evt.GetData();
            stageKnives = stageSettings.Knives;
            EventManager.Instance.TriggerEvent(new KnivesUpdateEvent(stageKnives));
            StartCoroutine(StageRoutine());
        }

        public void Initialize(ScoreData scoreData)
        {
            this.scoreData = scoreData;
        }

        /// <summary>
        /// The stage routine.
        /// Gets a throwable knife from the knifeFactory, and awaits user input to throw it.
        /// </summary>
        /// <returns></returns>
        private IEnumerator StageRoutine()
        {
            Attachable knife = knifeFactory.GetEntity();
            knife.transform.position = knifePlaceholder.position;
            knife.transform.up = knifePlaceholder.up;
            knife.gameObject.SetActive(true);

            while (!controller.HasTapped())
            {
                yield return null;
            }

            IThrowable throwable = (IThrowable)knife;
            throwable.Throw();

            stageKnives--;
            EventManager.Instance.TriggerEvent(new KnivesUpdateEvent(stageKnives));
        }

        public void Reset()
        {
            knifeFactory.Reset();
        }
    }
}
