using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject settingsPanel;

    public GameObject sceneTransitionGameObject;
    public SceneTransition sceneTransition;   
    public void StartGame()
    {
        Debug.Log("Start button was clicked!");
        if (sceneTransition != null)
        {
            sceneTransition.FadeToScene(2);   
        }
        else
        {
            Debug.LogWarning("SceneTransition not assigned!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit button was clicked!");
        StartCoroutine(StartQuitGame());
    }

    private IEnumerator StartQuitGame()
    {
        sceneTransitionGameObject.SetActive(true);
        Animation sceneTransitionAnimation = sceneTransitionGameObject.GetComponent<Animation>();

        sceneTransitionAnimation.Play("anim_sceneOut");
        yield return new WaitForSecondsRealtime(2f);

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
            StartCoroutine(StartCloseSettings());
            
    }

    private IEnumerator StartCloseSettings()
    {
        Animation settingPannelAnimation = settingsPanel.GetComponent<Animation>();

        settingPannelAnimation.Play("anim_settingsOut");
        yield return new WaitForSecondsRealtime(0.5f);
        settingsPanel.SetActive(false);
    } 
}
