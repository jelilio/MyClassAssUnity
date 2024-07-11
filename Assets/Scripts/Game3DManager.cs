using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Game3DManager : MonoBehaviour
{
    
    private GameObject _player;
    private Rigidbody _playerRb;

    public int escapes;
    public int catches;
    
    public Transform playerSpawnPoint;
    private Animator _playerAnim;
    private NavMeshAgent _navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
        _navMeshAgent = _player.GetComponent<NavMeshAgent>();
        _playerAnim = _player.GetComponentInChildren<Animator>();
        playerSpawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IncrementEscape()
    {
        escapes += 1;
    }
    
    public void IncrementCatch()
    {
        catches += 1;
        escapes -= 1;
        RespawnPlayer();
    }
    
    private void RespawnPlayer()
    {
        StartCoroutine(Respawn(0.2f));
    }
    
    private IEnumerator Respawn(float duration)
    {
        // _playerRb.simulated = false;
        // _playerRb.velocity = new Vector3(0, 0, 0);
        
        yield return new WaitForSeconds(duration);
        
        _navMeshAgent.destination = playerSpawnPoint.position;
        _player.transform.position = playerSpawnPoint.position;
        _playerAnim.transform.localScale = new Vector3(1, 1, 1);
        // _playerRb.simulated = true;
    }
}
