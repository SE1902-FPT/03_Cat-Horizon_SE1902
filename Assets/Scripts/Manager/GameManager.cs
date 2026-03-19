using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio Settings")]
    public AudioMixer audioMixer; // Dùng để điều chỉnh AudioMixer

    private void Awake()
    {
        // Khởi tạo Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ GameManager không bị hủy khi đổi Scene
            LoadSettings(); // Tải cấu hình khi game vừa bật
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadSettings()
    {
        // Tải cấu hình từ PlayerPrefs, mặc định là 1 (tối đa) nếu chưa có
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        SetMusicVolume(musicVolume);
        SetSoundVolume(soundVolume);
    }

    // Đặt âm lượng nhạc nền
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        if (audioMixer != null)
        {
            // Chuyển đổi volume (0.0001 -> 1) sang decibel (-80 -> 0)
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume > 0.0001f ? volume : 0.0001f) * 20);
        }
    }

    // Đặt âm lượng hiệu ứng âm thanh
    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundVolume", volume);
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume > 0.0001f ? volume : 0.0001f) * 20);
        }
    }
}
