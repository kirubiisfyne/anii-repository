using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer mainMixer;

    [Header("Full Screen Toggle")]
    public Toggle fullScreenToggle;

    [Header("DropDown Quality")]
    public TMP_Dropdown dropdown;

    private void Start()
    {
        // Full Screen Toggle
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Quality Settings
        dropdown.value = QualitySettings.GetQualityLevel();
        dropdown.RefreshShownValue();

        dropdown.onValueChanged.AddListener(SetQuality);
    }
    private void SetVolume(string parameter, float value01)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value01, 0.0001f, 1f)) * 20f;
        mainMixer.SetFloat(parameter, dB);
    }

    public void SetMasterVolume(float value) => SetVolume("MasterVolume", value);
    public void SetMusicVolume(float value) => SetVolume("MusicVolume", value);
    public void SetSFXVolume(float value) => SetVolume("SFXVolume", value);

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
        
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}