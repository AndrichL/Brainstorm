using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd 
{
    public class PlayerController : MonoBehaviour
    {
        public Inputmaster controls;
        private Vector3 movment;

        private void Awake()
        {
            controls = new Inputmaster();
            controls.Player.Movment.performed += ctx => Movment(ctx.ReadValue<float>());
        }

        void Movment(float direction)
        {
            movment.x = direction;
        }
        private void Update()
        {
            gameObject.transform.position = gameObject.transform.position + movment;
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


