using UnityEngine;

public class PuertaFinal : MonoBehaviour
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
        if (abierta) return; 
        abierta = true;
        
        if (animator != null)
        {
            animator.SetBool("Abrir",true);
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        
        Destroy(gameObject, 8f);
    }
}