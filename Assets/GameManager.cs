using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public Transform player;
    public Transform selectionManager;
    public GameObject gameOverMenu;

    public ScoreManager scoreManager;

    public static GameManager instance = null;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
        }
    }
    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
    }
    void Update()
    {
        if (player.GetComponent<PlayerController>().isDead)
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
        AudioManager.instance.gameObject.SetActive(false);


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
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        AudioListener.pause = true;
        gameOverMenu.SetActive(true);
    

    }
}
