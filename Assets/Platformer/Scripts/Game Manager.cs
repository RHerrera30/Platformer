using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    private int coinCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 300 - (int)Time.time;
        timerText.text = $"Time Left: {timeLeft}";
    }

    public void addCoin()
    {
        coinCount++;
        if (coinCount < 10)
        {
            coinText.text = "x0" + coinCount.ToString();
        } else if (coinCount >= 10)
        {
            coinText.text = "x" + coinCount.ToString();
        }
    }
}
