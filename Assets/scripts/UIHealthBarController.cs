using System.Collections; // Necesario para Corrutinas
using UnityEngine;
using UnityEngine.UIElements;

public class UIHealthBarController : MonoBehaviour
{
    private ProgressBar healthBar;
    private VisualElement progressFill; 

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("HealthBar");

        
        progressFill = healthBar.Q<VisualElement>(className: "unity-progress-bar__progress");

        AnimalHealth.OnAnimalHealthChanged += UpdateBar;
        
        AnimalHealth.OnAnimalHealedVisual += IniciarEfectoVerde;
    }

    private void OnDisable()
    {
        AnimalHealth.OnAnimalHealthChanged -= UpdateBar;
        AnimalHealth.OnAnimalHealedVisual -= IniciarEfectoVerde;
    }

    private void IniciarEfectoVerde()
    {
        StartCoroutine(EfectoColorCuracion());
    }

    private IEnumerator EfectoColorCuracion()
    {
        if (progressFill == null) yield break;

        
        progressFill.style.backgroundColor = new StyleColor(Color.green);

        
        yield return new WaitForSeconds(2f);

        
        progressFill.style.backgroundColor = new StyleColor(Color.white); 
    }

    private void UpdateBar(float current, float max)
    {
        if (healthBar == null) return;
        healthBar.highValue = max;
        healthBar.value = current;
        healthBar.title = $"{Mathf.CeilToInt(current)} / {max}";
    }
}