using UnityEngine;
using UnityEngine.UIElements;

public class UIHealthBarController : MonoBehaviour
{
    private ProgressBar healthBar;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("HealthBar"); // El nombre que pusiste en UI Builder

        // Nos suscribimos al evento del animal
        AnimalHealth.OnAnimalHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        AnimalHealth.OnAnimalHealthChanged -= UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        if (healthBar == null) return;

        healthBar.highValue = max;
        healthBar.value = current;
        // Mostramos solo números enteros para que no se vean decimales infinitos
        healthBar.title = $"{Mathf.CeilToInt(current)} / {max}";
    }
}