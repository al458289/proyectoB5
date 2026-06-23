
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    
    public string FinalBueno;

   
    public string tagDelJugador = "Player";

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(tagDelJugador))
        {
            CambiarDeEscena();
        }
    }

    
    private void CambiarDeEscena()
    {
        
            GameManager.Instance?.PrepararGameOver();
            SceneManager.LoadScene(FinalBueno);
        }
        
    }
