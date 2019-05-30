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
        ScoreData scoreData;

        const string ResourceSaveKey = "ResourceSave";
        const string ScoreSaveKey = "ScoreSave";

        private void OnEnable()
        {
            EventManager.Instance.AddListener<GameStartEvent>(OnGameStart);
            EventManager.Instance.AddListener<GameOverEvent>(OnGameOver);
            EventManager.Instance.AddListener<StageEndEvent>(OnStageEnd);
            EventManager.Instance.AddListener<CurrencyPickupEvent>(OnCurrencyPickup);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<GameStartEvent>(OnGameStart);
            EventManager.Instance.RemoveListener<GameOverEvent>(OnGameOver);
            EventManager.Instance.RemoveListener<StageEndEvent>(OnStageEnd);
            EventManager.Instance.RemoveListener<CurrencyPickupEvent>(OnCurrencyPickup);
        }

        private void OnStageEnd(StageEndEvent evt)
        {
            log.Reset();
            knifeThrow.Reset();
        }

        private void OnCurrencyPickup(CurrencyPickupEvent evt)
        {
            resourceData.AdjustResource((int)evt.GetData());
            EventManager.Instance.TriggerEvent(new CurrencyUpdateEvent(resourceData.Currency));
        }

        private void OnGameOver(GameOverEvent evt)
        {
            uiManager.OpenPanel<MenuPanel>();
            log.Reset();
            knifeThrow.Reset();
            stageSetup.Reset();

            SaveResourceData();
            SaveScoreData();
            scoreData.Reset();

            EventManager.Instance.TriggerEvent(new HighScoreUpdateEvent(scoreData.HighScore));
        }

        private void OnGameStart(GameStartEvent evt)
        {
            uiManager.OpenPanel<GameHUD>();
            stageSetup.Initialize();

            EventManager.Instance.TriggerEvent(new ScoreUpdateEvent(scoreData.Score));
        }

        private IEnumerator Start()
        {
            persistence = new Persistence();
            InitializeResourceData();
            InitializeScoreData();

            uiManager = Instantiate(uiManager);     
            yield return null;
            log = Instantiate(log);
            yield return null;
            knifeThrow = Instantiate(knifeThrow);
            knifeThrow.Initialize(scoreData);
            yield return null;
            stageSetup = Instantiate(stageSetup);

            EventManager.Instance.TriggerEvent(new CurrencyUpdateEvent(resourceData.Currency));
            EventManager.Instance.TriggerEvent(new HighScoreUpdateEvent(scoreData.HighScore));
        }

        void InitializeResourceData()
        {
            resourceData = persistence.LoadData<ResourceData>(ResourceSaveKey);

            if (resourceData == null)
            {
                resourceData = new ResourceData();
            }
        }

        void InitializeScoreData()
        {
            scoreData = persistence.LoadData<ScoreData>(ScoreSaveKey);

            if (scoreData == null)
            {
                scoreData = new ScoreData();
            }
        }

        void SaveResourceData()
        {
            persistence.SaveData(ResourceSaveKey, resourceData);
        }

        void SaveScoreData()
        {
            persistence.SaveData(ScoreSaveKey, scoreData);
        }
    } 
}
