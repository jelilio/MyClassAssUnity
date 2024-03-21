using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public float playerHealth;
    public float maxPlayerHealth;
    public float playerScore;
    
    private GameObject _player;
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;
    private SpriteRenderer _playerCharacter;
    
    private static readonly int IsHurt = Animator.StringToHash("isHurt");
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        playerSpawnPoint = newSpawnPoint;
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
        _playerAnim.Play("hurt");
    }
    
    private IEnumerator ShakePlayer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _playerAnim.Play("hurt");
    }
}
