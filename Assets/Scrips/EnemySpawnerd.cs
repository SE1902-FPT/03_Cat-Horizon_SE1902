using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Kéo Prefab quái vào đây
    [SerializeField] private float spawnRate = 2f;    // Thời gian giữa mỗi lần tạo quái
    [SerializeField] private float xRange = 8f;       // Độ rộng vùng spawn (trục X)
    [SerializeField] private float spawnY = 6f;       // Độ cao vùng spawn (trục Y - ngoài màn hình)

    void Start()
    {
        // Gọi hàm Spawn liên tục
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Tạo vị trí ngẫu nhiên theo chiều ngang
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnY, 0);

        // Tạo quái vật tại vị trí đó
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}