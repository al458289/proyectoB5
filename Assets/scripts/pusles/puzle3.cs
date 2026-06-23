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
        "Apartas  un  par  de  libros  de  la  estantería,  ves  una  pregunta  muy  extraña  y  unos  botones  que  suben  y  bajan  la  cantidad.",
        "¿Que  deberías  de  hacer?"
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