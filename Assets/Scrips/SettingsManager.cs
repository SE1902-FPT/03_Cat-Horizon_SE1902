using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Settings")]
    public Slider volumeSlider;
    public Slider sfxSlider;

    [Header("Music Toggle")]
    public Toggle musicToggle;

    [Header("Display Settings")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        // Volume
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
        }
        AudioListener.volume = volume;

        // SFX
        float sfx = PlayerPrefs.GetFloat("sfx", 1f);
        if (sfxSlider != null)
        {
            sfxSlider.value = sfx;
        }

        // Music toggle
        bool music = PlayerPrefs.GetInt("music", 1) == 1;
        if (musicToggle != null)
        {
            musicToggle.isOn = music;
        }

        // Resolution
        SetupResolutions();

        // Fullscreen
        bool isFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = isFullscreen;
        }
        Screen.fullScreen = isFullscreen;
    }

    private void SetupResolutions()
    {
        if (resolutionDropdown == null) return;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        int savedRes = PlayerPrefs.GetInt("resolution", currentResolutionIndex);
        resolutionDropdown.value = savedRes;
        resolutionDropdown.RefreshShownValue();
    }

    public void ChangeVolume()
    {
        if (volumeSlider != null)
        {
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("volume", volumeSlider.value);
        }
    }

    public void ChangeSFXVolume()
    {
        if (sfxSlider != null)
        {
            PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        }
    }

    public void ToggleMusic()
    {
        if (musicToggle != null)
        {
            PlayerPrefs.SetInt("music", musicToggle.isOn ? 1 : 0);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions != null && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt("resolution", resolutionIndex);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
}
