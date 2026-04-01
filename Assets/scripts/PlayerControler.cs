using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 10f;

    private float initialScaleX;
    private Rigidbody2D rb;
    private Animator animator;

    // Cambiamos a Vector2 para guardar los ejes X (horizontal) e Y (vertical)
    private Vector2 movementInput;

    void Start()
    {
        initialScaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Importante: Si es un juego visto desde arriba, la gravedad no debería afectarle.
        // Lo pongo a 0 aquí por precaución, aunque puedes hacerlo en el inspector.
        rb.gravityScale = 0f;
    }

    public void OnMove(InputValue value)
    {
        // Guardamos el vector completo de movimiento
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // 1. MOVIMIENTO: Aplicamos la velocidad tanto en X como en Y
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, movementInput.y * moveSpeed);

        // 2. ANIMACIÓN: Controlamos el bool "caminando"
        if (animator != null)
        {
            // Si el jugador está pulsando alguna tecla de movimiento (magnitud mayor a 0.1)
            if (movementInput.magnitude > 0.1f)
            {
                animator.SetBool("caminando", true); // Empieza a caminar
            }
            else
            {
                animator.SetBool("caminando", false); // Vuelve a estar quieto
            }
        }

        // 3. GIRO (FLIP): Mantenemos el giro solo para el eje X
        if (movementInput.x > 0.1f) // Moviéndose a la derecha
        {
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (movementInput.x < -0.1f) // Moviéndose a la izquierda
        {
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}