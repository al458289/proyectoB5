using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDePregunta : MonoBehaviour
{
    [Header("Configuración")]
    public bool esLaOpcionCorrecta;
    public string nombreEscenaSiguiente;

    
    public void SeleccionarOpcion()
    {
        if (esLaOpcionCorrecta)
        {
            

            if (GameManager.Instance != null)
            {
                
                GameManager.Instance.puzzle4Completado = true;
                GameManager.Instance.SaveGame();
            }
        }
        

        
        SceneManager.LoadScene(nombreEscenaSiguiente);
    }
}