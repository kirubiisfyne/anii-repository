using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject sceneTransition;
    private bool isPaused = false;

    public AudioMixer mainMixer;

    void Update()
    {
        // Toggle pause on Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void SetVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        mainMixer.SetFloat("MasterVolume", dB);
    }

    public void Pause()
    {
        StartCoroutine(StartPause());
    }

    private IEnumerator StartPause()
    {
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<Animation>().Play("anim_pauseIn");
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;       // Stops all time-based updates
        isPaused = true;
    }

    public void Resume()
    {
        StartCoroutine(StartResume());
    }

    private IEnumerator StartResume()
    {
        Time.timeScale = 1f;
        pauseMenu.GetComponent<Animation>().Play("anim_pauseOut");
        yield return new WaitForSecondsRealtime(0.5f);
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMenu()
    {
        StartCoroutine(StartReturnToMenu());
    }

    private IEnumerator StartReturnToMenu()
    {
        Time.timeScale = 1f;

        sceneTransition.SetActive(true);
        sceneTransition.GetComponent<Animation>().Play("anim_sceneOut");
        yield return new WaitForSecondsRealtime(1.25f);
        SceneManager.LoadScene(1);
    }
}
