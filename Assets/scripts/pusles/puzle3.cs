using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ContadorPuzle : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoNumero;
    public int valorActual = 10;
    public int respuestaCorrecta = 15;

    [Header("Retorno a Escena")]
    public string nombreEscenaPrincipal = "SampleScene";
     

    void Start()
    {
        ActualizarInterfaz();
        
        string[] bienvenida = {
        "After   moving   a   few   books   aside,   you   discover   a   strange   question   and   a   set   of   buttons   that   increase   or   decrease   a   number.",
        "What   should   you   do?"
    };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    public void SumarUno()
    {
        valorActual++;
        ActualizarInterfaz();
    }

    public void RestarUno()
    {
        valorActual--;
        ActualizarInterfaz();
    }

    public void ConfirmarYPasarEscena()
    {
        if (valorActual == respuestaCorrecta)
        {
            if (GameManager.Instance != null)
            {
                
                GameManager.Instance.puzzle3Completado = true; 

                
                
            }

            // 4. Cambiar escena
            SceneManager.LoadScene(nombreEscenaPrincipal);
        }
        
    }

    void ActualizarInterfaz()
    {
        if (textoNumero != null)
        {
            textoNumero.text = valorActual.ToString();
        }
    }
}