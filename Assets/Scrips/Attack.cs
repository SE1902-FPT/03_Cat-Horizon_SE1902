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
}