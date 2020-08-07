using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public Transform player;
    public Transform selectionManager;
    public GameObject gameOverMenu;

    public ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
    }
    void Update()
    {
        if (player.GetComponent<PlayerController>().isDead && !isGameOver)
        {
            OnGameOver();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;

        selectionManager.GetComponent<SelectionController>().enabled = false;
        
        Camera.main.transform.GetComponent<CameraController>().enabled = false;


        if (PlayerPrefs.GetInt("HighScore") < scoreManager.score)
        {
            PlayerPrefs.SetInt("HighScore", scoreManager.score);
        }

        ShowGameOverMenu();

    }

    public void Restart()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowGameOverMenu()
    {
       
        Time.timeScale = 0;
        AudioListener.pause = true;
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

    }
}
