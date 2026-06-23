using UnityEngine;

public class puzle5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        string[] bienvenida = {
            "You   find   an   old   board   with   a   question   about   the   Iberian   lynx.   Four   regions   are   shown   below,   but   only   one   of   them   holds   the   correct   answer.",
            "What   should   you   do?"
        };
        DialogueManager.Instance.ShowText(bienvenida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
