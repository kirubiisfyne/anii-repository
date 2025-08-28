using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Main Panel")]
    public GameObject settingsPanel;

    [Header("UI Elements")]
    public Slider masterVolumeSlider;

    [Header("Audio")]
    public AudioMixer audioMixer;

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        masterVolumeSlider.value = 1f; // Default to max volume
    }

    public void ShowSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        Debug.Log("Settings panel shown");
    }

    public void HideSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void SetMasterVolume(float value)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
            Debug.Log("Master Volume set to: " + value);
        }
    }
}