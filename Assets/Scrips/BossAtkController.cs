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
        // Kiểm tra nếu chạm vào đối tượng có Tag là Enemy
        if (collision.CompareTag("Player"))
        {

            // 2. Tiêu diệt kẻ địch
            Destroy(collision.gameObject);

            // 3. Tiêu diệt chính viên đạn
            Destroy(gameObject);
        }
    }
}