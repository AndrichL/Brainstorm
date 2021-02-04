using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd
{
    public class TestScripts : MonoBehaviour
    {
        AudioManager audio;
        private void Start()
        {
            audio = FindObjectOfType<AudioManager>();
        }
        private void Update()
        {
            if (InputSystem.GetDevice<Keyboard>().iKey.wasPressedThisFrame)
            {
                audio.Play("OST");
            }

            if (InputSystem.GetDevice<Keyboard>().kKey.wasPressedThisFrame)
            {
                audio.Pause("OST");
            }

            if (InputSystem.GetDevice<Keyboard>().lKey.wasPressedThisFrame)
            {
                audio.UnPause("OST");
            }

            if (InputSystem.GetDevice<Keyboard>().mKey.wasPressedThisFrame)
            {
                audio.Stop("OST");
            }
        }
    }
}
