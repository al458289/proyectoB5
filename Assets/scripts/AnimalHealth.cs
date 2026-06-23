using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalHealth : MonoBehaviour
{
    public HealthData data;
    public float poisonDamagePerSecond = 1f;
    public static event Action<float, float> OnAnimalHealthChanged;
    public static event Action OnAnimalHealedVisual;
    public static AnimalHealth Instance;
    public TextMeshProUGUI TextoPuzles;
    public int puzles;
    

    // Control para curar solo una vez por puzle

    public bool recompensaP1Entregada = false;
    public bool recompensaP2Entregada = false;
    public bool recompensaP3Entregada = false;
    public bool recompensaP4Entregada = false;
    public bool recompensaP5Entregada = false;

    private void Awake()
    {   

        if (Instance == null)

        {

            Instance = this;

            DontDestroyOnLoad(gameObject);



            

#if UNITY_EDITOR

            if (data != null)

            {

                data.currentHealth = data.maxHealth;

            }

#endif

        }

        else

        {

            Destroy(gameObject);

        }
        

        TextoPuzles.text = string.Format("P:" + puzles.ToString());


    }
    public void buscarTexto()
    {
        GameObject obj = GameObject.Find("puzlesCompletados");
        if (obj != null)
        {
            TextoPuzles = obj.GetComponent<TextMeshProUGUI>();

        }
    }

    public void resetDatos()
    {
        recompensaP1Entregada = false;
        recompensaP2Entregada = false;
        recompensaP3Entregada = false;
        recompensaP4Entregada = false;
        recompensaP5Entregada = false;
    }

    private void Update()
    {
        if (data != null && data.currentHealth > 0)
        {
            
            ComprobarRecompensasPuzles();

            
            data.currentHealth -= poisonDamagePerSecond * Time.deltaTime;
            data.currentHealth = Mathf.Max(data.currentHealth, 0);
            OnAnimalHealthChanged?.Invoke(data.currentHealth, data.maxHealth);
        }
        else if (data != null && data.currentHealth <= 0)
        {
            Die();
        }
    }
    

    private void ComprobarRecompensasPuzles()
    {
        if (GameManager.Instance == null) return;

        
        if (GameManager.Instance.puzzle1Completado && !recompensaP1Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP1Entregada);
            puzles += 1;

            TextoPuzles.text = string.Format("P:" + puzles.ToString());
        }
        
        if (GameManager.Instance.puzzle2Completado && !recompensaP2Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP2Entregada);
            puzles += 1;

            TextoPuzles.text = string.Format("P:" + puzles.ToString());
        }
        
        if (GameManager.Instance.puzzle3Completado && !recompensaP3Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP3Entregada);
            puzles += 1;

            TextoPuzles.text = string.Format("P:" + puzles.ToString());
        }
        if (GameManager.Instance.puzzle4Completado && !recompensaP4Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP4Entregada);
            puzles += 1;

            TextoPuzles.text = string.Format("P:" + puzles.ToString());
        }
        if (GameManager.Instance.puzzle5Completado && !recompensaP5Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP5Entregada);
            puzles += 1;

            TextoPuzles.text = string.Format("P:" + puzles.ToString());
        }
    }
    private void EjecutarCuracionRecompensa(ref bool controlBooleano)
    {
        float cantidadCuracion = 30f;
        data.currentHealth = Mathf.Min(data.currentHealth + cantidadCuracion, data.maxHealth);
        controlBooleano = true;

        
        OnAnimalHealthChanged?.Invoke(data.currentHealth, data.maxHealth);

        
        OnAnimalHealedVisual?.Invoke();
    }

    void Die() {
        GameManager.Instance?.PrepararGameOver();
        
        SceneManager.LoadScene("GameOver");
    }
}