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
        if (jugadorCerca && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // Guardar posición antes de cambiar de escena
                PlayerSave playerSave = player.GetComponent<PlayerSave>();
                if (playerSave != null)
                {
                    playerSave.GuardarPosicion();
                }
            }

            // Guardar datos del juego
            GameManager.Instance.SaveGame();

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