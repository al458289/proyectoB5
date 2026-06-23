using UnityEngine;
using UnityEngine.SceneManagement;

public class BotController : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] private float speed = 4f; 
    [SerializeField] private float rotationSmoothTime = 0.05f; 
    [SerializeField] private Transform target; 
    [SerializeField] private Rigidbody2D rb;

    [Header("Radar (Evitar Obstáculos)")]
    [SerializeField] private float detectionDistance = 1.2f; 
    [SerializeField] private float circleCastRadius = 0.3f; 
    [SerializeField] private LayerMask obstacleLayer;

    
    [SerializeField] private float timeForcedToAvoid = 0.8f; 
    private float avoidanceForcedTimer = 0f;
    private Vector2 chosenAvoidanceDir;
    

    private bool hasStarted = false;
    private Vector2 currentVelocitySmoothDamp;

    public void ActivarPersecucion()
    {
        hasStarted = true;
        
    }

    void FixedUpdate()
    {
        if (!hasStarted || target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dirToTarget = (target.position - transform.position).normalized;

        
        if (avoidanceForcedTimer > 0)
        {
            avoidanceForcedTimer -= Time.fixedDeltaTime;
            
            MoveBot(chosenAvoidanceDir);
            ControlRotation();
            return;
        }

        
        RaycastHit2D hitCenter = Physics2D.CircleCast(
            transform.position,
            circleCastRadius,
            dirToTarget,
            detectionDistance,
            obstacleLayer
        );

        
        if (hitCenter.collider != null)
        {
            
            Vector2 left60Dir = Quaternion.Euler(0, 0, 60) * dirToTarget;
            Vector2 right60Dir = Quaternion.Euler(0, 0, -60) * dirToTarget;

            
            Vector2 perpendicular = Vector2.Perpendicular(hitCenter.normal);
            float dot = Vector2.Dot(perpendicular, dirToTarget);
            Vector2 wallPerpendicularDir = perpendicular * (dot > 0 ? 1 : -1);

            
            chosenAvoidanceDir = ChooseBestDirection(left60Dir, right60Dir, wallPerpendicularDir);

            
            avoidanceForcedTimer = timeForcedToAvoid;
        }
        else
        {
            
            MoveBot(dirToTarget);
        }

        ControlRotation();
    }

    private Vector2 ChooseBestDirection(Vector2 d1, Vector2 d2, Vector2 d3)
    {
        
        if (Physics2D.Raycast(transform.position, d1, detectionDistance, obstacleLayer).collider == null)
            return d1;

        if (Physics2D.Raycast(transform.position, d2, detectionDistance, obstacleLayer).collider == null)
            return d2;

        return d3; 
    }

    private void MoveBot(Vector2 direction)
    {
        
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
            
            

            SceneManager.LoadScene("SampleScene");

            
        }
    }
}
