using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class HunterController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private Transform target;

    [Header("Radar (Evitar Obstáculos)")]
    [SerializeField] private float detectionDistance = 1.2f;
    [SerializeField] private float circleCastRadius = 0.3f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Evasión")]
    [SerializeField] private float timeForcedToAvoid = 0.6f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D hunterCollider;

    private bool hasStarted = false;
    private float avoidanceForcedTimer = 0f;
    private Vector2 chosenAvoidanceDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hunterCollider = GetComponent<Collider2D>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        SetHunterActive(false);
    }

    void FixedUpdate()
    {
        if (!hasStarted)
        {
            CheckActivation();
            return;
        }

        if (target == null) return;

        HandleMovement();
    }

    private void CheckActivation()
    {
        if (GameManager.Instance.puzzle5Completado && GameManager.Instance.puzzle4Completado&& GameManager.Instance.textoEnseñado)
        {
            SetHunterActive(true);
            hasStarted = true;
        }
    }

    private void HandleMovement()
    {
        Vector2 dirToTarget = (target.position - transform.position).normalized;
        Vector2 finalMoveDir;

        if (avoidanceForcedTimer > 0)
        {
            avoidanceForcedTimer -= Time.fixedDeltaTime;
            finalMoveDir = chosenAvoidanceDir;
        }
        else
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleCastRadius, dirToTarget, detectionDistance, obstacleLayer);

            if (hit.collider != null)
            {
                finalMoveDir = CalculateAvoidance(dirToTarget, hit.normal);
                chosenAvoidanceDir = finalMoveDir;
                avoidanceForcedTimer = timeForcedToAvoid;
            }
            else
            {
                finalMoveDir = dirToTarget;
            }
        }

        ApplyMovementAndVisuals(finalMoveDir);
    }

    private void ApplyMovementAndVisuals(Vector2 moveDir)
    {
        rb.linearVelocity = moveDir * speed;

        float h = moveDir.x;
        float v = moveDir.y;
        float currentSpeedSqr = moveDir.sqrMagnitude;

        
        animator.SetFloat("Speed", currentSpeedSqr > 0.01f ? 1f : 0f);

        
        if (currentSpeedSqr > 0.01f)
        {
            
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                
                animator.SetFloat("Horizontal", h > 0 ? 1 : -1);
                animator.SetFloat("Vertical", 0);

                
                spriteRenderer.flipX = h < 0;
            }
            else
            {
                
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", v > 0 ? 1 : -1);

                
                spriteRenderer.flipX = false;
            }
        }
    }

    private Vector2 CalculateAvoidance(Vector2 currentDir, Vector2 hitNormal)
    {
        Vector2 perpendicular = Vector2.Perpendicular(hitNormal);
        float dot = Vector2.Dot(perpendicular, currentDir);
        return perpendicular * (dot > 0 ? 1 : -1);
    }

    private void SetHunterActive(bool active)
    {
        spriteRenderer.enabled = active;
        hunterCollider.enabled = active;
        if (!active) rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance?.PrepararGameOver();
            
            SceneManager.LoadScene("GameOver");
        }
    }
}