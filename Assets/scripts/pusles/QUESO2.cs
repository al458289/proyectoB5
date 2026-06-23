using UnityEngine;
using UnityEngine.SceneManagement;

public class QUESO2 : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreDeLaEscenaDestino;

    private bool cambiandoEscena = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
            
            GameManager.Instance.puzzle5Completado = true;

            
        }

        
        SceneManager.LoadScene(nombreDeLaEscenaDestino);
    }

}
