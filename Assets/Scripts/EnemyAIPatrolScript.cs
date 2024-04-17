using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIPatrolScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    Rigidbody2D rb;
    BoxCollider2D box;
    
    public float damage;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2((Mathf.Sign(rb.velocity.x)), transform.localScale.y);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.TakeDamage2(damage);
            gameManager.ShakePlayer();
        }
    }
}
