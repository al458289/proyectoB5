using UnityEngine;

public class puzle4 : MonoBehaviour
{
    
    void Start()
    {

        string[] bienvenida = {
            "You   see   four   buttons,   each   with   a   different   message.   Only   one   of   them   will   help   the   lynx   recover.",
            "¿Que  deberías  de  hacer?"
        };
        DialogueManager.Instance.ShowText(bienvenida);
    }
     
    

    
    void Update()
    {
        
    }
}
