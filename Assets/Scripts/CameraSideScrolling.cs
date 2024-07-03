using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSideScrolling : MonoBehaviour
{
    private Transform _player;
    private GameManager _gameManager;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform; 
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Math.Max(cameraPosition.x, _player.position.x); // prevent the camera from moving backward
        transform.position = cameraPosition;
    }
}
