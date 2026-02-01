using UnityEngine;
using System.Collections;

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

    private Coroutine rotateRoutine;
    private int currentIndex = 0;
    private const int maskCount = 4;

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


    public void RotateToNextMask(float rotationDuration)
    {
        int nextIndex = (currentIndex + 1) % maskCount;
        RotateToIndex(nextIndex, rotationDuration);
    }

    public void RotateToIndex(int index, float rotationDuration)
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        float anglePerMask = 360f / maskCount;
        float targetAngle = index * anglePerMask;

        rotateRoutine = StartCoroutine(
            RotatePivotSmooth(Quaternion.Euler(0f, targetAngle, 0f), rotationDuration)
        );

        currentIndex = index;
    }

    IEnumerator RotatePivotSmooth(Quaternion targetRotation, float rotationDuration)
    {
        Quaternion startRotation = maskRotator.localRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotationDuration;
            maskRotator.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        maskRotator.localRotation = targetRotation;
    }


}
