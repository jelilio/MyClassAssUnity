using UnityEngine;

namespace SMB
{
    public class CastleScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);
                
                Player player = other.GetComponent<Player>();
                player.StageClear();
            }
        }
    }
}
