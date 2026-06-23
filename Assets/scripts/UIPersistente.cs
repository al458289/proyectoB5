using UnityEngine;
using UnityEngine.SceneManagement; 

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
        
        if (SceneManager.GetActiveScene().name == "GameOver"|| SceneManager.GetActiveScene().name == "primera escena" || SceneManager.GetActiveScene().name == "FinalBueno")
        {
            Instance = null; // Limpiamos la referencia estática
            Destroy(gameObject);
        }
    }
}