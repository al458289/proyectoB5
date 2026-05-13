using UnityEngine;
using UnityEngine.SceneManagement;

public class BotController : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] private float speed = 4f; // Un pelín más lento para dar margen
    [SerializeField] private float rotationSmoothTime = 0.05f; // Giro casi instantáneo
    [SerializeField] private Transform target; // El ratón
    [SerializeField] private Rigidbody2D rb;

    [Header("Radar (Evitar Obstáculos)")]
    [SerializeField] private float detectionDistance = 1.2f; // Longitud de los rayos
    [SerializeField] private float circleCastRadius = 0.3f; // Ancho del "radar" central
    [SerializeField] private LayerMask obstacleLayer;

    // --- VARIABLES CLAVE PARA ARREGLAR EL BUG ---
    [SerializeField] private float timeForcedToAvoid = 0.8f; // CUÁNTO tiempo se desvía (Aumenta esto si tiembla)
    private float avoidanceForcedTimer = 0f;
    private Vector2 chosenAvoidanceDir;
    // -------------------------------------------

    private bool hasStarted = false;
    private Vector2 currentVelocitySmoothDamp;

    public void ActivarPersecucion()
    {
        hasStarted = true;
        Debug.Log("Pesadilla activada");
    }

    void FixedUpdate()
    {
        if (!hasStarted || target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dirToTarget = (target.position - transform.position).normalized;

        // --- SOLUCIÓN: LÓGICA DE EVASIÓN PERSISTENTE ---

        // 1. ¿Estamos ya esquivando obligatoriamente?
        if (avoidanceForcedTimer > 0)
        {
            avoidanceForcedTimer -= Time.fixedDeltaTime;
            // Nos movemos en la dirección que elegimos antes, ignorando el target
            MoveBot(chosenAvoidanceDir);
            ControlRotation();
            return;
        }

        // 2. Si no estamos en "modo evasión", comprobamos si hay obstáculos
        // Usamos CircleCast para que sea más ancho que un simple rayo
        RaycastHit2D hitCenter = Physics2D.CircleCast(
            transform.position,
            circleCastRadius,
            dirToTarget,
            detectionDistance,
            obstacleLayer
        );

        // 3. Si detectamos pared, ACTIVAMOS la evasión obligatoria
        if (hitCenter.collider != null)
        {
            // Calculamos 3 direcciones alternativas para encontrar la mejor
            Vector2 left60Dir = Quaternion.Euler(0, 0, 60) * dirToTarget;
            Vector2 right60Dir = Quaternion.Euler(0, 0, -60) * dirToTarget;

            // Preferimos la dirección perpendicular al obstáculo si está bloqueado todo
            Vector2 perpendicular = Vector2.Perpendicular(hitCenter.normal);
            float dot = Vector2.Dot(perpendicular, dirToTarget);
            Vector2 wallPerpendicularDir = perpendicular * (dot > 0 ? 1 : -1);

            // Prioridad: 
            // A) Girar 60º a la izquierda
            // B) Girar 60º a la derecha
            // C) Seguir la pared
            chosenAvoidanceDir = ChooseBestDirection(left60Dir, right60Dir, wallPerpendicularDir);

            // Activamos el temporizador forzoso. Durante X segundos, el bot
            // NO mirará al ratón, solo correrá en chosenAvoidanceDir.
            avoidanceForcedTimer = timeForcedToAvoid;
        }
        else
        {
            // 4. Camino despejado, vamos recto al target
            MoveBot(dirToTarget);
        }

        ControlRotation();
    }

    private Vector2 ChooseBestDirection(Vector2 d1, Vector2 d2, Vector2 d3)
    {
        // Esta es la lógica para decidir a dónde girar
        // Lanzamos rayos para ver cuál está más libre
        if (Physics2D.Raycast(transform.position, d1, detectionDistance, obstacleLayer).collider == null)
            return d1;

        if (Physics2D.Raycast(transform.position, d2, detectionDistance, obstacleLayer).collider == null)
            return d2;

        return d3; // Como último recurso, sigue la perpendicular
    }

    private void MoveBot(Vector2 direction)
    {
        // Aplicamos un suavizado rápido para que el cambio de dirección sea limpio
        Vector2 smoothDir = Vector2.SmoothDamp(
            rb.linearVelocity.normalized,
            direction,
            ref currentVelocitySmoothDamp,
            rotationSmoothTime
        );

        rb.linearVelocity = smoothDir * speed;
        
    }

    private void ControlRotation()
    {
        // Giramos el sprite hacia donde nos movemos
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Cazado!");

            
            SceneManager.LoadScene("SampleScene");

            
        }
    }
}
