using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
