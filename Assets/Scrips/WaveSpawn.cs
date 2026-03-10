using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public EnemyStatic enemyPrefab;
    public EnemyPath enemyPathPrefab;

    public Transform[] frontSpawn;
    public Transform[] backSpawn;

    public Transform[] path;

    int wave = 1;
    int enemiesAlive;

    void Start()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        if (wave == 1)
        {
            SpawnStatic(frontSpawn, 4);
        }
        else if (wave == 2)
        {
            SpawnStatic(backSpawn, 4);
        }
        else if (wave == 3)
        {
            SpawnStatic(frontSpawn, 2);
            SpawnStatic(backSpawn, 2);
        }
        else if (wave == 4)
        {
            SpawnPathEnemy(3);
        }
    }

    void SpawnStatic(Transform[] spawnPoints, int count)
    {
        enemiesAlive += count;

        for (int i = 0; i < count; i++)
        {
            Transform spawn = spawnPoints[i % spawnPoints.Length];

            EnemyStatic enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

            Vector3 target = spawn.position + new Vector3(0, -3f, 0);

            enemy.Init(target);
        }
    }

    void SpawnPathEnemy(int count)
    {
        enemiesAlive += count;

        for (int i = 0; i < count; i++)
        {
            EnemyPath enemy = Instantiate(enemyPathPrefab);

            enemy.Init(path);
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            wave++;
            SpawnWave();
        }
    }
}