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
            "You   notice   a   strange   game   resting   on   the   table.   As   you   look   closer,   you   realize   you   can   control   the   mouse.",
            "But   be   careful.   A   red   laser   is   watching   it,   and   you'll   need   to   distract   it   if   you   want   to   succeed. ",
            "What   should   you   do?"
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