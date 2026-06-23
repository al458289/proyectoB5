using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    
    [Header("Configuración Persecución")]
    [SerializeField] private BotController enemigo; 
    private bool haAvisadoAlEnemigo = false;

    private Vector3 escalaOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        escalaOriginal = transform.localScale;

        string[] bienvenida = {
            "Ves  un  tipo  de  juego  encima  de  la  mesilla,  te  das  cuenta  de  que  puedes  controlar  al  raton.",
            "PERO  ten  cuidado  ya  que  al  observarlo  te  das  cuenta  que  hay  un  laser  rojo,  tienes  que  intentar  despistarlo. ",
            "¿Que  deberías  de  hacer?"
        };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        
        if (moveInput.sqrMagnitude > 0 && !haAvisadoAlEnemigo)
        {
            if (enemigo != null)
            {
                enemigo.ActivarPersecucion(); 
                haAvisadoAlEnemigo = true;    
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