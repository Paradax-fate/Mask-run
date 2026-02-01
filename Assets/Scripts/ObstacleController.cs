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

    private void OnTriggerEnter(Collider other)
    {
        if (killerCollider != null) {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.hitPlayer.Invoke();
                Debug.Log("Trigger con el jugador");
                other.GetComponent<PlayerController>().loseGame();
            }
        }
    }
    

}
