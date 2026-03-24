using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public float moveSpeed = 3f;

    Transform[] waypoints;
    int index = 0;

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
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[index];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            index++;

            if (index >= waypoints.Length)
                index = 0;
        }
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

    public void Init(Transform[] path)
    {
        waypoints = path;
        transform.position = path[0].position;
    }

    void OnDestroy()
    {
        if (WaveSpawner.instance != null)
        {
            WaveSpawner.instance.EnemyDied();
        }
    }
}