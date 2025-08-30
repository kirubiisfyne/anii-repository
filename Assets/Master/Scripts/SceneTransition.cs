using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvas;   // assign FadeOverlay here
    public float fadeDuration = 1f;  // how long the fade lasts

    void Start()
    {

        StartCoroutine(FadeIn());
    }

    public void FadeToScene(int sceneIndex)
    {

        StartCoroutine(FadeOut(sceneIndex));
    }

    IEnumerator FadeIn()
    {
        fadeCanvas.alpha = 1; 
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut(int sceneIndex)
    {
        fadeCanvas.alpha = 0; 
        while (fadeCanvas.alpha < 1)
        {
            fadeCanvas.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
