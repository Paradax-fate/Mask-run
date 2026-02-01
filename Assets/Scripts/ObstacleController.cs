using UnityEngine;

public class ObstacleController : MonoBehaviour
{   

    [Header("Obstacle Settings")]
    [SerializeField] private Collider killerCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colision con el jugador");
            
        }
    }

}
