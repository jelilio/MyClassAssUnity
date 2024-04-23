using System;
using UnityEngine;

namespace SMB
{
    public class PlayerMovement : MonoBehaviour
    {
        private new Camera camera;
        private new Rigidbody2D rigidbody;

        private Vector2 velocity;
        private float inputAxis;

        public float moveSpeed = 8f;
        public float maxJumpHeight = 5f;
        public float maxJumpTime = 1f;
        public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
        public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

     
        public bool grounded { get; private set; }
        public bool jumping { get; private set; }
        
        private void Awake()
        {
            camera = Camera.main;
            rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            rigidbody.isKinematic = false;
            velocity = Vector2.zero;
            jumping = false;
        }

        private void OnDisable()
        {
            rigidbody.isKinematic = true;
            velocity = Vector2.zero;
            jumping = false;
        }

        private void Update()
        {
            HorizontalMovement();
            
            grounded = rigidbody.Raycast(Vector2.down);
            if (grounded)
            {
                GroundedMovement();
            }

            ApplyGravity();
        }

        private void FixedUpdate()
        {
            Vector2 position = rigidbody.position;
            position += velocity * Time.fixedDeltaTime;

            // prevent the mario from moving out of camera bound
            Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
            Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
            
            rigidbody.MovePosition(position);
        }

        private void HorizontalMovement()
        {
            inputAxis = Input.GetAxis("Horizontal");
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed + Time.deltaTime);
        }

        private void GroundedMovement()
        {
            // prevent gravity from infinitly building up
            velocity.y = Mathf.Max(velocity.y, 0f);
            jumping = velocity.y > 0f;

            // perform jump
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
                jumping = true;
            }
        }

        private void ApplyGravity()
        {
            // check if falling
            bool falling = velocity.y < 0f || !Input.GetButton("Jump");
            float multiplier = falling ? 2f : 1f;

            // apply gravity and terminal velocity
            velocity.y += gravity * multiplier * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, gravity / 2f);
        }
    }
}
