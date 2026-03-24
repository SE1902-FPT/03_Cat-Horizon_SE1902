using UnityEngine;

public class BossAtkController : MonoBehaviour
{

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Vector2 m_Direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction * Time.deltaTime * m_MoveSpeed);
    }

    public void Fire()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lấy script Health của Player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Gây 1 sát thương (vì MaxHealth là 1 nên sẽ chết luôn)
                playerHealth.TakeDamage(1);
            }

            // Biến mất viên đạn
            Destroy(gameObject);
        }
    }
}