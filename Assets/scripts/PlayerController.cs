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
            // 1. Desactivamos físicas un momento para que no "luchen" contra el teletransporte
            rb.simulated = false;

            // 2. Aplicamos la posición guardada
            transform.position = GameManager.Instance.playerPosition;

            // 3. Reactivamos físicas
            rb.simulated = true;

            Debug.Log("Jugador posicionado en: " + transform.position);
        }
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Movimiento físico profesional
        rb.linearVelocity = movementInput * moveSpeed;

        // Animación
        if (animator != null)
        {
            animator.SetFloat("horizontal", movementInput.x);
            animator.SetFloat("vertical", movementInput.y);
            bool estaMoviendose = movementInput.sqrMagnitude > 0.01f;
            animator.SetBool("caminando", estaMoviendose);
        }

        // Flip del Sprite
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