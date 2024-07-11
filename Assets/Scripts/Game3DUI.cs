using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game3Dui : MonoBehaviour
{
    public Game3DManager gameManager;
    
    public TMP_Text escapesText;
    public TMP_Text catchesText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Game3DManager>();
    }

    // Update is called once per frame
    void Update()
    {
        escapesText.text = "Escapes: " + gameManager.escapes;
        catchesText.text = "Catches: " + gameManager.catches;
    }
}
