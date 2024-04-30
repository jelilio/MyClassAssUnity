using UnityEditor;
using UnityEngine;

namespace SMB
{
    public class EntityMovement : MonoBehaviour
    {
        public float speed = 1f;
        public Vector2 direction = Vector2.left;

        private new Rigidbody2D rigidbody;
        private Vector2 velocity;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            enabled = false;
        }

        private void OnBecameVisible()
        {
            enabled = !EditorApplication.isPaused;
        enabled = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            rigidbody.WakeUp();
        }

        private void OnDisable()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.Sleep();
        }

        private void FixedUpdate()
        {
            velocity.x = direction.x * speed;
            velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

            // move the entity object
            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

            // detect collision and switch to opposite direction
            if (rigidbody.Raycast(direction)) {
                direction = -direction;
            }

            if (rigidbody.Raycast(Vector2.down)) {
                velocity.y = Mathf.Max(velocity.y, 0f);
            }

            if (direction.x > 0f) {
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            } else if (direction.x < 0f) {
                transform.localEulerAngles = Vector3.zero;
            }
        }

    }
}
