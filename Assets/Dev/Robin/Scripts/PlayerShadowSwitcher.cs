using System;
using System.Collections;
using System.Collections.Generic;
using Sjoerd;
using UnityEngine;

namespace Robin
{
    public class PlayerShadowSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private GameObject leftShadow;
        [SerializeField] private GameObject rightShadow;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            
            foreach (Transform child in transform) 
            {
                if (child.name == "LeftShadow")
                    leftShadow = child.gameObject;
                

                if (child.name == "RightShadow")
                    rightShadow = child.gameObject;
            }
        }

        private void Update()
        {
            if (_playerAnimation.RightDirection)
            {
                leftShadow.SetActive(false);
                rightShadow.SetActive(true);
            }
            else
            {
                leftShadow.SetActive(true);
                rightShadow.SetActive(false);
            }
        }
    }
}

