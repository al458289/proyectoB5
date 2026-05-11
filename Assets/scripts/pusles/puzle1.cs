using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // --- NUEVAS VARIABLES PARA EL ENEMIGO ---
    [Header("Configuración Persecución")]
    [SerializeField] private BotController enemigo; // Arrastra al enemigo aquí
    private bool haAvisadoAlEnemigo = false;

    private Vector3 escalaOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        escalaOriginal = transform.localScale;

        string[] bienvenida = {
            "Ves un tipo de juego encima de la mesilla, te das cuenta de que puedes controlar al raton.",
            "¿Que deberías de hacer?"
        };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        // LÓGICA DE AVISO:
        // Si el jugador pulsa cualquier dirección y aún no hemos avisado
        if (moveInput.sqrMagnitude > 0 && !haAvisadoAlEnemigo)
        {
            if (enemigo != null)
            {
                enemigo.ActivarPersecucion(); // Despierta al enemigo
                haAvisadoAlEnemigo = true;    // Marcamos como avisado para no repetir
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;

        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (moveInput.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (moveInput.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}