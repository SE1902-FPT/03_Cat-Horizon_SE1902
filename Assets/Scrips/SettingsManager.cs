using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle musicToggle;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = volume;
        AudioListener.volume = volume;

        bool music = PlayerPrefs.GetInt("music", 1) == 1;
        musicToggle.isOn = music;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt("music", musicToggle.isOn ? 1 : 0);
    }
}