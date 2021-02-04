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

            Time.timeScale = 1;
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
            Time.timeScale = 0f;
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GameOver);
            Sjoerd.AudioManager.thisAudioManager.Play("Death");

            StartCoroutine(GameOver(player));
        }

        private IEnumerator GameOver(GameObject player)
        {
            EventManager.instance.BroadcastOnPlayerAliveStateChange(false);

            float animationLength = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length * Time.unscaledTime;
            yield return new WaitForSecondsRealtime(animationLength);

            if (player == null)
            {
                player = m_CurrentPlayer.gameObject;
            }
            player.SetActive(false);

        }

        private void OnDeathAnimationFinished()
        {
            m_CurrentPlayer.gameObject.SetActive(false);
            EventManager.instance.BroadcastOnPlayerAliveStateChange(false);
            MenuManager.instance.ShowGameOverMenu();
        }

        public void DeactivateItem(GameObject item, float time)
        {
            StartCoroutine(DeactivateTimer(item, time));
        }

        private IEnumerator DeactivateTimer(GameObject item, float time)
        {
            yield return new WaitForSeconds(time);
            item.SetActive(false);
        }
    }
}
