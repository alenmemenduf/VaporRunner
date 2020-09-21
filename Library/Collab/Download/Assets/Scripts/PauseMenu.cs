using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public PlayerMovement player;

    [SerializeField] private bool isPaused = false;

    public CameraController camController;

    public Slider sensitivitySlider;

    private void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused && !GameManager.instance.isGameOver)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }

        
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        player.enabled = false;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        player.enabled = true;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        camController.mouseSensitivity = sensitivitySlider.value;
    }
}
