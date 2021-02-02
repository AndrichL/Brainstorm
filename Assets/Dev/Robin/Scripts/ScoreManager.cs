using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robin
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        private int currentScore;
        public int CurrentScore => currentScore;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        public void AddToScore(int scoreAmount)
        {
            currentScore += scoreAmount;
            Robin.EventManager.instance.BroadcastOnScoreUpdate(currentScore);
        }

        public void ResetScore()
        {
            currentScore = 0;
            Robin.EventManager.instance.BroadcastOnScoreUpdate(currentScore);
        }
    }
}

