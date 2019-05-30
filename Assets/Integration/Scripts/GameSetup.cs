using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Game Setup class.
    /// Instantiates all the sub systems of the game.
    /// Currently, also responsible for saving/loading score and currency data.
    /// </summary>
    public class GameSetup : MonoBehaviour
    {
        [SerializeField]
        private UIManager uiManager;
        [SerializeField]
        private LogPiece logPiece;
        [SerializeField]
        private KnifeThrow knifeThrow;
        [SerializeField]
        private StageSetup stageSetup;
        private Persistence persistence;
        private ResourceData resourceData;
        private ScoreData scoreData;
        private const string ResourceSaveKey = "ResourceSave";
        private const string ScoreSaveKey = "ScoreSave";

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
            logPiece.Reset();
            knifeThrow.Reset();
        }

        private void OnCurrencyPickup(CurrencyPickupEvent evt)
        {
            resourceData.AdjustResource((int)evt.GetData());
            EventManager.Instance.TriggerEvent(new CurrencyUpdateEvent(resourceData.Currency));
        }

        /// <summary>
        /// Resets all the subsystems on game over.
        /// Also saves the currency, and high score updates.
        /// </summary>
        /// <param name="evt"></param>
        private void OnGameOver(GameOverEvent evt)
        {
            uiManager.OpenPanel<MenuPanel>();
            logPiece.Reset();
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

        /// <summary>
        /// Instantiates all the sub systems of the game.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            persistence = new Persistence();
            InitializeResourceData();
            InitializeScoreData();

            uiManager = Instantiate(uiManager);
            yield return null;
            logPiece = Instantiate(logPiece);
            yield return null;
            knifeThrow = Instantiate(knifeThrow);
            knifeThrow.Initialize(scoreData);
            yield return null;
            stageSetup = Instantiate(stageSetup);

            EventManager.Instance.TriggerEvent(new CurrencyUpdateEvent(resourceData.Currency));
            EventManager.Instance.TriggerEvent(new HighScoreUpdateEvent(scoreData.HighScore));
        }

        /// <summary>
        /// Creates a ResourceData instance from an existing save, if any.
        /// Otherwise, just creates a new ResourceData instance
        /// </summary>
        private void InitializeResourceData()
        {
            resourceData = persistence.LoadData<ResourceData>(ResourceSaveKey);

            if (resourceData == null)
            {
                resourceData = new ResourceData();
            }
        }

        /// <summary>
        /// Creates a ScoreData instance from an existing save, if any.
        /// Otherwise, just creates a new ScoreData instance
        /// </summary>
        private void InitializeScoreData()
        {
            scoreData = persistence.LoadData<ScoreData>(ScoreSaveKey);

            if (scoreData == null)
            {
                scoreData = new ScoreData();
            }
        }

        private void SaveResourceData()
        {
            persistence.SaveData(ResourceSaveKey, resourceData);
        }

        private void SaveScoreData()
        {
            persistence.SaveData(ScoreSaveKey, scoreData);
        }

        public void ClearAllData()
        {
            persistence = new Persistence();
            persistence.ClearAllData();
        }
    }
}
