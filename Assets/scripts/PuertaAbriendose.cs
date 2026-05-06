using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    private Animator animator;
    private bool abierta = false;

    // 1. Añade esta línea para guardar el archivo del Animator
    public RuntimeAnimatorController controlador;

    void Start()
    {
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
            if (GameManager.Instance.puzzle1Completado &&
                GameManager.Instance.puzzle2Completado &&
                GameManager.Instance.puzzle3Completado)
            {
                AbrirPuerta();
            }
        }
    }

    void AbrirPuerta()
    {
        if (abierta) return; // Evita que se ejecute dos veces
        abierta = true;

        if (animator != null)
        {
            animator.SetTrigger("Abrir");
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 2f);
    }
}