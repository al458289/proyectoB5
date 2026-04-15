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
        // Guardamos la escala original (positiva). 
        // Como el dibujo original mira a la izq., esta escala significa "Mirar a la izquierda"
        initialScaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Evitamos que el personaje flote o rote por colisiones
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // 1. MOVIMIENTO
        rb.linearVelocity = movementInput * moveSpeed;

        // 2. ANIMACIÓN
        if (animator != null)
        {
            animator.SetFloat("horizontal", movementInput.x);
            animator.SetFloat("vertical", movementInput.y);

            // Si la magnitud es mayor a 0, está caminando
            bool estaMoviendose = movementInput.sqrMagnitude > 0.01f;
            animator.SetBool("caminando", estaMoviendose);
        }

        // 3. FLIP (Giro de cara)
        // IMPORTANTE: Hemos invertido la lógica porque tu original mira a la izquierda
        if (movementInput.x > 0.1f)
        {
            // Si va a la DERECHA, le ponemos la escala en negativo para voltearlo
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (movementInput.x < -0.1f)
        {
            // Si va a la IZQUIERDA, lo dejamos en positivo (como el original)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}