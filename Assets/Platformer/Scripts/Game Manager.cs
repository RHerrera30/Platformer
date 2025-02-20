using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    
    private int coinCount = 0;
    private int score = 0;
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
        int coinPoints = 200;
        coinCount++;
        if (coinCount < 10)
        {
            coinText.text = "x0" + coinCount.ToString();
        } else if (coinCount >= 10)
        {
            coinText.text = "x" + coinCount.ToString();
        }
        updateScore(coinPoints);
    }

    public void updateScore(int points)
    {
        score += points;
        if (score < 10)
        {
            scoreText.text = "Mario \n" + "0000" + score.ToString();
            
        } else if (score >= 10 && score < 100)
        {
            scoreText.text = "Mario \n" + "000" + score.ToString();
            
        } else if (score >= 100 && score < 1000)
        {
            scoreText.text = "Mario \n" + "00" + score.ToString();
            
        } else if (score >= 1000 && score < 10000)
        {
            scoreText.text = "Mario \n" + "0" + score.ToString();
        }
        else
        {
            scoreText.text = "Mario \n" + score.ToString();
        }
    }
}
