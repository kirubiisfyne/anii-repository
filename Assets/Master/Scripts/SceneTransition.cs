using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject transition;
    public Animation animation;

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
        animation.Play("anim_sceneIn");
        yield return new WaitForSecondsRealtime(2f);
        transition.SetActive(false);
    }

    IEnumerator FadeOut(int sceneIndex)
    {
        transition.SetActive(true);
        animation.Play("anim_sceneOut");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
