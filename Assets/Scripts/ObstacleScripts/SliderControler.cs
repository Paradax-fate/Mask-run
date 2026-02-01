using UnityEngine;

public class SliderControler : MonoBehaviour
{

    [Header("Slider Settings")]
    [SerializeField] private float sliderSpeed = 15f;
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] public Transform killerCollider;


    private Vector3 target;
    private bool movingToB = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        RotateSaw();
        MoveSlider();
    }

    void RotateSaw()
    {
        killerCollider.Rotate(Vector3.forward, 360f * Time.deltaTime);
    }
    void MoveSlider()
    {


        float targetX = movingToB ? pointB.localPosition.x : pointA.localPosition.x;

        Vector3 pos = killerCollider.localPosition;
        pos.x = Mathf.MoveTowards(
            pos.x,
            targetX,
            sliderSpeed * Time.deltaTime
        );

        killerCollider.localPosition = pos;

        if (Mathf.Abs(pos.x - targetX) < 0.05f)
        {
            movingToB = !movingToB;
        }
    }

}
