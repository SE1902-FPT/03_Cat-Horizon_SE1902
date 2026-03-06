using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Xoay viên đạn theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // --- PHẦN THÊM MỚI: Xử lý va chạm ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu chạm vào đối tượng có Tag là Enemy
        if (collision.CompareTag("Enemy"))
        {
            // 1. Cộng điểm thông qua ScoreManager
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(1);
            }

            // 2. Tiêu diệt kẻ địch
            Destroy(collision.gameObject);

            // 3. Tiêu diệt chính viên đạn
            Destroy(gameObject);
        }
    }
}