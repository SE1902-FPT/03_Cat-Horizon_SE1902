using UnityEngine;

public class EnemyCharge : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private Vector3 moveDirection;

    void Start()
    {
        // 1. Tìm đối tượng người chơi (đảm bảo Player của bạn có Tag là "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 2. Tính toán hướng từ Enemy đến Player
            moveDirection = (player.transform.position - transform.position).normalized;

            // 3. Xoay quái vật hướng về phía người chơi (tùy chọn)
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // -90 tùy vào hướng sprite
        }
        else
        {
            // Nếu không thấy Player, đi thẳng xuống dưới
            moveDirection = Vector3.down;
        }

        // 4. Tự hủy sau 5 giây để tránh rác bộ nhớ nếu không ra khỏi màn hình
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Di chuyển liên tục theo hướng đã xác định ban đầu (đường thẳng)
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    // --- PHẦN BỔ SUNG: Xử lý va chạm ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu va chạm với đối tượng có Tag là Player
        if (collision.CompareTag("Player"))
        {
            // Gọi hàm TakeDamage từ script PlayerHealth của người chơi
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            // Quái vật biến mất sau khi đâm trúng
            Destroy(gameObject);
        }
    }

    // Tự hủy khi đi ra khỏi vùng nhìn của Camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}