using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // List of enemy prefabs to spawn
    public List<GameObject> enemyPrefabs;

    // List of spawn points
    public List<Transform> spawnPoints;

    // Time between spawns
    public float spawnInterval = 2f;

    // Total number of enemies to spawn
    public int totalEnemies = 20;

    private int enemiesSpawned = 0;

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < totalEnemies)
        {
            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Get a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Get a random enemy prefab
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Instantiate the enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Increment the number of spawned enemies
            enemiesSpawned++;
        }
    }
}
