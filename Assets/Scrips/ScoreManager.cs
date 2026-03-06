using UnityEngine;
using TMPro; // Sử dụng TextMeshPro để hiển thị điểm

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Awake()
    {
        // Singleton để dễ dàng truy cập từ các script khác
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }
}