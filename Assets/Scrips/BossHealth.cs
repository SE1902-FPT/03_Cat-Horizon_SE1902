using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Settings")]
    public int health = 10;

    [SerializeField] private GameObject winPanel;

    public void TakeDamage(int damage)
    {
        health -= damage;

        // --- DÒNG THÊM MỚI: Tăng điểm mỗi khi trúng đòn ---
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(1); // Cộng 1 điểm mỗi lần trúng
        }
        // ------------------------------------------------

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        Destroy(gameObject);
    }
}