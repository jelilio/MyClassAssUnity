using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchPlayer : MonoBehaviour
{
    public Game3DManager gameManager;
    public float aggroTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<Game3DManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.IncrementCatch();
        }
    }
}
