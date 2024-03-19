using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float scoreValue;
    public GameManager gameManager;
    public GameObject pickupEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        var transform1 = transform;
        gameManager.AddScore(scoreValue);
        Instantiate(pickupEffect, transform1.position, transform1.rotation);
        Destroy(gameObject);
    }
}
