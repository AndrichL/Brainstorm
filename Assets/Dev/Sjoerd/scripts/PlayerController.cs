using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 5;
        [SerializeField] private float baseMovelimit = 5;
        [SerializeField] private Vector2 movementInfo;
        public Vector2 MovmentInfo => movementInfo;

        private Rigidbody2D rb;
        private BoxCollider2D box;        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
        }
        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            float moveLimit = baseMovelimit - (box.bounds.size.x * 0.5f);
            Vector2 newPos = rb.position + (movementInfo * speed * Time.fixedDeltaTime);
            newPos.x = Mathf.Clamp(newPos.x, -moveLimit, moveLimit);

            rb.MovePosition(newPos);
        }

        public void MovmentInputInfo(InputAction.CallbackContext ctx)
        {
            movementInfo = ctx.ReadValue<Vector2>();
        }
    }
}


