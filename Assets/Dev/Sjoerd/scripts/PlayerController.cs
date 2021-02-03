using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] int maxDistance;

        public Inputmaster controls;
        [SerializeField] private Vector3 adToMovment;
        private Vector3 playerStartingPos;

        private void Awake()
        {
            controls = new Inputmaster();
            playerStartingPos = transform.position;
        }      
        public void MovmentInputInfo(InputAction.CallbackContext ctx )
        {
            adToMovment = ctx.ReadValue<Vector2>() * speed * Time.deltaTime;
        }
        private void Update()
        {
            Movment();
        }
        private void Movment()
        {
            if (transform.position.x < playerStartingPos.x + maxDistance && adToMovment.x >= 0)
                transform.position = transform.position + adToMovment;

            if (transform.position.x > playerStartingPos.x - maxDistance && adToMovment.x <= 0)
                transform.position = transform.position + adToMovment;
        }       
        private void OnEnable()
        {
            controls.Enable();
        }
        private void OnDisable()
        {
            controls.Disable();
        }
    }
}


