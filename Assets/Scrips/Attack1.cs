using UnityEngine;

public class Attack1 : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lấy component PlayerHealth từ Player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Trừ 1 HP
            }

            Destroy(gameObject); // Chỉ hủy đạn/attack, KHÔNG hủy Player
        }
    }
}