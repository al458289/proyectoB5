using UnityEngine;

public class Puerta2 : MonoBehaviour
{
    private Animator animator;
    private bool abierta = false;
    

    // 1. Añade esta línea para guardar el archivo del Animator
    public RuntimeAnimatorController controlador;


    void Start()
    {
        Time.timeScale = 1f;

        animator = GetComponent<Animator>();

        // 2. Si al volver de la escena el controlador se ha borrado, lo reponemos
        if (animator != null && animator.runtimeAnimatorController == null)
        {
            animator.runtimeAnimatorController = controlador;
            Debug.Log("Controlador de animaciones restaurado manualmente.");
        }

        ComprobarPuzzles();
    }

    void Update()
    {
        if (!abierta)
        {

            ComprobarPuzzles();
        }
    }

    void ComprobarPuzzles()
    {

        // Añadimos una protección extra por si el GameManager tarda en cargar
        if (GameManager.Instance != null)
        {

            if (GameManager.Instance.puzzle5Completado &&
                GameManager.Instance.puzzle4Completado)
            {
                Debug.Log("segundo if");
                AbrirPuerta();
            }
        }
    }

    void AbrirPuerta()
    {
        if (abierta) return; // Evita que se ejecute dos veces
        abierta = true;
        Debug.Log("entra abrir puerta");
        if (animator != null)
        {
            animator.SetBool("Abrir", true);
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;


        Destroy(gameObject, 8f);
        HunterController.Instance.ActivarPersecucion();
    }
}
