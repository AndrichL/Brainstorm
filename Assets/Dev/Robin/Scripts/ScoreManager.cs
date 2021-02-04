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

        private void OnEnable()
        {
            EventManager.instance.onDeathAnimationFinished += OnPlayerDeath;
        }

        private void OnDisable()
        {
            EventManager.instance.onDeathAnimationFinished -= OnPlayerDeath;
        }

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
            EventManager.instance.BroadcastOnScoreUpdate(currentScore);
        }

        public void ResetScore()
        {
            currentScore = 0;
            EventManager.instance.BroadcastOnScoreUpdate(currentScore);
        }

        private void OnPlayerDeath()
        {
            PlayerPrefs.SetInt("Score6", currentScore);
        }
    }
}

