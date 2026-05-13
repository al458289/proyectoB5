using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDePregunta : MonoBehaviour
{
    [Header("Configuración")]
    public bool esLaOpcionCorrecta;
    public string nombreEscenaSiguiente; // La escena a la que irán AMBOS (acierto o fallo)

    
    public void SeleccionarOpcion()
    {
        if (esLaOpcionCorrecta)
        {
            Debug.Log("¡Correcto! Marcando Puzle 4 como completado.");

            if (GameManager.Instance != null)
            {
                // Marcamos el puzle y guardamos la partida
                GameManager.Instance.puzzle4Completado = true;
                GameManager.Instance.SaveGame();
            }
        }
        else
        {
            Debug.Log("Fallo. Yendo a la escena sin completar el puzle.");
        }

        // Independientemente de si es correcto o no, cargamos la escena
        SceneManager.LoadScene(nombreEscenaSiguiente);
    }
}