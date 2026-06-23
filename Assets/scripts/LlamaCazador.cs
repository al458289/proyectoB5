using UnityEngine;
using UnityEngine.SceneManagement;

public class LlamaCazador : MonoBehaviour
{
    public string tagDelJugador = "Player";

    // 1. Creamos la variable para controlar que solo entre una vez
    private bool yaEntro = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 2. Añadimos "&& !yaEntro" a la condición (así solo pasa si NO ha entrado antes)
        if (collision.gameObject.CompareTag(tagDelJugador) &&
            GameManager.Instance.puzzle5Completado &&
            GameManager.Instance.puzzle4Completado &&
            !yaEntro)
        {
            // 3. Inmediatamente lo volvemos true para bloquear el "if"
            yaEntro = true;
            LlegandoAlFinal();
        }
    }

    private void LlegandoAlFinal()
    {
        string[] bienvenida = {
            "El  lince  ya  se  ha  marchado  y  esta  a  salvo  fuera  de  la  casa  pero  de  repente  escuchas  que  el  cazador  esta  viniendo",
            "Si  no  consigues  salir  de  aqui  el  cazador  te  atrapara  a  ti  y  al  lince"
        };
        DialogueManager.Instance.ShowText(bienvenida);

        GameManager.Instance.textoEnseñado = true;
    }
}