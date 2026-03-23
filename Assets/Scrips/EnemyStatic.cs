using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public float moveSpeed = 5f;

    Vector3 targetPosition;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 2f;

    float fireTimer;

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCooldown)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            Vector2 direction = player.position - firePoint.position;

            bullet.GetComponent<Attack1>().SetDirection(direction);

            fireTimer = 0;
        }
    }

    public void Init(Vector3 target)
    {
        targetPosition = target;
    }

    void OnDestroy()
    {
        if (WaveSpawner.instance != null)
        {
            WaveSpawner.instance.EnemyDied();
        }
    }
}