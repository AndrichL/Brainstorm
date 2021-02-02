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
        
        // NEXT EVENT....
    }
}


