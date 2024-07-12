using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Game3DManager : MonoBehaviour
{
    
    private GameObject _player;
    private Rigidbody _playerRb;

    public int escapes;
    public int catches;
    public int playerHealth;
    public GameObject gameOverScreen;

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
    
    public void TakeDamage()
    {
        playerHealth -= 1;
        escapes -= 1;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }
    }
    
    private void GameOver()
    { 
        _player.SetActive(false);
        gameOverScreen.SetActive(true);
    }
    
    private void RespawnPlayer()
    {
        StartCoroutine(Respawn(0.2f));
    }
    
    private IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        _navMeshAgent.destination = playerSpawnPoint.position;
        _player.transform.position = playerSpawnPoint.position;
        _playerAnim.transform.localScale = new Vector3(1, 1, 1);
    }
    
    public void ResetGame()
    {
        _player.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void RestartGame()
    {
        Invoke(nameof(ResetGame), 1f);
    }
}
