using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    
    private NavMeshAgent _navMeshAgent;
    private Camera _camera;

    // Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = _navMeshAgent.velocity.magnitude;
        // playerAnim.SetFloat("speed", velocity);
        
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
