using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsPanelController : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void SetMasterVolume(float volume)
    {
        Debug.Log($"Setting MasterVolume to: {volume} dB");
        mainMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}