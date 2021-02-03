using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robin
{
    // Just to make sure this script always initialized before any subscribers
    [DefaultExecutionOrder(-10)]
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        // SCORE UPDATE
        public Action<int> onScoreUpdate;
        public void BroadcastOnScoreUpdate(int newScore)
        {
            onScoreUpdate?.Invoke(newScore);
        }

        // PLAYER ALIVE STATE CHANGE
        public Action<bool> onPlayerAliveStateChange;
        public void BroadcastOnPlayerAliveStateChange(bool playerAlive)
        {
            onPlayerAliveStateChange?.Invoke(playerAlive);
        }

        // HURT PLAYER
        public Action onHurtPlayer;
        public void BroadcastOnHurtPlayer()
        {
            onHurtPlayer?.Invoke();
        }
        
        // HURT ANIMATION FINISHED
        public Action onHurtAnimationFinished;
        public void BroadcastOnHurtAnimationFinished()
        {
            onHurtAnimationFinished?.Invoke();
        }
        
        // DEATH ANIMATION FINISHED
        public Action onDeathAnimationFinished;
        public void BroadcastOnDeathAnimationFinished()
        {
            onDeathAnimationFinished?.Invoke();
        }

        // NEXT EVENT....
    }
}


