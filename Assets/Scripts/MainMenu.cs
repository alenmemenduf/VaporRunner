using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject instructionMenu;
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

    public void ShowInstructions()
    {
        mainMenuUI.SetActive(false);
        instructionMenu.SetActive(true);
    }
    public void HideInstructions()
    {
        mainMenuUI.SetActive(true);
        instructionMenu.SetActive(false);
    }
}
