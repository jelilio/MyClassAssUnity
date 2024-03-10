using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    
    public Rigidbody2D myRb;
    public float jumpForce;
    public bool isGrounded;
    
    public float secondaryJumpForce;
    public float secondaryJumpTime;

    public bool secondaryJump;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"), 0), ForceMode2D.Force);
        }
        
        //JUMP CODE
        if (isGrounded == true && Input.GetButtonDown("Jump")) // if the player is grounded and the jump button is pressed
        {
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // add a force in the y direction
            StartCoroutine(SecondaryJump());
        }

        if (isGrounded == false && Input.GetButton("Jump") && secondaryJump == true)
        {
            myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force); //while the jump button is held, add a force in the y direction
        }
        //END JUMP CODE
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay2D");
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        isGrounded = false;
    }
    
    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpTime); // wait for a certain amount of time
        secondaryJump = false;
        yield return null;
    }
}
