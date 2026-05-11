using UnityEngine;
using System;

public class AnimalHealth : MonoBehaviour
{
    public HealthData data;
    public float poisonDamagePerSecond = 1f;
    public static event Action<float, float> OnAnimalHealthChanged;
    public static event Action OnAnimalHealedVisual;
    public static AnimalHealth Instance;

    // Control para curar solo una vez por puzle
    
    private bool recompensaP1Entregada = false;
    private bool recompensaP2Entregada = false;
    private bool recompensaP3Entregada = false;

    private void Awake()
    {
        if (Instance == null)

        {

            Instance = this;

            DontDestroyOnLoad(gameObject);



            // ESTO reseteará la vida a 100 cada vez que pulses "Play" en el editor

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

    }

    private void Update()
    {
        if (data != null && data.currentHealth > 0)
        {
            // 1. LÓGICA DE CURACIÓN POR PUZLE
            ComprobarRecompensasPuzles();

            // 2. LÓGICA DEL VENENO (Tu código actual)
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

        // Puzle 1
        if (GameManager.Instance.puzzle1Completado && !recompensaP1Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP1Entregada);
        }
        // Puzle 2
        if (GameManager.Instance.puzzle2Completado && !recompensaP2Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP2Entregada);
        }
        // Puzle 3
        if (GameManager.Instance.puzzle3Completado && !recompensaP3Entregada)
        {
            EjecutarCuracionRecompensa(ref recompensaP3Entregada);
        }
    }
    private void EjecutarCuracionRecompensa(ref bool controlBooleano)
    {
        float cantidadCuracion = 20f;
        data.currentHealth = Mathf.Min(data.currentHealth + cantidadCuracion, data.maxHealth);
        controlBooleano = true;

        // Aquí es donde lanzamos el aviso para que la barra se ponga verde
        OnAnimalHealthChanged?.Invoke(data.currentHealth, data.maxHealth);

        // Llamamos a un nuevo evento específico para el efecto visual
        OnAnimalHealedVisual?.Invoke();
    }

    void Die() { Debug.Log("El animal ha muerto."); }
}