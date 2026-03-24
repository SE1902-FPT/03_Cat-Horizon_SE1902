using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Settings")]
    public int health = 10; // Đặt là 10 hit trong Inspector

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Boss tự tiêu diệt chính mình khi hết máu
        Destroy(gameObject);
    }
}