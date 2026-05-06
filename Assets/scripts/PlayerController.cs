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
        // --- FIX CRÍTICO ---
        // Forzamos que el tiempo corra. Si un puzzle puso Time.timeScale = 0,
        // esto lo arregla en cuanto aparece el jugador.
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScaleX = transform.localScale.x;

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Forzamos la carga de posición
        CargarPosicionDesdeManager();
    }

    public void CargarPosicionDesdeManager()
    {
        if (GameManager.Instance != null)
        {
            // Desactivamos momentáneamente para evitar conflictos con colisiones al "teletransportar"
            rb.simulated = false;
            transform.position = GameManager.Instance.playerPosition;
            rb.simulated = true;

            Debug.Log("Jugador posicionado en: " + transform.position);
        }
    }

    // Este es el método que llama el "Player Input" (Behavior: Send Messages)
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Si el tiempo está en 0 (pausa), FixedUpdate NO se ejecuta.
        // Al poner Time.timeScale = 1 en el Start, esto volverá a funcionar.
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