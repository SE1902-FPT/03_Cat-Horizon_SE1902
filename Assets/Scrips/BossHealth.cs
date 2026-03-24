using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Settings")]
    public int health = 10;

    [SerializeField] private GameObject winPanel; // Kéo WinPanel vào đây

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss HP: " + health); // Để bạn theo dõi trong Console

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Hiện bảng thắng
            Time.timeScale = 0f;      // Dừng game để chúc mừng
        }

        // Boss tự tiêu diệt chính mình
        Destroy(gameObject);
    }
}