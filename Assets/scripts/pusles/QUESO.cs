using UnityEngine;
using UnityEngine.SceneManagement;

public class QUESO : MonoBehaviour
{
    public string nombreDeLaEscenaDestino;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca)
        {
            // Marcar puzzle como completado
            GameManager.Instance.puzzle1Completado = true;

            // Guardar partida (SIN tocar la posición)
            GameManager.Instance.SaveGame();

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