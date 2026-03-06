using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Animator animator;
    [SerializeField] private Attack attack;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float firingCooldown;

    private float tempCooldown;
    private Rigidbody2D rb;

    // --- PHẦN THÊM MỚI ---
    // Tạo biến static để lưu hướng di chuyển (Trái: -1, Phải: 1, Đứng yên: 0)
    // Các script khác có thể đọc biến này bằng cách gọi: Movement.HorizontalInput
    public static float HorizontalInput { get; private set; }
    // ---------------------

    private Vector2 lastDirection = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- THAY ĐỔI ---
        // Gán giá trị Input vào biến static thay vì chỉ dùng biến cục bộ
        HorizontalInput = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // ----------------

        bool isMoving = HorizontalInput != 0f || vertical != 0f;
        animator.SetBool("isWalk", isMoving);

        if (HorizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (HorizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector2 direction = new Vector2(HorizontalInput, vertical);
        transform.Translate(direction * Time.deltaTime * speed);

        if (Input.GetMouseButtonDown(0))
        {
            if (tempCooldown <= 0)
            {
                Fire();
                tempCooldown = firingCooldown;
            }
        }
        tempCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        Instantiate(attack, firingPoint.position, Quaternion.identity, null);
    }
}
