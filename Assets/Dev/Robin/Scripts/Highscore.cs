using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Robin
{
    public class Highscore : MonoBehaviour
    {
        public static Highscore instance;
        
        [SerializeField] private TMP_Text score1;
        [SerializeField] private TMP_Text score2;
        [SerializeField] private TMP_Text score3;
        [SerializeField] private TMP_Text score4;
        [SerializeField] private TMP_Text score5;

        private void OnEnable()
        {
            GetSavedHighscore();
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
            
        }

        public void GetSavedHighscore()
        {
            score1.text = PlayerPrefs.GetInt("Score1", 10000).ToString();
            score2.text = PlayerPrefs.GetInt("Score2", 8000).ToString();
            score3.text = PlayerPrefs.GetInt("Score3", 6000).ToString();
            score4.text = PlayerPrefs.GetInt("Score4", 4000).ToString();
            score5.text = PlayerPrefs.GetInt("Score5", 2000).ToString();
        }

        public void NewScore(int newScore)
        {
            List<int> tempScoreList = new List<int>();
            
            tempScoreList.Add(PlayerPrefs.GetInt("Score1", 10000));
            tempScoreList.Add(PlayerPrefs.GetInt("Score2", 10000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score3", 10000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score4", 10000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score5", 10000)); 
            tempScoreList.Add(newScore);
            
            tempScoreList.Sort();

            for (int i = 0; i < tempScoreList.Count; i++)
            {
                PlayerPrefs.SetInt("Score" + (tempScoreList.Count - i), tempScoreList[i]);
            }
        }
    }
}

