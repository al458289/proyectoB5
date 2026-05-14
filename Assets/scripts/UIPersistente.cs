using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para detectar la escena

public class UIPersistente : MonoBehaviour
{
    public static UIPersistente Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Si entramos en la escena de Game Over, destruimos toda la UI persistente
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Instance = null; // Limpiamos la referencia estática
            Destroy(gameObject);
        }
    }
}