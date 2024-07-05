using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public float playerHealth;
    public float maxPlayerHealth;
    public float playerScore;
    public float maxScoreToHealth;
    
    private GameObject _player;
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;
    private SpriteRenderer _playerCharacter;
    public GameObject gameOverScreen;
    
    public static GameManager Instance { get; private set; }
    
    private static readonly int IsHurt = Animator.StringToHash("isHurt");
    
    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/
    
    /*
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
        _playerAnim = _player.GetComponentInChildren<Animator>();
        _playerCharacter = _player.GetComponentInChildren<SpriteRenderer>();
        
        playerSpawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(float score)
    {
        playerScore += score;

        if (Mathf.Approximately(playerScore, maxScoreToHealth))
        {
            playerScore = 0.0f;
            IncreaseHealth(1.0f);
        }
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0.0f)
        {
            playerHealth = 0.0f;
            GameOver();
        }
    }
    
    private void IncreaseHealth(float health)
    {
        playerHealth += health;
    }

    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        playerSpawnPoint = newSpawnPoint;
    }
    
    public void TakeDamage2(float damage)
    {
        StartCoroutine(TakeDamageAndWait(damage));
    }
    
    private IEnumerator TakeDamageAndWait(float damage)
    {
        // playerHealth -= damage;
        TakeDamage(damage);
        yield return new WaitForSeconds(1.0f);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(Respawn(0.2f));
    }
    
    public void ShakePlayer()
    {
        StartCoroutine(ShakePlayer(0.2f));
    }
    
    private IEnumerator Respawn(float duration)
    {
        _playerRb.simulated = false;
        _playerRb.velocity = new Vector2(0, 0);
        
        yield return new WaitForSeconds(duration);
        
        _player.transform.position = playerSpawnPoint.position;
        _playerAnim.transform.localScale = new Vector3(1, 1, 1);
        _playerRb.simulated = true;
        
        if(_player.activeSelf) 
            _playerAnim.Play("NinjaHurt");
    }
    
    private IEnumerator ShakePlayer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _playerAnim.Play("NinjaHurt");
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

    private void GameOver()
    { 
        _player.SetActive(false);
        gameOverScreen.SetActive(true);
    }

}
