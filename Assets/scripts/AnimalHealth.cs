using UnityEngine;
using System;

public class AnimalHealth : MonoBehaviour
{
    public HealthData data;
    public float poisonDamagePerSecond = 1f;

    public static event Action<float, float> OnAnimalHealthChanged;

    // AÑADIMOS ESTO: Singleton para persistencia
    public static AnimalHealth Instance;

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

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.animalHealthData != null)
        {
            data = GameManager.Instance.animalHealthData;
            OnAnimalHealthChanged?.Invoke(data.currentHealth, data.maxHealth);
        }
    }

    private void Update()
    {
        if (data != null && data.currentHealth > 0)
        {
            data.currentHealth -= poisonDamagePerSecond * Time.deltaTime;
            data.currentHealth = Mathf.Max(data.currentHealth, 0);
            OnAnimalHealthChanged?.Invoke(data.currentHealth, data.maxHealth);
        }
        else if (data != null && data.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() { Debug.Log("El animal ha muerto."); }
}