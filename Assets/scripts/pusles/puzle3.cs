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
     // Pon aquí las coordenadas de la casa

    void Start()
    {
        ActualizarInterfaz();
        Debug.Log("ha entrado1");
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
                // 1. Marcar progreso
                GameManager.Instance.puzzle3Completado = true; 

                // 2. Definir dónde aparece el jugador al volver
                // Importante: Pon las coordenadas de la habitación donde esté el puzzle
                
            }

            // 4. Cambiar escena
            SceneManager.LoadScene(nombreEscenaPrincipal);
        }
        else
        {
            Debug.Log("Respuesta incorrecta: " + valorActual);
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