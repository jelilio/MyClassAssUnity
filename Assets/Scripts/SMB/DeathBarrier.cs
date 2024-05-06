using UnityEngine;

namespace SMB
{
    public class DeathBarrier : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);
                
                Player player = other.GetComponent<Player>();
                player.Death();
                
                // GameController.Instance.ResetLevel(3f);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
