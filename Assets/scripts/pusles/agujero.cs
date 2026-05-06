using UnityEngine;
using UnityEngine.SceneManagement;

public class agujero : MonoBehaviour
{
    public string nombreDeLaEscenaDestino;

    [Header("Punto de Aparición en la Nueva Escena")]
    

    private bool jugadorCerca = false;
    private bool yaSeActivo = false; // Para evitar que intente cargar la escena muchas veces

    void Update()
    {
        if (jugadorCerca && !yaSeActivo)
        {
            yaSeActivo = true;
            CompletarAgujero();
        }
    }

    private void CompletarAgujero()
    {
        if (GameManager.Instance != null)
        {
            // 1. Marcar puzzle como completado
            GameManager.Instance.puzzle2Completado = true;

            

            // 2. Definimos dónde aparecerá el jugador en la casa
            

            // 3. Guardar partida para que el progreso persista
            
        }

        // 4. Cambiar escena
        SceneManager.LoadScene(nombreDeLaEscenaDestino);
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