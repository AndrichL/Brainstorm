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
                switch (GameStateManager.instance.CurrentGameState)
                {
                    case GameStateManager.GameState.MainMenu:
                        
                        Application.Quit();
                        break;
                    
                    case GameStateManager.GameState.GameLoop:
                        
                        ShowPauseMenu();
                        break;
                    
                    case GameStateManager.GameState.InGameMenu:
                        
                        CloseAllMenus();
                        break;
                }
            }
        }

        public void ShowMainMenu()
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.MainMenu);
            
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }
        
        public void ShowPauseMenu()
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.InGameMenu);
            
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(true);
            highscoreMenu.SetActive(false);
        }
        
        public void CloseAllMenus()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }

        public void ShowGameOverMenu()
        {
            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GameOver);
            
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            gameOverMenu.SetActive(true);
            pauseMenu.SetActive(false);
            highscoreMenu.SetActive(false);
        }
        
        public void StartGame()
        {
            //SceneManager.LoadScene()
        }
        
    }
}

