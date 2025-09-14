using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject transition;
    public Animation transitionAnimation;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(int sceneIndex)
    {

        StartCoroutine(FadeOut(sceneIndex));
    }

    private IEnumerator FadeIn()
    {
        transitionAnimation.Play("anim_sceneIn");
        yield return new WaitForSecondsRealtime(1f);
        transition.SetActive(false);
    }

    private IEnumerator FadeOut(int sceneIndex)
    {
        transition.SetActive(true);
        transitionAnimation.Play("anim_sceneOut");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
