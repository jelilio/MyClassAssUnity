using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float offsetLeft = 3, offsetRight = 3, speed = 1;
    [SerializeField] bool hasReachedRight= false, hasReachedLeft = false;
    private Vector3 _startPosition = Vector3.zero;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _startPosition = transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasReachedRight)
        {
            if (transform.position.x < _startPosition.x + offsetRight)
            {
                Move(offsetRight);        
            }
            else if (transform.position.x >= _startPosition.x + offsetRight)
            {
                hasReachedRight = true;
                hasReachedLeft = false;
            }
        }
        else if (!hasReachedLeft)
        {
            if (transform.position.x > _startPosition.x + offsetLeft)
            {
                Move(offsetLeft);
            }
            else if (transform.position.x <= _startPosition.x + offsetLeft)
            {
                hasReachedRight = false;
                hasReachedLeft = true;
            }
        }
    }
    
    void Move(float offset)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(_startPosition.x + offset,
                transform.position.y,
                transform.position.z),
            speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
