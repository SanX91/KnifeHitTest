using System;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The score data class.
    /// Maintains the current game's score.
    /// Also stores the high score.
    /// </summary>
    [Serializable]
    public class ScoreData
    {
        [SerializeField]
        private int highScore;

        public int HighScore => highScore;

        public int Score { get; private set; }

        public void AdjustScore(int amount, float multiplier = 1)
        {
            Score += amount;
            Score = (int)(Score * multiplier);
            SetHighScore();
        }

        public void Reset()
        {
            Score = 0;
        }

        private void SetHighScore()
        {
            if (Score <= highScore)
            {
                return;
            }

            highScore = Score;
        }
    }
}
