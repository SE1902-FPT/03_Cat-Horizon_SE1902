using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 targetPosition;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 2f;

    float tempCooldown;
    bool reached = false;

    void Start()
    {
        tempCooldown = Random.Range(0.5f, fireCooldown);
    }

    void Update()
    {
        if (!reached)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                reached = true;
        }
        else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (tempCooldown <= 0)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            tempCooldown = fireCooldown;
        }

        tempCooldown -= Time.deltaTime;
    }

    public void Init(Vector3 target)
    {
        targetPosition = target;
    }

    void OnDestroy()
    {
        FindObjectOfType<WaveSpawner>().EnemyDied();
    }
}