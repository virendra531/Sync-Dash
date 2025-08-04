using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;
    public float segmentLength = 50f; // Length of each platform segment
    public int initialSegments = 3;
    public float recycleDistance = 50f; // Distance behind player to recycle
    private List<GameObject> platforms = new List<GameObject>();
    private Vector3 lastSpawnPosition;

    void Start()
    {
        lastSpawnPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Check if player is near the end of the last platform
        if (player.position.z > lastSpawnPosition.z - (initialSegments * segmentLength / 2))
        {
            SpawnPlatform();
        }

        // Recycle platforms behind the player
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.z < player.position.z - recycleDistance)
            {
                GameObject platform = platforms[i];
                platforms.RemoveAt(i);
                platform.transform.position = lastSpawnPosition;
                platforms.Add(platform);
                lastSpawnPosition += new Vector3(0, 0, segmentLength);
            }
        }
    }

    void SpawnPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, lastSpawnPosition, Quaternion.identity);
        platforms.Add(platform);
        lastSpawnPosition += new Vector3(0, 0, segmentLength);
    }
}