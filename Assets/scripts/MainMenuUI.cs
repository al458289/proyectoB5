using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "02_Game";
    [SerializeField] private string creditsSceneName = "03_Credits";
    

    private void Awake()
    {
        Time.timeScale = 1f;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(gameSceneName);

    }

    public void OpenCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        
    }
}