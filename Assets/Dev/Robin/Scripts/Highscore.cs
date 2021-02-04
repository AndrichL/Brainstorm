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
            //ResetScore();
            CalculateScoreOrder();
            GetSavedHighscore();
        }

        private void ResetScore()
        {
            PlayerPrefs.DeleteKey("Score1");
            PlayerPrefs.DeleteKey("Score2");
            PlayerPrefs.DeleteKey("Score3");
            PlayerPrefs.DeleteKey("Score4");
            PlayerPrefs.DeleteKey("Score5");
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

        public void CalculateScoreOrder()
        {
            List<int> tempScoreList = new List<int>();
            
            tempScoreList.Add(PlayerPrefs.GetInt("Score1", 10000));
            tempScoreList.Add(PlayerPrefs.GetInt("Score2", 8000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score3", 6000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score4", 4000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score5", 2000)); 
            tempScoreList.Add(PlayerPrefs.GetInt("Score6", 0));
            
            tempScoreList.Sort();

            for (int i = 0; i < tempScoreList.Count -1; i++)
            {
                PlayerPrefs.SetInt("Score" + (tempScoreList.Count - i), tempScoreList[i]);
            }
        }
    }
}

