using UnityEngine;
using UnityEngine.SceneManagement;

public class agujero : MonoBehaviour
{
    public string nombreDeLaEscenaDestino;

    private bool jugadorCerca = false;

    void Update()
    {
        // Se activa automáticamente al tocar el trigger
        if (jugadorCerca)
        {
            // Marcar puzzle como completado
            GameManager.Instance.puzzle2Completado = true;

            // Guardar posición del jugador
            GameObject player = GameObject.FindGameObjectWithTag("Player");

           

            // Cambiar escena
            SceneManager.LoadScene(nombreDeLaEscenaDestino);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
