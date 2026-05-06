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

    private static bool yaSeLimpioAlEmpezar = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // --- ESTA ES LA LÍNEA QUE HEMOS AÑADIDO ---
            // Permite que el Manager te siga al laberinto/puzles sin borrarse.
            // Cuando quites el modo Play, se destruirá automáticamente como tú quieres.
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            // Si es la primera vez que entramos (al darle al Play)
            if (!yaSeLimpioAlEmpezar)
            {
                PlayerPrefs.DeleteAll();
                ResetDatosLocales();
                yaSeLimpioAlEmpezar = true; // Marcamos que ya se limpió
                Debug.Log("Memoria limpiada por inicio de sesión de juego");
            }
#endif

            LoadGame();
        }
        else
        {
            // Si ya existe un Manager (de la escena anterior), 
            // destruimos el nuevo para quedarnos con el que ya tiene los datos.
            Destroy(gameObject);
        }
    }

    private void ResetDatosLocales()
    {
        tiempoTranscurrido = 0f;
        puzzle1Completado = false;
        puzzle2Completado = false;
        puzzle3Completado = false;
        playerPosition = Vector3.zero; // Reset de posición
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
        // Esta es la única línea nueva: preguntar al jugador dónde está antes de guardar
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerPosition = player.transform.position;

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