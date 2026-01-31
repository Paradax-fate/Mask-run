using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Transform  maskRotator;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;

    [Header("Floating")]
    [SerializeField] private float floatAmplitude = 0.5f; // altura
    [SerializeField] private float floatFrequency = 2f;   // velocidad

    private Vector3 startPosition;
    private int direction = 1; // 1 = derecha, -1 = izquierda
    void Awake()
    {
        startPosition = transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoss();
    }

    void MoveBoss()
    {
        // ─── Movimiento horizontal limitado ───
        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;

        if (newX >= maxX)
        {
            newX = maxX;
            direction = -1;
        }
        else if (newX <= minX)
        {
            newX = minX;
            direction = 1;
        }

        // ─── Flotación vertical ───
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = new Vector3(
            newX,
            startPosition.y + yOffset,
            transform.position.z
        );
    }
}
