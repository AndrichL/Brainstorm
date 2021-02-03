using System;
using System.Collections;
using System.Collections.Generic;
using Sjoerd;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Robin
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private PlayerController _playerController;
        private SpriteRenderer _spriteRenderer;

        private bool _rightDirection;

        public bool RightDirection
        {
            get
            {
                return _rightDirection;
            }
            set
            {
                _spriteRenderer.flipX = value;
                _rightDirection = value;
            } 
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 movementVector = _playerController.MovmentInfo;

            if (movementVector.x != 0)
            {
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }

            if (movementVector.x > 0)
                RightDirection = true;
            
            if(movementVector.x < 0)
                RightDirection = false;
        }

        public void PlayerInput(InputAction.CallbackContext callbackContext)
        {
            Vector2 movementInfo = callbackContext.ReadValue<Vector2>();
            
            Debug.Log(movementInfo);
            
            
        }
        
        private void FlipPlayer()
        {
            
        }
    }
}

