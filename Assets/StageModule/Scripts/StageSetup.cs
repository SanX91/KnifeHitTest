using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Stage setup class.
    /// Loads the stage settings for each stage, and triggers the stage start event.
    /// Currently loads the stage settings from the Resources folder.
    /// </summary>
    public class StageSetup : MonoBehaviour
    {
        private int stageId = 1;
        private const string path = "Stages/";
        private const string stageName = "Stage_";
        private IStageSettings stageSettings;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<StageEndEvent>(OnStageEndEvent);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<StageEndEvent>(OnStageEndEvent);
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

        /// <summary>
        /// Loads the stage settings asynchronously from the Resources folder with the path name.
        /// On successfully loading the settings, triggers the stage start event.
        /// If there are no stages found with the current stageId, then game over is triggered.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadStageAsync()
        {
            ResourceRequest request = Resources.LoadAsync<StageSettings>(path + stageName + stageId);
            while (!request.isDone)
            {
                yield return null;
            }

            if (request.asset == null)
            {
                EventManager.Instance.TriggerEvent(new GameOverEvent());
                yield break;
            }

            stageSettings = (IStageSettings)Instantiate(request.asset);
            EventManager.Instance.TriggerEvent(new StageStartEvent(stageSettings));
            EventManager.Instance.TriggerEvent(new StageIdEvent(stageId));
        }

        public void Reset()
        {
            stageId = 1;
        }
    }
}