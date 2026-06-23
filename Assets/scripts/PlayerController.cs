using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 10f;

    private float initialScaleX;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movementInput;

    void Start()
    {
        
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScaleX = transform.localScale.x;

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        
        CargarPosicionDesdeManager();
    }

    public void CargarPosicionDesdeManager()
    {
        if (GameManager.Instance != null)
        {
            
            rb.simulated = false;
            transform.position = GameManager.Instance.playerPosition;
            rb.simulated = true;

            
        }
    }

    
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        
        rb.linearVelocity = movementInput * moveSpeed;

        ActualizarAnimaciones();
        ManejarFlip();
    }

    void ActualizarAnimaciones()
    {
        if (animator != null)
        {
            animator.SetFloat("horizontal", movementInput.x);
            animator.SetFloat("vertical", movementInput.y);
            bool estaMoviendose = movementInput.sqrMagnitude > 0.01f;
            animator.SetBool("caminando", estaMoviendose);
        }
    }

    void ManejarFlip()
    {
        if (movementInput.x > 0.1f)
        {
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (movementInput.x < -0.1f)
        {
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}