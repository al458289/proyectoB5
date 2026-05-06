using UnityEngine;
using UnityEngine.SceneManagement;

public class QUESO : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreDeLaEscenaDestino;

    [Header("Punto de Aparición al Volver")]
    [Tooltip("Las coordenadas X, Y, Z donde quieres que el jugador aparezca en la siguiente escena")]
    public Vector3 posicionEnNuevaEscena;

    private bool cambiandoEscena = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo actuamos si es el Jugador y si no estamos ya cambiando de escena
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
            // 1. Marcar el puzzle como hecho
            GameManager.Instance.puzzle1Completado = true;

            // 2. IMPORTANTE: En lugar de guardar la posición actual del laberinto,
            // le decimos al Manager que la posición guardada sea la de la nueva escena.
            GameManager.Instance.playerPosition = posicionEnNuevaEscena;

            // 3. Guardar los datos en PlayerPrefs
            // Pasamos 'false' si modificaste el SaveGame como te sugerí, 
            // o simplemente llamamos a SaveGame() si prefieres el método estándar.
            GameManager.Instance.SaveGame();
        }

        // 4. Cambiar a la escena de la casa/habitación
        SceneManager.LoadScene(nombreDeLaEscenaDestino);
    }

}