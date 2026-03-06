using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [Header("UI Components")]
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        // Khởi tạo giá trị thanh trượt (slider) từ PlayerPrefs
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (soundSlider != null)
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
            soundSlider.onValueChanged.AddListener(SetSoundVolume);
        }
    }

    // Gửi giá trị cho GameManager khi người dùng kéo thanh trượt Music
    public void SetMusicVolume(float volume)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetMusicVolume(volume);
        }
    }

    // Gửi giá trị cho GameManager khi người dùng kéo thanh trượt Sound
    public void SetSoundVolume(float volume)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetSoundVolume(volume);
        }
    }

    // Hàm gọi khi nhấn nút Back
    public void BackToMainMenu()
    {
        // Giả sử Main Menu có tên Scene là "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }
}
