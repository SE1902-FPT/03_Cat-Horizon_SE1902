using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private Attack attack;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float firingCooldown = 0.3f;

    private float tempCooldown;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 lastDirection = Vector2.right; // mặc định quay phải

    private float firePointX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        firePointX = firingPoint.localPosition.x;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(h, v);

        // Lưu hướng cuối cùng
        if (moveDir != Vector2.zero)
            lastDirection = moveDir.normalized;

        // Animation chạy
        animator.SetBool("isWalk", moveDir != Vector2.zero);

        // Flip sprite Player (KHÔNG flip transform)
        if (h > 0)
        {
            sr.flipX = false;
            firingPoint.localPosition = new Vector3(firePointX, firingPoint.localPosition.y, 0);
        }
        else if (h < 0)
        {
            sr.flipX = true;
            firingPoint.localPosition = new Vector3(-firePointX, firingPoint.localPosition.y, 0);
        }
        // Di chuyển
        transform.Translate(moveDir * speed * Time.deltaTime);

        // Bắn (vừa chạy vừa bắn)
        if (Input.GetMouseButton(0) && tempCooldown <= 0f)
        {
            Fire();
            tempCooldown = firingCooldown;
        }

        tempCooldown -= Time.deltaTime;
    }

    private void Fire()
    {
        Attack bullet = Instantiate(attack, firingPoint.position, Quaternion.identity);
        bullet.SetDirection(lastDirection);
    }
}