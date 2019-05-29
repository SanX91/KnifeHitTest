using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class StageSetup : MonoBehaviour
    {
        int stageId = 1;
        const string path = "Stages/";
        const string stageName = "Stage_";

        IStageSettings stageSettings;

        private void OnEnable()
        {           
            EventManager.Instance.AddListener<StageEndEvent>(OnStageEndEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<StageEndEvent>(OnStageEndEvent);
        }

        private void OnTurnEndEvent(TurnEndEvent obj)
        {
            throw new NotImplementedException();
        }

        private void OnStageEndEvent(StageEndEvent evt)
        {
            stageId++;
            Initialize();
        }

        // Start is called before the first frame update
        public void Initialize()
        {
            StartCoroutine(LoadStageAsync());
        }

        IEnumerator LoadStageAsync()
        {
            ResourceRequest request = Resources.LoadAsync<StageSettings>(path + stageName + stageId);
            while(!request.isDone)
            {
                yield return null;
            }

            if(request.asset == null)
            {
                Debug.LogWarning("Stage not found, triggering game over!");
                yield break;
            }

            stageSettings = (IStageSettings)Instantiate(request.asset);
            EventManager.Instance.TriggerEvent(new StageStartEvent(stageSettings));
            EventManager.Instance.TriggerEvent(new StageIdEvent(stageId));
        }
    } 
}