using System;
using UnityEngine;

namespace SMB
{
    public class SideScrolling : MonoBehaviour
    {
        private Transform _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = Math.Max(cameraPosition.x, _player.position.x); // prevent the camera from moving backward
            transform.position = cameraPosition;
        }
    }
}
