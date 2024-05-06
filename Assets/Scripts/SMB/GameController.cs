using UnityEngine;
using UnityEngine.SceneManagement;

namespace SMB
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public AudioSource playSound;
        public AudioClip stageClear;
    
        public int world { get; private set;  }
        public int stage { get; private set;  }
        public int lives { get; private set;  }
        public int coins { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            NewGame();
        }

        private void NewGame()
        {
            lives = 3;
            coins = 0;
            
            
            LoadLevel(1, 1);
        }

        private void LoadLevel(int world, int stage)
        {
            this.world = world;
            this.stage = stage;

            SceneManager.LoadScene(this.world-1);
        }

        public void ResetLevel(float delay)
        {
            Invoke(nameof(ResetLevel), delay);
        }

        public void ResetLevel()
        {
            lives--;
            

            if (lives > 0)
            {
                LoadLevel(world, stage);
            }
            else
            {
                GameOver();
            }
        }

        public void NextLevel()
        {
            
        }

        private void GameOver()
        {
            Invoke(nameof(NewGame), 3f);
        }
        
        public void AddCoin()
        {
            coins++;

            if (coins == 100)
            {
                coins = 0;
                AddLife();
            }
        }
        
        public void AddLife()
        {
            lives++;
        }
    }
}
