using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(CargarPosicion), 0.1f);
    }

    void CargarPosicion()
    {
        transform.position = GameManager.Instance.playerPosition;
    }

    public void GuardarPosicion()
    {
        GameManager.Instance.playerPosition = transform.position;
    }
}