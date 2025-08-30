using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject settingsPanel;       
    public SceneTransition sceneTransition;   
    public void StartGame()
    {
        Debug.Log("Start button was clicked!");
        if (sceneTransition != null)
        {
            sceneTransition.FadeToScene(1);   
        }
        else
        {
            Debug.LogWarning("SceneTransition not assigned!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit button was clicked!");
        Application.Quit();
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button was clicked!");
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}
