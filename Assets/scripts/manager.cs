using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI - Referencias")]
    public TextMeshProUGUI textoTiempo;

    [Header("Datos de Salud")]
    public HealthData animalHealthData;

    [Header("Datos del Jugador")]
    public Vector3 playerPosition;

    [Header("Progreso y Tiempo")]
    public float tiempoTranscurrido;
    public bool puzzle1Completado;
    public bool puzzle2Completado;
    public bool puzzle3Completado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ESTO RESETEA TODO AL DARLE AL PLAY EN EL EDITOR
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
            ResetDatosLocales(); // Ponemos las variables a 0 manualmente
#endif

            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para asegurar que las variables se limpian en memoria
    private void ResetDatosLocales()
    {
        tiempoTranscurrido = 0f;
        puzzle1Completado = false;
        puzzle2Completado = false;
        puzzle3Completado = false;
        if (animalHealthData != null)
        {
            animalHealthData.currentHealth = animalHealthData.maxHealth;
        }
    }

    void Update()
    {
        ActualizarCronometro();
    }

    private void ActualizarCronometro()
    {
        tiempoTranscurrido += Time.deltaTime;

        if (textoTiempo != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
            textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    public void SaveGame()
    {
        if (animalHealthData != null)
            PlayerPrefs.SetFloat("AnimalHealth", animalHealthData.currentHealth);

        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        PlayerPrefs.SetInt("Puzzle1", puzzle1Completado ? 1 : 0);
        PlayerPrefs.SetInt("Puzzle2", puzzle2Completado ? 1 : 0);
        PlayerPrefs.SetInt("Puzzle3", puzzle3Completado ? 1 : 0);

        PlayerPrefs.SetFloat("TiempoJuego", tiempoTranscurrido);

        PlayerPrefs.Save();
        Debug.Log("Partida Guardada");
    }

    public void LoadGame()
    {
        // El segundo valor es el "por defecto" si no hay nada guardado
        if (animalHealthData != null)
            animalHealthData.currentHealth = PlayerPrefs.GetFloat("AnimalHealth", 100f);

        playerPosition = new Vector3(
            PlayerPrefs.GetFloat("PlayerX", 0),
            PlayerPrefs.GetFloat("PlayerY", 0),
            PlayerPrefs.GetFloat("PlayerZ", 0)
        );

        puzzle1Completado = PlayerPrefs.GetInt("Puzzle1", 0) == 1;
        puzzle2Completado = PlayerPrefs.GetInt("Puzzle2", 0) == 1;
        puzzle3Completado = PlayerPrefs.GetInt("Puzzle3", 0) == 1;

        tiempoTranscurrido = PlayerPrefs.GetFloat("TiempoJuego", 0f);

        Debug.Log("Partida Cargada");
    }
}