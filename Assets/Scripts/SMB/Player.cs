using System.Collections;
using UnityEngine;

namespace SMB
{
    public class Player : MonoBehaviour
    {
        public PlayerSpriteRenderer smallRenderer;
        public PlayerSpriteRenderer bigRenderer;
        private PlayerSpriteRenderer activeRenderer;

        public AudioSource playSound;
        public AudioClip marioDie;
        public AudioClip stageClear;

        public CapsuleCollider2D capsuleCollider { get; private set; }
        public DeathAnimation deathAnimation { get; private set; }

        public bool big => bigRenderer.enabled;
        public bool dead => deathAnimation.enabled;
        public bool starpower { get; private set; }
        
        
        private void Awake()
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            deathAnimation = GetComponent<DeathAnimation>();
            activeRenderer = smallRenderer;
        }

        public void Hit()
        {
            if (!dead && !starpower)
            {
                if (big)
                {
                    // Shrink();
                }
                else
                {
                    Death();
                }
            }
        }

        public void Death()
        {
            smallRenderer.enabled = false;
            bigRenderer.enabled = false;
            deathAnimation.enabled = true;
            playSound.Stop();
            playSound.PlayOneShot(marioDie);

            GameController.Instance.ResetLevel(3f);
        }
        
        public void StageClear()
        {
            playSound.Stop();
            playSound.PlayOneShot(stageClear);
            // ResetLevel(5f);
        }

        private IEnumerator StarpowerAnimation()
        {
            starpower = true;

            float elapsed = 0f;
            float duration = 10f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                if (Time.frameCount % 4 == 0)
                {
                    activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                }

                yield return null;
            }

            activeRenderer.spriteRenderer.color = Color.white;
            starpower = false;
        }
    }
}
