using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHaveshAI : MonoBehaviour
{
    public Transform player;
    
    public float velocity;
    public float aggroSpeed;
    public Animator anim;
    public bool destinationReached;
    public NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // velocity = GetComponent<NavMeshAgent>().velocity.magnitude;
        // anim.SetFloat("velocity", velocity);

        // if (aggro == false && destinationReached)
        // {
        //     agent.speed = patrolSpeed;
        //     destinationReached = false;
        //     agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)];
        // }
        
        // GetComponent<NavMeshAgent>().destination = player.position;
    }
}
