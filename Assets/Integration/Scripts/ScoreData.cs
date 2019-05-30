using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    [Serializable]
    public class ScoreData
    {
        [SerializeField]
        int highScore;

        public int HighScore => highScore;

        public int Score { get; private set; }

        public void AdjustScore(int amount, float multiplier = 1)
        {
            Score += amount;
            Score = (int)(Score * multiplier);
            SetHighScore();
            Debug.Log($"{Score},{amount},{multiplier}");
        }

        public void Reset()
        {
            Score = 0;
        }

        void SetHighScore()
        {
            if (Score <= highScore)
            {
                return;
            }

            highScore = Score;
        }
    } 
}
