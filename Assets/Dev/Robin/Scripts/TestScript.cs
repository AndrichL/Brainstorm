using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyNamespace
{
    public class TestScript : MonoBehaviour
    {
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     Robin.ScoreManager.instance.AddToScore(100);
            // }
        }
        
        public void TestInput(InputAction.CallbackContext something)
        {
            if(something.performed)
                Robin.ScoreManager.instance.AddToScore(100);
        }
    }
}