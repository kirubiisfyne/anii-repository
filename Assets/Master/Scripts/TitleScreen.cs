using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

     public GameObject settingsPanel;
    public void StartGame()
    {
        Debug.Log("Start button was clicked!");
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button was clicked!");
        Application.Quit();
    }

     public void OpenSettings()
    {
        
        Debug.Log("Settings button was clicked!");
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

}