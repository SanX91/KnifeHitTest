using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class GameSetup : MonoBehaviour
    {
        [SerializeField]
        UIManager uiManager;
        [SerializeField]
        Log log;
        [SerializeField]
        KnifeThrow knifeThrow;
        [SerializeField]
        StageSetup stageSetup;

        Persistence persistence;
        ResourceData resourceData;

        const string ResourceSaveKey = "ResourceSave";

        private void OnEnable()
        {
            EventManager.Instance.AddListener<GameStartEvent>(OnGameStart);
            EventManager.Instance.AddListener<GameOverEvent>(OnGameOver);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<GameStartEvent>(OnGameStart);
            EventManager.Instance.RemoveListener<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent evt)
        {
            uiManager.OpenPanel<MenuPanel>();
            log.Reset();
            knifeThrow.Reset();
        }

        private void OnGameStart(GameStartEvent evt)
        {
            uiManager.OpenPanel<GameHUD>();
            stageSetup.Initialize();
        }

        private IEnumerator Start()
        {
            persistence = new Persistence();
            resourceData = persistence.LoadData<ResourceData>(ResourceSaveKey);

            if(resourceData == null)
            {
                resourceData = new ResourceData();
            }

            uiManager = Instantiate(uiManager);     
            yield return null;
            log = Instantiate(log);
            yield return null;
            knifeThrow = Instantiate(knifeThrow);
            yield return null;
            stageSetup = Instantiate(stageSetup);

            EventManager.Instance.TriggerEvent(new CurrencyUpdateEvent(resourceData.Currency));
        }

        void SaveResourceData()
        {
            persistence.SaveData(ResourceSaveKey, resourceData);
        }
    } 
}
