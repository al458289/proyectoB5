using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // <--- ESTO ES NUEVO E IMPRESCINDIBLE

public class ContadorPuzle : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoNumero;
    public int valorActual = 10;
    public int respuestaCorrecta = 15; // Pon aquí el número que sea la solución

    void Start()
    {
        ActualizarInterfaz();
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

    // --- ESTA ES LA FUNCIÓN PARA EL BOTÓN PLAY ---
    public void ConfirmarYPasarEscena()
    {
        if (valorActual == respuestaCorrecta)
        {
            // Marcar puzzle como completado
           GameManager.Instance.puzzle3Completado = true;

            // Guardar partida (SIN tocar la posición)
            GameManager.Instance.SaveGame();

            // Cambiar escena
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.Log("Respuesta incorrecta: " + valorActual);
            // Aquí podrías poner un sonido de error o un mensaje en pantalla
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