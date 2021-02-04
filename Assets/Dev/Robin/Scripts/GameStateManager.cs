using System;
using UnityEngine;

namespace Robin
{
    [DefaultExecutionOrder(-8)]
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager instance;
        
        public enum GameState
        {
            MainMenu,
            InGameMenu,
            GameLoop,
            GameOver,
            Victory
        }

        [SerializeField] private GameState currentGameState;
        [SerializeField] private GameState lastGameState;

        public GameState CurrentGameState => currentGameState;
        public GameState LastGameState => lastGameState;
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        public Action<GameState> onChangeGameState;

        public void ChangeGameState(GameState newGameState)
        {
            if (newGameState == currentGameState)
                return;

            lastGameState = currentGameState;
            currentGameState = newGameState;
            
            onChangeGameState?.Invoke(newGameState);
        }
    }
}


