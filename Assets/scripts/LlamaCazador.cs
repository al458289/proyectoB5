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
            "The   lynx   has   escaped   and   is   finally   safe. But   before   you   can   relax,   you   hear   heavy   footsteps   approaching.   The   hunter   is   coming...   and   he's   getting   closer.",
            "You   must   escape   the   house   before   the   hunter   catches   you.   If   you   fail,   everything   you   have   done   to   save   the   lynx   will   have   been   for   nothing."
        };
        DialogueManager.Instance.ShowText(bienvenida);

        GameManager.Instance.textoEnseñado = true;
    }
}