using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum ActiveMask
    {
        NONE,
        CamMask,
        InvertInputsMask
    }
    #region Events 
    public UnityEvent<bool> gameOver = new UnityEvent<bool>();
    public UnityEvent jumpPlayer = new UnityEvent();
    public UnityEvent crouchPlayer = new UnityEvent();
    public UnityEvent standUpPlayer = new UnityEvent();
    public UnityEvent landingPlayer = new UnityEvent();
    public UnityEvent hitPlayer = new UnityEvent();
    public UnityEvent changeMaskBoss = new UnityEvent();
    public UnityEvent spellLaunch = new UnityEvent();
    public UnityEvent bossDeath = new UnityEvent();
    #endregion

    private bool isGameOver { get; set; } = false;
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] public float difficultyMultiplier = 1f;
    [SerializeField] public ActiveMask currentMask = ActiveMask.NONE;
    [SerializeField] public BossController bossController;


    [Header("Masks Settings")]
    [Header("Mask Random Change")]
    [SerializeField] private float minMaskTime = 6f;
    [SerializeField] private float maxMaskTime = 15f;
    [SerializeField] private float camFlipDuration = 0.6f;
    [SerializeField] private float maskRotationTime = 1f;


    [Header("Tile Settings")]
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int startTileCount = 3;
    [SerializeField] private float tileLength = 10f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Spawn Position")]
    [SerializeField] private float startZ = 0f;

    private List<GameObject> activeTiles = new List<GameObject>();
    private Coroutine camRoutine;
    private bool isCameraRotating = false;

    private bool isMaskCamActive = false;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnInitialTiles();
        currentMask = ActiveMask.NONE;
        StartCoroutine(RandomMaskRoutine());
    }

    void Update()
    {
        MoveTiles();
        difficultyMultiplier += Time.deltaTime * 0.01f;
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
        TileObstacleSpawner content = tile.GetComponent<TileObstacleSpawner>();
        if (content != null)
        {
            content.OnRecycle();
        }
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

    public void ChangeMask(ActiveMask newMask)
    {
        if (isCameraRotating) return;
        currentMask = newMask;
        print (message: "Mask changed to: " + newMask.ToString());
        switch (newMask)
        {
            case ActiveMask.CamMask:
                {
                    bossController.RotateToIndex(1, maskRotationTime);
                    ActiveCamMask(true); 
                    isMaskCamActive = true;
                }
                break;
            case ActiveMask.NONE:
                {
                    if (isMaskCamActive)
                    {
                        ActiveCamMask(false);
                        isMaskCamActive = false;
                    }
                    bossController.RotateToIndex(0, maskRotationTime);
                    

                }
                break;
            case ActiveMask.InvertInputsMask:
                {
                    if (isMaskCamActive)
                    {
                        ActiveCamMask(false);
                        isMaskCamActive = false;
                    }
                    bossController.RotateToIndex(2, maskRotationTime);

                }
                break;
        }

        changeMaskBoss.Invoke();
    }


    public void ActiveCamMask(bool isActive)
    {
        Quaternion targetRotation = isActive
            ? Quaternion.Euler(11.6f, 0f, 180f)
            : Quaternion.Euler(11.6f, 0f, 0f);

        if (camRoutine != null)
            StopCoroutine(camRoutine);

        camRoutine = StartCoroutine(RotateCameraSmooth(targetRotation));
    }

    void ChangeToRandomMask()
    {
        ActiveMask newMask;

        do
        {
            newMask = (ActiveMask)Random.Range(0, System.Enum.GetValues(typeof(ActiveMask)).Length);
        }
        while (newMask == currentMask);

        ChangeMask(newMask);
    }

    IEnumerator RandomMaskRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minMaskTime, maxMaskTime);
            waitTime /= GameManager.Instance.difficultyMultiplier;
            yield return new WaitForSecondsRealtime(waitTime);

            ChangeToRandomMask();
        }
    }

    IEnumerator RotateCameraSmooth(Quaternion target)
    {
        isCameraRotating = true;

        Camera cam = Camera.main;

    Quaternion start = cam.transform.localRotation;

    float t = 0f;
    while (t < 1f)
    {
        t += Time.deltaTime / camFlipDuration;
        cam.transform.localRotation = Quaternion.Slerp(start, target, t);
        yield return null;
    }

    cam.transform.localRotation = target;
        isCameraRotating = false;
    }


}
