using UnityEngine;

public class BossMaskController : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Transform maskRotator;
    [SerializeField] private float minX = -0.7f;
    [SerializeField] private float maxX = 0.7f;

    [Header("Floating")]
    [SerializeField] private float floatAmplitude = 0.5f; // altura
    [SerializeField] private float floatFrequency = 2f;   // velocidad

    private Vector3 startPosition;
    private int direction = 1; // 1 = derecha, -1 = izquierda
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        IdleMovementMask();
    }

    void IdleMovementMask()
    {
             

        // ─── Flotación vertical ───
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = new Vector3(
            transform.position.x,
            startPosition.y + yOffset,
            transform.position.z
        );
    }
}
