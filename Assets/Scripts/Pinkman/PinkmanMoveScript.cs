using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Pinkman
{
    public class PinkmanMoveScript : MonoBehaviour
    {
        public float maxSpeed;
        public float acceleration;
    
        public Rigidbody2D myRb;
        public Animator anim;
        public float jumpForce;
        public bool isGrounded;
    
        public float secondaryJumpForce;
        public float secondaryJumpDelay;

        public bool secondaryJump;
        private static readonly int Speed = Animator.StringToHash("speed");
        
        // Start is called before the first frame update
        void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // to transit animation between idle and run
            anim.SetFloat(Speed, Mathf.Abs(myRb.velocity.x));
            
            // flip
            Flip();

            // move
            Move();
            
            // jump
            Jump();
        }

        void Move()
        {
            // to move the player horizontally
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Mathf.Abs(myRb.velocity.x) < maxSpeed) // if the absolute value of the input is greater than 0.1, and player is not moving faster than max Speed
            {
                myRb.AddForce(new Vector2(Input.GetAxis("Horizontal") * acceleration, 0), ForceMode2D.Force); //gets Input value and multiplies it by acceleration in the x direction.
            }
        }

        void Flip()
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded) // if the player is grounded and the jump button is pressed
            {
                myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // add a force in the y direction
                StartCoroutine(SecondaryJump());
            }
            
            if (Input.GetButton("Jump") && secondaryJump && isGrounded)
            {
                myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force); //while the jump button is held, add a force in the y direction
            }
        }
        
        IEnumerator SecondaryJump()
        {
            secondaryJump = true;
            yield return new WaitForSeconds(secondaryJumpDelay); // wait for a certain amount of time
            secondaryJump = false;
            yield return null;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            isGrounded = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            isGrounded = false;
        }
    }
}
