using System.Collections;
using System.Xml;
using TMPro;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public float setTime = 120f;
    public TMP_Text TMPText;
    public Animation timerAnimation;

    private float timer;
    private bool isRunning = true;

    private void OnEnable()
    {
        isRunning = true;
    }

    void Start()
    {
        timer = setTime;
    }

    private void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            isRunning = false; // stop at zero

            StartCoroutine(StartTimerOut());
        }

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        string formatted = string.Format("{0:00}:{1:00}", minutes, seconds);

        TMPText.text = formatted;
    }

    private IEnumerator StartTimerOut()
    {
        timerAnimation.Play("anim_timerOut");
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
    }
}
