using System;
using Robin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Andrich
{ 
    public class GameManager : MonoBehaviour
    {
        public static GameManager m_Instance { get; private set; }
        private Player m_CurrentPlayer;

        private void OnEnable()
        {
            EventManager.instance.onDeathAnimationFinished += OnDeathAnimationFinished;
        }

        private void OnDisable()
        {
            EventManager.instance.onDeathAnimationFinished -= OnDeathAnimationFinished;
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
            }
            else if (m_Instance != null)
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                //Restart();
            }
        }

        private void Restart()
        {
            if(m_CurrentPlayer == null)
            {
                m_CurrentPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
        }

        public void CurrentPlayer(GameObject player)
        {
            m_CurrentPlayer = player.GetComponent<Player>();
        }

        public Player GetCurrentPlayer()
        {
            return m_CurrentPlayer;
        }

        public void GameIsOver(GameObject player)
        {
            Time.timeScale = 0;
            Debug.Log("Game is over!");

            
            StartCoroutine(GameOver(player));
        }

        private IEnumerator GameOver(GameObject player)
        {
            EventManager.instance.BroadcastOnPlayerAliveStateChange(false);

            yield return new WaitForSecondsRealtime(2f);

            if (player == null)
            {
                player = m_CurrentPlayer.gameObject;
            }
            player.SetActive(false);
        }

        private void OnDeathAnimationFinished()
        {
            
        }
    }
}
