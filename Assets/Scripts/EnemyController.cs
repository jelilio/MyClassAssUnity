using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private NavMeshAgent _navMeshAgent;
    public Animator enemyAnim;
    
    public float velocity;
    private static readonly int Velocity = Animator.StringToHash("velocity");

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the velocity of the enemy
        velocity = _navMeshAgent.velocity.magnitude;
        // Set the speed of the animator to the velocity of the enemy
        enemyAnim.SetFloat(Velocity, velocity);
        
        _navMeshAgent.destination = player.position;
    }
}
