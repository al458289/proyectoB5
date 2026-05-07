using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Quitamos el SpriteRenderer y usamos la escala original
    private Vector3 escalaOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        // Guardamos CÓMO es tu personaje al principio
        escalaOriginal = transform.localScale;
        Debug.Log("ha entrado1");
        string[] bienvenida = {
        "Ves  un  tipo  de  juego  encima  de  la  mesilla,  te  das  cuenta  de  que  puedes  controlar  al  raton.",
        "¿Que  deberías  de  hacer?"
    };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;

        if (moveInput.x > 0) // Si se mueve a la derecha
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (moveInput.x < 0) // Si se mueve a la izquierda
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (moveInput.y > 0)
        {
            // Ponerlo en vertical
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (moveInput.y < 0)
        {
            // Ponerlo en vertical
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}