using UnityEngine;
using TMPro;

public class DisplayFinalTime : MonoBehaviour
{
    void Start()
    {
        
        float t = GameManager.Instance.tiempoFinalPartida;

        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = Mathf.FloorToInt(t % 60);

        GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}