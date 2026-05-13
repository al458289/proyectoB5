using UnityEngine;
using UnityEngine.SceneManagement;

public class QUESO2 : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreDeLaEscenaDestino;

    private bool cambiandoEscena = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo actuamos si es el Jugador (el ratón) y si no estamos ya cambiando de escena
        if (collision.CompareTag("Player") && !cambiandoEscena)
        {
            cambiandoEscena = true;
            CompletarPuzzle();
        }
    }

    private void CompletarPuzzle()
    {
        if (GameManager.Instance != null)
        {
            // 1. Marcamos el puzzle como hecho en el Manager
            // Esto activará la curación automática en el script AnimalHealth
            GameManager.Instance.puzzle5Completado = true;

            // 2. Guardamos la partida para que el progreso persista


            Debug.Log("Puzle 1 completado y guardado.");
        }

        // 3. Cambiar a la escena de la casa/habitación
        SceneManager.LoadScene(nombreDeLaEscenaDestino);
    }

}
