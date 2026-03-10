using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform[] waypoints;

    int index;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 2f;

    float tempCooldown;

    void Start()
    {
        tempCooldown = Random.Range(0.5f, fireCooldown);
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        int next = index + 1;

        if (next >= waypoints.Length)
            next = 0;

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[next].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, waypoints[next].position) < 0.1f)
            index = next;
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

    public void Init(Transform[] path)
    {
        waypoints = path;
        transform.position = path[0].position;
    }

    void OnDestroy()
    {
        FindObjectOfType<WaveSpawner>().EnemyDied();
    }
}