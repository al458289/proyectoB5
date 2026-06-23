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
        "You   notice   something   that   looks   like   a   key.   By   moving   the   books   around,   you   might   be   able   to   guide   it   through   a   small   hole. (CLICK  AND  MOVE  THE  MOUSE)",
        "What   should   you   do?"
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