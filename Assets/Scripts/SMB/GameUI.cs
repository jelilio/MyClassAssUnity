using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace SMB
{
    public class GameUI : MonoBehaviour
    {
        public TMP_Text scoreText;
        public TMP_Text coinsText;
        public TMP_Text livesText;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            livesText.text = "" + GameController.Instance.lives;
            coinsText.text = "" + GameController.Instance.coins;
        }
    }
}
