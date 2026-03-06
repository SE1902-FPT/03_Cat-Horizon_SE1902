using UnityEngine;

/// <summary>
/// Singleton GameManager — persists across scenes.
/// Stores and applies audio settings via PlayerPrefs.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ── PlayerPrefs keys ──
    private const string KEY_SOUND_VOL = "SoundVolume";
    private const string KEY_MUSIC_VOL = "MusicVolume";
    private const string KEY_MUSIC_ON = "MusicEnabled";

    // ── Current values (runtime) ──
    [Header("Audio Settings")]
    [Range(0f, 1f)] public float soundVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    public bool musicEnabled = true;

    // Optional: drag a background‑music AudioSource here in Inspector
    [Header("References")]
    public AudioSource bgmSource;

    // ───────────────────────── Unity lifecycle ─────────────────────────

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        ApplySettings();
    }

    // ───────────────────────── Public API ─────────────────────────

    public void SetSoundVolume(float vol)
    {
        soundVolume = Mathf.Clamp01(vol);
        AudioListener.volume = soundVolume;
        PlayerPrefs.SetFloat(KEY_SOUND_VOL, soundVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float vol)
    {
        musicVolume = Mathf.Clamp01(vol);
        if (bgmSource != null)
            bgmSource.volume = musicVolume;
        PlayerPrefs.SetFloat(KEY_MUSIC_VOL, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        if (bgmSource != null)
            bgmSource.mute = !musicEnabled;
        PlayerPrefs.SetInt(KEY_MUSIC_ON, musicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ───────────────────────── Helpers ─────────────────────────

    private void LoadSettings()
    {
        soundVolume = PlayerPrefs.GetFloat(KEY_SOUND_VOL, 1f);
        musicVolume = PlayerPrefs.GetFloat(KEY_MUSIC_VOL, 1f);
        musicEnabled = PlayerPrefs.GetInt(KEY_MUSIC_ON, 1) == 1;
    }

    private void ApplySettings()
    {
        AudioListener.volume = soundVolume;

        if (bgmSource != null)
        {
            bgmSource.volume = musicVolume;
            bgmSource.mute = !musicEnabled;
        }
    }
}
