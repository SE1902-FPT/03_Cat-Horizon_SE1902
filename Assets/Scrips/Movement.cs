using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;

    // --- PHẦN THÊM MỚI ---
    // Tạo biến static để lưu hướng di chuyển (Trái: -1, Phải: 1, Đứng yên: 0)
    // Các script khác có thể đọc biến này bằng cách gọi: Movement.HorizontalInput
    public static float HorizontalInput { get; private set; }
    // ---------------------

        
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
        }
    }
