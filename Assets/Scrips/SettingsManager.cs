using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the Setting scene UI.
/// Reads current values from GameManager on Start,
/// pushes changes back to GameManager when the user interacts.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider soundSlider;
    public Slider musicSlider;
    public Toggle musicToggle;
    public Button backButton;

    [Header("Value Labels (optional)")]
    public TextMeshProUGUI soundValueText;
    public TextMeshProUGUI musicValueText;

    // ───────────────────────── Unity lifecycle ─────────────────────────

    private void Start()
    {
        // ── Load current settings into UI ──
        if (GameManager.Instance != null)
        {
            if (soundSlider != null)
                soundSlider.value = GameManager.Instance.soundVolume;
            if (musicSlider != null)
                musicSlider.value = GameManager.Instance.musicVolume;
            if (musicToggle != null)
                musicToggle.isOn = GameManager.Instance.musicEnabled;
        }

        // ── Wire up listeners ──
        if (soundSlider != null)
            soundSlider.onValueChanged.AddListener(OnSoundChanged);
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        if (musicToggle != null)
            musicToggle.onValueChanged.AddListener(OnMusicToggled);
        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);

        // ── Initial label update ──
        UpdateLabels();
    }

    // ───────────────────────── Callbacks ─────────────────────────

    private void OnSoundChanged(float value)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetSoundVolume(value);
        UpdateLabels();
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetMusicVolume(value);
        UpdateLabels();
    }

    private void OnMusicToggled(bool isOn)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetMusicEnabled(isOn);
    }

    private void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ───────────────────────── Helpers ─────────────────────────

    private void UpdateLabels()
    {
        if (soundValueText != null && soundSlider != null)
            soundValueText.text = Mathf.RoundToInt(soundSlider.value * 100f) + "%";
        if (musicValueText != null && musicSlider != null)
            musicValueText.text = Mathf.RoundToInt(musicSlider.value * 100f) + "%";
    }
}
