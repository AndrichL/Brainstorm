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

        private bool PlayerIsAlive;

        private bool _rightDirection;

        public bool RightDirection
        {
            get { return _rightDirection;}
            set
            {
                _spriteRenderer.flipX = value;
                _rightDirection = value;
            } 
        }

        private void OnEnable()
        {
            EventManager.instance.onPlayerAliveStateChange += OnPlayerAliveStateChange;
            EventManager.instance.onHurtPlayer += OnHurtPlayer;
        }

        private void OnDisable()
        {
            EventManager.instance.onPlayerAliveStateChange -= OnPlayerAliveStateChange;
            EventManager.instance.onHurtPlayer -= OnHurtPlayer;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator.updateMode = AnimatorUpdateMode.Normal;
            PlayerIsAlive = true;
        }

        private void Update()
        {
            if(GameStateManager.instance.CurrentGameState != GameStateManager.GameState.GameLoop)
                return;
                
            if(PlayerIsAlive)
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
        }

        private void KillPlayer()
        {
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            _animator.SetTrigger("KillPlayer");
            PlayerIsAlive = false;
        }

        private void ResetAnimator()
        {
            _animator.SetTrigger("ResetAnimator");
            PlayerIsAlive = true;
        }

        private void HurtPlayer()
        {
            _animator.SetTrigger("HurtPlayer");
        }

        private void OnPlayerAliveStateChange(bool playerAlive)
        {
            if (playerAlive)
            {
                ResetAnimator();
            }
            else
            {
                KillPlayer();
            }
        }

        private void OnHurtPlayer()
        {
            HurtPlayer();
        }

        public void SendDeathAnimationFinished()
        {
            EventManager.instance.BroadcastOnDeathAnimationFinished();
        }

        public void SendHurtAnimationFinished()
        {
            EventManager.instance.BroadcastOnHurtAnimationFinished();
        }

        // public void PlayerInput(InputAction.CallbackContext callbackContext)
        // {
        //     Vector2 movementInfo = callbackContext.ReadValue<Vector2>();
        //     
        //     Debug.Log(movementInfo);
        // }
    }
}

