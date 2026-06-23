using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool puzzle4Completado;
    public bool puzzle5Completado;
    public bool textoEnseñado;

    private static bool yaSeLimpioAlEmpezar = false;

    [Header("Resultados Última Partida")]
    public float tiempoFinalPartida;
    public float vidaFinalPartida;

    void Start()
    {
        
        string[] bienvenida = {
        "You   slowly   open   your   eyes.   Everything   around   you   feels   strange   and   unfamiliar.   Next   to   you   lies   a   lynx,   tired   and   weak. (PRESS  ENTER)",
        "To   help   the   lynx   and   escape   this   place,   you   must   solve   the   puzzles   ahead.   Each   one   will   help   it   regain   its   strength.  (F  TO  INTERACT  WITH  OBJECTS) ",
    };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += AlCargarEscena;
        if (Instance == null)
        {
            Instance = this;


            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            
            if (!yaSeLimpioAlEmpezar)
            {
                PlayerPrefs.DeleteAll();
                ResetDatosLocales();
                yaSeLimpioAlEmpezar = true; 
                
            }
#endif

            LoadGame();
        }
        else
        {

            Destroy(gameObject);
        }
    }
    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {

        if (escena.name != "GameOver")
        {

            GameObject obj = GameObject.Find("tiempoTexto");
            if (obj != null)
            {
                textoTiempo = obj.GetComponent<TextMeshProUGUI>();
                
            }
            AnimalHealth.Instance.buscarTexto();
        }
    }

    public void ResetDatosLocales()
    {
        tiempoTranscurrido = 0f;
        puzzle1Completado = false;
        puzzle2Completado = false;
        puzzle3Completado = false;
        puzzle4Completado = false;
        puzzle5Completado = false;
        textoEnseñado = false;
        AnimalHealth.Instance.puzles = 0;
        playerPosition = Vector3.zero;
        if (animalHealthData != null)
        {
            animalHealthData.currentHealth = animalHealthData.maxHealth;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            ActualizarCronometro();
        }
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
        PlayerPrefs.SetInt("Puzzle4", puzzle4Completado ? 1 : 0);
        PlayerPrefs.SetInt("Puzzle5", puzzle5Completado ? 1 : 0);
        PlayerPrefs.SetFloat("TiempoJuego", tiempoTranscurrido);

        PlayerPrefs.Save();
        
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
        puzzle4Completado = PlayerPrefs.GetInt("Puzzle4", 0) == 1;
        puzzle5Completado = PlayerPrefs.GetInt("Puzzle5", 0) == 1;
        tiempoTranscurrido = PlayerPrefs.GetFloat("TiempoJuego", 0f);

        
    }


    public void PrepararGameOver()
    {
        
        tiempoFinalPartida = tiempoTranscurrido;

        if (animalHealthData != null)
        {
            vidaFinalPartida = animalHealthData.currentHealth;
        }

        
        ResetDatosLocales();

        tiempoTranscurrido = 0f;
        AnimalHealth.Instance.resetDatos();

       
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        
    }
}