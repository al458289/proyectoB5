using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class InteractuarEscena : MonoBehaviour
{
    public string nombreDeLaEscenaDestino;

    [Header("Configuración de Luz")]
    public Light2D luzInteraccion;
    public float intensidadAlAcercarse = 1.0f;
    public Vector3 posicionEnNuevaEscena;

    private bool jugadorCerca = false;

    void Start()
    {
        if (luzInteraccion != null)
        {
            luzInteraccion.enabled = false;
        }
    }

    void Update()
    {
        // Detectar interacción (Tecla F)
        if (jugadorCerca && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            // ACTUALIZACIÓN: Solo llamamos al GameManager.
            // Él ya se encarga de buscar al jugador y guardar su posición actual.
            if (GameManager.Instance != null)
            {
                GameManager.Instance.playerPosition = posicionEnNuevaEscena;
                GameManager.Instance.SaveGame();
            }

            // Cambiar de escena
            SceneManager.LoadScene(nombreDeLaEscenaDestino);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            

            if (luzInteraccion != null)
            {
                luzInteraccion.enabled = true;
                luzInteraccion.intensity = intensidadAlAcercarse;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (luzInteraccion != null)
            {
                luzInteraccion.enabled = false;
            }
        }
    }
}