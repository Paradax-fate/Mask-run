using UnityEngine;

public class TileObstacleSpawner : MonoBehaviour
{
    public Transform obstaclesContainer;
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    [Range(0f, 1f)] public float spawnChance = 0.6f;
    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {

        foreach (Transform child in obstaclesContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform point in spawnPoints)
        {
            if (Random.value <= spawnChance * GameManager.Instance.difficultyMultiplier)
            {
                Instantiate(
                    obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)],
                    point.position,
                    point.rotation,
                    obstaclesContainer
                );
            }
        }
    }

    public void OnRecycle()
    {
       SpawnObstacles();
    }
}
