using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Robin
{
    public class TestScript : MonoBehaviour
    {
        private void Update()
        {
            if (InputSystem.GetDevice<Keyboard>().fKey.wasPressedThisFrame)
            {
                EventManager.instance.BroadcastOnPlayerAliveStateChange(false);
            }
        }
        
        public void TestInput(InputAction.CallbackContext something)
        {
            // if(something.performed)
            //     Robin.ScoreManager.instance.AddToScore(100);
        }
    }
}