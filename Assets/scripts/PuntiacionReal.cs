using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DisplayFinalScore : MonoBehaviour
{
    void Start()
    {

        float p = GameManager.Instance.vidaFinalPartida ;
        ;

        int puntuacion = Mathf.RoundToInt(p * 100);

        GetComponent<TextMeshProUGUI>().text = puntuacion.ToString();
    }
}