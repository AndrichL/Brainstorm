using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sjoerd 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] private Vector2 baseMovelimit;
        [SerializeField] private Vector2 movmentInfo;
        public Vector2 MovmentInfo => movmentInfo;

        private Rigidbody2D rb;
        private BoxCollider2D box;        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            box = GetComponent<BoxCollider2D>();
        }      
        public void MovmentInputInfo(InputAction.CallbackContext ctx )
        {
            movmentInfo = ctx.ReadValue<Vector2>();
        }
        private void Movment()
        {
            Vector2 moveLimet = baseMovelimit - (Vector2)box.bounds.size * 0.5f;
            Vector2 newPos = rb.position + (movmentInfo * speed * Time.fixedDeltaTime);
            newPos.x = Mathf.Clamp(newPos.x, -moveLimet.x, moveLimet.x);
            rb.MovePosition(newPos);
        }
        private void FixedUpdate()
        {
            Movment();
        }
    }
}


