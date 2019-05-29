using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class KnifeThrow : MonoBehaviour
    {
        [SerializeField]
        private PooledAttachableFactory knifeFactory;
        [SerializeField]
        Transform knifePlaceholder;

        IController controller;
        int stageKnives;

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

        private void Start()
        {
            controller = new MouseController();
        }

        private void OnTurnEndEvent(TurnEndEvent obj)
        {
            if(stageKnives>0)
            {
                StartCoroutine(StageRoutine());
                return;
            }

            EventManager.Instance.TriggerEvent(new StageSuccessEvent());
        }

        private void OnStageStart(StageStartEvent evt)
        {
            IStageSettings stageSettings = (IStageSettings)evt.GetData();
            stageKnives = stageSettings.Knives;
            EventManager.Instance.TriggerEvent(new KnivesUpdateEvent(stageKnives));
            Initialize();
        }

        private void Initialize()
        {
            StartCoroutine(StageRoutine());
        }

        IEnumerator StageRoutine()
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
