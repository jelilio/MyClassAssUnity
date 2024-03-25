using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public GameObject _player;
    public float speed;

    private float _distance;
    private AIBehaviour _currentBehaviour = AIBehaviour.Idle;

    public Rigidbody2D rb;
    public Animator anim;
    
    public float minDistance;
    public float minWalkDistance;
    public float minRunDistance;
    
    public float damage;
    public GameManager gameManager;
    
    private static readonly int Speed = Animator.StringToHash("xSpeed");
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        
        // rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);
        Vector2 direction = _player.transform.position - transform.position;
        // direction.Normalize();

        // float angle = Mathf.Atan2(direction.y, direction.x) + Mathf.Rad2Deg;

        // transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        // transform.position= Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        // transform.position = position;
        // rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Force); //gets Input value and multiplies it by acceleration in the x direction.
        // rb.AddForce(new Vector2(direction.magnitude * speed, 0), ForceMode2D.Force); //gets Input value and multiplies it by acceleration in the x direction.
        // rb.AddForce(new Vector2(-1 * position.x, 0), ForceMode2D.Force); //gets Input value and multiplies it by acceleration in the x direction.

        // rb.MovePosition(position);
        if (_distance < minDistance)
        {
            Vector2 position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector2(position.x, transform.position.y);
        }

        if (_distance < minWalkDistance) anim.SetFloat(Speed, 0.5f); // walk
        if (_distance < minRunDistance) anim.SetFloat(Speed, 1.5f); // run
        if (_distance > minDistance) anim.SetFloat(Speed, 0);
        
        
        // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        Flip(direction);
        
        // velocity = (transform.position - pos) / Time.deltaTime;
    }
    
    private void FixedUpdate()
    {
        // to transit animation between idle and run
        // anim.SetFloat(Speed, Mathf.Abs(rb.velocity.x));
    }
    
    private void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            anim.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (direction.x < 0)
        {
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    public record AIBehaviour(int State)
    {
        public static readonly AIBehaviour Idle = new(1);
        public static readonly AIBehaviour Patrol = new(2);
        public static readonly AIBehaviour DetectPlayer = new(3);
        public static readonly AIBehaviour ChasePlayer = new(4);
        
        public int State { get; } = State;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.TakeDamage(damage);
            gameManager.ShakePlayer();
        }
    }
}
