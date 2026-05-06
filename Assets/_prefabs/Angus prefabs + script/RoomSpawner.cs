using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    [Header("Setup")]
    // Drag your multiple prefabs here
    public GameObject[] prefabsToAssign;
    // Drag your sequence of target objects/positions here
    public Transform[] spawnPoints;

    void Start()
    {
        AssignRandomPrefabs();
    }

    void AssignRandomPrefabs()
    {
        if (prefabsToAssign.Length == 0 || spawnPoints.Length == 0) return;

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Pick a random index
            int randomIndex = Random.Range(0, prefabsToAssign.Length);
            GameObject prefabToSpawn = prefabsToAssign[randomIndex];

            // Instantiate prefab at the spawn point's position/rotation
            GameObject instance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Optional: Parent the new object to the spawn point
            instance.transform.SetParent(spawnPoint);
        }
    }
}
