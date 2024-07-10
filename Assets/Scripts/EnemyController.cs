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

    private bool aggro;
    public Transform[] patrolPoints;
    public bool destinationReached;
    
    public float destinationReachedDistance;
    
    public float patrolSpeed;
    public float aggroSpeed;
    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        aggro = false;
        destinationReached = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the velocity of the enemy
        velocity = _navMeshAgent.velocity.magnitude;
        // Set the speed of the animator to the velocity of the enemy
        enemyAnim.SetFloat(Velocity, velocity);
        
        if (aggro == false && destinationReached == true)
        {
            _navMeshAgent.speed = patrolSpeed;
            destinationReached = false;
            // Move towards the patrol points using navmesh
            _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }
        
        if (aggro == true)
        {
            _navMeshAgent.speed = aggroSpeed;
            // Move towards the player using navmesh
            _navMeshAgent.destination = player.position;
        }
        
        // Check if the enemy has reached the destination
        if (Vector3.Distance(transform.position, _navMeshAgent.destination) < destinationReachedDistance)
        {
            destinationReached = true;
        }
        
        // _navMeshAgent.destination = player.position;
    }
}
