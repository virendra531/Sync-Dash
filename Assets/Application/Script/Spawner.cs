using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public ObjectPool orbPool;
    public PlayerController player;
    public float baseSpawnInterval = 2f;
    public float spawnDistance = 50f;
    public float recycleDistance = 50f; // Distance behind player to recycle
    private float timer = 0f;

    void Start()
    {
        // Initial spawn
        Spawn();
    }

    void Update()
    {
        // Adjust spawn interval based on player's current speed
        float speedRatio = player.currentSpeed / player.baseSpeed;
        float adjustedSpawnInterval = baseSpawnInterval / speedRatio;

        timer += Time.deltaTime;
        if (timer >= adjustedSpawnInterval)
        {
            timer = 0f;
            Spawn();
        }

        // Recycle objects behind the player
        foreach (GameObject obj in obstaclePool.pool)
        {
            if (obj.activeInHierarchy && obj.transform.position.z < player.transform.position.z - recycleDistance)
            {
                obstaclePool.ReturnObject(obj);
            }
        }
        foreach (GameObject obj in orbPool.pool)
        {
            if (obj.activeInHierarchy && obj.transform.position.z < player.transform.position.z - recycleDistance)
            {
                orbPool.ReturnObject(obj);
            }
        }
    }

    void Spawn()
    {
        float spawnZ = player.transform.position.z + spawnDistance;

        // Spawn obstacle with random X position
        GameObject obstacle = obstaclePool.GetObject();
        if (obstacle != null)
        {
            obstacle.transform.position = new Vector3(Random.Range(-5f, 5f), 0.5f, spawnZ);
        }

        // Spawn orb in player's path (X close to 0, slight variation)
        GameObject orb = orbPool.GetObject();
        if (orb != null)
        {
            orb.transform.position = new Vector3(Random.Range(-1f, 1f), 1f, spawnZ + Random.Range(-5f, 5f));
        }
    }
}