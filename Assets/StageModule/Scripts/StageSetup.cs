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

        // Start is called before the first frame update
        IEnumerator Start()
        {
            StartCoroutine(LoadStageAsync());

            EventManager.Instance.AddListener<StageStartEvent>(OnStageStart);
            EventManager.Instance.AddListener<StageStartEvent>(OnStageStartV2);

            //yield return new WaitForSeconds(2);

            EventManager.Instance.TriggerEvent(new StageStartEvent(new TestStageSettings()));

            yield return new WaitForSeconds(2);

            EventManager.Instance.TriggerEvent(new StageStartEvent(new TestStageSettings()));

            yield return new WaitForSeconds(2);

            EventManager.Instance.RemoveListener<StageStartEvent>(OnStageStart);
            //EventManager.GetInstance().RemoveListener<StageStartEvent>(OnStageStartV2);

            EventManager.Instance.TriggerEvent(new StageStartEvent(new TestStageSettings()));
        }

        void OnStageStart(StageStartEvent stageStartEvent)
        {
            IStageSettings stageSettings = (IStageSettings)stageStartEvent.GetData();
            Debug.Log(stageSettings.Knives);
        }

        void OnStageStartV2(StageStartEvent stageStartEvent)
        {
            IStageSettings stageSettings = (IStageSettings)stageStartEvent.GetData();
            Debug.Log($"{stageSettings.Knives} knives up yours!");
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator LoadStageAsync()
        {
            ResourceRequest request = Resources.LoadAsync<StageSettings>(path + stageName + stageId);
            while(!request.isDone)
            {
                yield return null;
            }

            stageSettings = (IStageSettings)Instantiate(request.asset);
            //Debug.Log(stageSettings.Knives);
        }
    } 
}