using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Puzle2 : MonoBehaviour
{
    public enum Orientacion { Horizontal, Vertical }
    public Orientacion tipo;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Camera cam;
    private bool estaArrastrando = false;
    private Vector2 offset;
    private float posicionEjeFijo;

    // Filtro para que el rayo solo choque con lo que queremos
    private ContactFilter2D filtroContacto;
    private RaycastHit2D[] resultados = new RaycastHit2D[1];

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        cam = Camera.main;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.useFullKinematicContacts = true;

        // Configuramos el filtro para que detecte cualquier Collider2D
        filtroContacto = new ContactFilter2D();
        filtroContacto.useTriggers = false;
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;
        Vector2 mousePos = cam.ScreenToWorldPoint(mouse.position.ReadValue());

        if (mouse.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.transform == transform)
            {
                estaArrastrando = true;
                offset = (Vector2)transform.position - mousePos;
                posicionEjeFijo = (tipo == Orientacion.Horizontal) ? transform.position.y : transform.position.x;
            }
        }

        if (mouse.leftButton.wasReleasedThisFrame) estaArrastrando = false;
    }

    void FixedUpdate()
    {
        if (!estaArrastrando) return;

        Vector2 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 destino = mousePos + offset;

        if (tipo == Orientacion.Horizontal) destino.y = posicionEjeFijo;
        else destino.x = posicionEjeFijo;

        Vector2 direccion = destino - rb.position;
        float distancia = direccion.magnitude;

        if (distancia > 0.01f)
        {
            // Tiramos la caja pero ignorando nuestra propia pieza
            int choques = col.Cast(direccion.normalized, filtroContacto, resultados, distancia);

            if (choques == 0)
            {
                rb.MovePosition(destino);
            }
            else
            {
                // Si choca, nos movemos solo la distancia permitida
                float distanciaSegura = resultados[0].distance;
                rb.MovePosition(rb.position + direccion.normalized * (distanciaSegura - 0.02f));
            }
        }
    }
}