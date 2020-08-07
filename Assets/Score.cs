using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;

    public ScoreManager scoreManager;
    private void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    void Update()
    {
        scoreText.text = scoreManager.score.ToString();
       
    }
}
