using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winPanel; // Kéo WinPanel vào đây

    void Awake()
    {
        instance = this;
        // Đảm bảo WinPanel luôn tắt khi bắt đầu game
        if (winPanel != null) winPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();

        // Kiểm tra nếu đạt đủ 15 điểm
        if (score >= 10)
        {
            ShowWinWindow();
        }
    }

    void ShowWinWindow()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Hiện cửa sổ chiến thắng
            Time.timeScale = 0f;      // Tạm dừng trò chơi (tùy chọn)
        }
    }
}