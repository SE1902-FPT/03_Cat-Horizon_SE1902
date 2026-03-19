using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider musicSlider;
    public Slider soundSlider;

    [Header("Navigation")]
    public string mainMenuSceneName = "MainMenu"; // Điền tên Scene Main Menu của bạn vào đây

    void Start()
    {
        // Khởi tạo giá trị ban đầu cho các thanh trượt (Sliders)
        if (GameManager.instance != null)
        {
            musicSlider.value = GameManager.instance.musicVolume;
            soundSlider.value = GameManager.instance.soundVolume;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        }

        // Thêm sự kiện khi người dùng kéo Slider
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetMusicVolume(volume);
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void OnSoundVolumeChanged(float volume)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetSoundVolume(volume);
        }
        else
        {
            PlayerPrefs.SetFloat("SoundVolume", volume);
        }
    }

    public void BackToMainMenu()
    {
        // Lưu lại mọi thay đổi vào thiết bị và tải Scene Main Menu
        PlayerPrefs.Save();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
