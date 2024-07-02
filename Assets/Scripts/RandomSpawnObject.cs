using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObject : MonoBehaviour
{
    // List of object prefabs to spawn
    public List<GameObject> objectPrefabs;

    // List of spawn positions
    public List<Transform> spawnPositions;

    void Start()
    {
        // Spawn a random object at a random position
        SpawnRandomObjectAtRandomPosition();
    }

    void SpawnRandomObjectAtRandomPosition()
    {
        // Check if the objectPrefabs list is not empty
        if (objectPrefabs.Count == 0)
        {
            Debug.LogWarning("No objects to spawn. Add prefabs to the objectPrefabs list.");
            return;
        }

        // Check if the spawnPositions list is not empty
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("No spawn positions defined. Add positions to the spawnPositions list.");
            return;
        }
        // Create a list to track used spawn positions
        List<Transform> availableSpawnPositions = new List<Transform>(spawnPositions);

        // Spawn objects ensuring no two objects are spawned at the same location
        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            if (availableSpawnPositions.Count == 0)
            {
                Debug.LogWarning("Not enough spawn positions for all objects. Some objects will not be spawned.");
                return;
            }

            // Get a random spawn position from the available positions
            int randomPositionIndex = Random.Range(0, availableSpawnPositions.Count);
            Transform randomSpawnPosition = availableSpawnPositions[randomPositionIndex];

            // Get a random object prefab from the list
            GameObject randomObjectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            // Instantiate the object at the random position
            Instantiate(randomObjectPrefab, randomSpawnPosition.position, randomSpawnPosition.rotation);

            // Remove the used spawn position from the list
            availableSpawnPositions.RemoveAt(randomPositionIndex);
        }


    }
}
