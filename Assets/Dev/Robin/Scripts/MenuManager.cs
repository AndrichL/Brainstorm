using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Robin
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject gameOverMenu;
        public GameObject pauseMenu;
        public GameObject highscoreMenu;

        private void OnEnable()
        {
            GameStateManager.instance.onChangeGameState += OnGameStateChange;
            EventManager.instance.onDeathAnimationFinished += OnDeathAnimationFinished;
        }

        private void OnDisable()
        {
            GameStateManager.instance.onChangeGameState -= OnGameStateChange;
            EventManager.instance.onDeathAnimationFinished -= OnDeathAnimationFinished;
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private void Update()
        {
            if (InputSystem.GetDevice<Keyboard>().escapeKey.wasPressedThisFrame || 
                InputSystem.GetDevice<Keyboard>().pKey.wasPressedThisFrame ||
                InputSystem.GetDevice<Keyboard>().pauseKey.wasPressedThisFrame)
            {

                Sjoerd.AudioManager.thisAudioManager.Play("Button");

                switch (GameStateManager.instance.CurrentGameState)
                {
                    case GameStateManager.GameState.MainMenu:
                        
                        Application.Quit();
                        break;
                    
                    case GameStateManager.GameState.GameLoop:
                        Time.timeScale = 0f;
                        Sjoerd.AudioManager.thisAudioManager.Pause("OST");
                        ShowPauseMenu();
                        break;
                    
                    case GameStateManager.GameState.InGameMenu:
                        Time.timeScale = 1f;
                        Sjoerd.AudioManager.thisAudioManager.UnPause("OST");
                        CloseAllMenus();
                        break;
                }
            }
        }

        public void ShowMainMenu()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.MainMenu);
            
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }
        
        public void ShowPauseMenu()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.InGameMenu);
            
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(true);
            highscoreMenu.SetActive(false);
        }
        
        public void ShowOptionsMenu()
        {
            Debug.Log(GameStateManager.instance.CurrentGameState);

            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.InGameMenu);
            
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }
        
        public void CloseAllMenus()
        {
            Time.timeScale = 1f;

            GameStateManager.instance.ChangeGameState(GameStateManager.instance.LastGameState);

            Sjoerd.AudioManager.thisAudioManager.Play("Button");
            Sjoerd.AudioManager.thisAudioManager.UnPause("OST");

            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }

        public void ShowGameOverMenu()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(true);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }
        
        public void ShowHighscoreMenu()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(true);
        }
        
        public void StartGame()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            Sjoerd.AudioManager.thisAudioManager.Stop("menuOST");
            Sjoerd.AudioManager.thisAudioManager.Play("OST");
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GameLoop);
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            Application.Quit();
        }

        public void Restart()
        {

            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            //GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GameLoop);
            CloseAllMenus();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToMainMenu()
        {
            Sjoerd.AudioManager.thisAudioManager.Play("Button");

            Sjoerd.AudioManager.thisAudioManager.Stop("OST");
            Sjoerd.AudioManager.thisAudioManager.Play("menuOST");

            SceneManager.LoadScene("Menu");
        }

        private void OnGameStateChange(GameStateManager.GameState newState)
        {
            
        }

        private void OnDeathAnimationFinished()
        {
            if (GameStateManager.instance.CurrentGameState == GameStateManager.GameState.GameOver)
            {
                ShowGameOverMenu();
            }
        }
    }
}

