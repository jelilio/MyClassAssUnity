using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    public Animator playerAnim;
    
    private NavMeshAgent _navMeshAgent;
    private Camera _camera;
    private static readonly int Velocity = Animator.StringToHash("velocity");

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = _navMeshAgent.velocity.magnitude;
        playerAnim.SetFloat(Velocity, velocity);
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera!.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _navMeshAgent.destination = hit.point;
            }
        }
    }
}
