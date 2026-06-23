using UnityEngine;

public class Puerta2 : MonoBehaviour
{
    private Animator animator;
    private bool abierta = false;
    

    
    public RuntimeAnimatorController controlador;


    void Start()
    {
        Time.timeScale = 1f;

        animator = GetComponent<Animator>();

        
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

        
        if (GameManager.Instance != null)
        {

            if (GameManager.Instance.puzzle5Completado &&
                GameManager.Instance.puzzle4Completado)
            {
                
                AbrirPuerta();
            }
        }
    }

    void AbrirPuerta()
    {
        if (abierta) return; 
        abierta = true;
        
        if (animator != null)
        {
            animator.SetBool("Abrir", true);
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;


        Destroy(gameObject, 8f);
        
    }
}
