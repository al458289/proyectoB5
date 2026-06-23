using UnityEngine;
using UnityEngine.SceneManagement;

public class agujero : MonoBehaviour
{
    public string nombreDeLaEscenaDestino;

    [Header("Punto de Aparición en la Nueva Escena")]
    

    private bool jugadorCerca = false;
    private bool yaSeActivo = false; 

    void Start()
    {
        string[] bienvenida2 = {
        "Te  das  cuenta  que  hay  algo  que  parece  una  llave  y  dependiento  de  como  muevas  los  libros  puedes  sacarla  por  un  pequeño  agujero",
        "¿Que  deberías  de  hacer?"
    };
        DialogueManager.Instance.ShowText(bienvenida2);
    }
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
            
            GameManager.Instance.puzzle2Completado = true;

            
        }

        
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