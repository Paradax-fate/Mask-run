using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Principal Game Settings")]
    [SerializeField] private GameManager gameManager;

    [Header("Movement")]
    [SerializeField] private float lateralSpeed = 6f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private float moveInput;
    private bool isGrounded;
    private bool isCrouching;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGround();
        MoveLateral();
    }

    void MoveLateral()
    {
        rb.linearVelocity = new Vector3(
            moveInput * lateralSpeed,
            rb.linearVelocity.y,
            0f
        );
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }

    // ───────── INPUT EVENTS ─────────

    public void OnMove(InputAction.CallbackContext context)
    {        
        moveInput = context.ReadValue<float>();
        if (GameManager.Instance.currentMask == GameManager.ActiveMask.InvertInputsMask)
        {
            moveInput *= -1f;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || !isGrounded) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
            Crouch();
        else if (context.canceled)
            StandUp();
    }

    void Crouch()
    {
        if (isCrouching) return;
        isCrouching = true;
        transform.localScale = new Vector3(1f, 0.5f, 1f);
    }

    void StandUp()
    {
        isCrouching = false;
        transform.localScale = Vector3.one;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over.");
            gameManager.gameOver.Invoke(true);
        }
    }

    public void loseGame()
    {
        GameManager.Instance.gameOver.Invoke(true);
    }
}
