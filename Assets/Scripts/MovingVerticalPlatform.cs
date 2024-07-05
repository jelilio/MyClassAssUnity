using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVerticalPlatform : MonoBehaviour
{
    [SerializeField] float offsetBottom = 3, offsetTop = 3, speed = 1;
    [SerializeField] bool hasReachedTop = false, hasReachedBottom = false;
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
        if (!hasReachedTop)
        {
            if (transform.position.y < _startPosition.y + offsetTop)
            {
                Move(offsetTop);        
            }
            else if (transform.position.y >= _startPosition.y + offsetTop)
            {
                hasReachedTop = true;
                hasReachedBottom = false;
            }
        }
        else if (!hasReachedBottom)
        {
            if (transform.position.y > _startPosition.y + offsetBottom)
            {
                Move(offsetBottom);
            }
            else if (transform.position.y <= _startPosition.y + offsetBottom)
            {
                hasReachedTop = false;
                hasReachedBottom = true;
            }
        }
    }
    
    void Move(float offset)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(_startPosition.x,
                transform.position.y + offset,
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
