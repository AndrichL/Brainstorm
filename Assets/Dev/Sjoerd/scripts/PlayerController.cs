using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed;

        public Inputmaster controls;
        private Vector3 adToMovment;

        private void Awake()
        {
            controls = new Inputmaster();

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


