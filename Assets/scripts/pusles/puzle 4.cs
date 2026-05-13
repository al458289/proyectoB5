using UnityEngine;

public class puzle4 : MonoBehaviour
{
    
    void Start()
    {

        string[] bienvenida = {
            "Ves  4  botones  con  un  texto  encima,  puedes  elegir  cualquiera  de  las  opciones  solo  le  curará  una  de  ellas.",
            "¿Que  deberías  de  hacer?"
        };
        DialogueManager.Instance.ShowText(bienvenida);
    }
     
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
