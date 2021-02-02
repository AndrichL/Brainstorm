using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Robin
{
    public class ScoreText : MonoBehaviour
    {
        private TMP_Text scoreText;

        private void OnEnable()
        {
            Robin.EventManager.instance.onScoreUpdate += OnScoreUpdate;
        }

        private void OnDisable()
        {
            Robin.EventManager.instance.onScoreUpdate -= OnScoreUpdate;
        }

        private void Start()
        {
            scoreText = GetComponent<TMP_Text>();
        }

        private void OnScoreUpdate(int score)
        {
            scoreText.text = "Score: " + score;
        }
    }
}

