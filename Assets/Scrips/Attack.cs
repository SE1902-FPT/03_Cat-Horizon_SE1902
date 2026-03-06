using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Vector2 direction;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Flip sprite / animation theo hướng bắn
        if (direction.x < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}