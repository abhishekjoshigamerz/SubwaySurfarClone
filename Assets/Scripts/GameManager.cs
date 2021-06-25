
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public static bool gameOver=false;
    public static int numberOfCoins=0;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] TextMeshProUGUI coinsText;
    void Start()
    {
        gameOver=false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver){
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins:" + numberOfCoins;
    }
}
