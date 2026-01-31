using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private bool isGameOver { get; set; } = false;

    [Header("UI Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Tile Settings")]
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int startTileCount = 3;
    [SerializeField] private float tileLength = 10f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Spawn Position")]
    [SerializeField] private float startZ = 0f;

    private List<GameObject> activeTiles = new List<GameObject>();


    private void Start()
    {
        SpawnInitialTiles();
    }

    void Update()
    {
        MoveTiles();
    }

    // ─── Tile Management ───
    void SpawnInitialTiles()
    {
        float spawnZ = startZ;

        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile(spawnZ);
            spawnZ += tileLength;
        }
    }

    void MoveTiles()
    {
        for (int i = 0; i < activeTiles.Count; i++)
        {
            activeTiles[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            // Si la tile salió completamente
            if (activeTiles[i].transform.position.z < -tileLength)
            {
                RecycleTile(activeTiles[i]);
            }
        }
    }

    void SpawnTile(float zPosition)
    {
        GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        GameObject tile = Instantiate(prefab, new Vector3(0f, 0f, zPosition), Quaternion.identity);
        activeTiles.Add(tile);
    }

    void RecycleTile(GameObject tile)
    {
        float rightMostZ = GetRightMostZ();
        tile.transform.position = new Vector3(
            tile.transform.position.x,
            tile.transform.position.y,
            rightMostZ + tileLength
        );
    }

    float GetRightMostZ()
    {
        float maxZ = activeTiles[0].transform.position.z;

        foreach (GameObject tile in activeTiles)
        {
            if (tile.transform.position.z > maxZ)
                maxZ = tile.transform.position.z;
        }

        return maxZ;
    }

    // Método para pausar y reanudar el juego
    public void TogglePauseMenu()
    {
        bool paused = pauseMenu.activeSelf;
        pauseMenu.SetActive(!paused);
        Time.timeScale = paused ? 1f : 0f;
    }
    private void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    #region Events 
    public UnityEvent<bool> gameOver = new UnityEvent<bool>();
    #endregion

    private void MoveScene()
    {
        // Movimiento de tiles para simular avance
    }


}
