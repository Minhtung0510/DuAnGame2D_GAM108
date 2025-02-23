using System.Collections;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    public Transform pointA, pointB;
    public float patrolSpeed = 2f;
    public float dashSpeed = 10f;
    public Vector2 detectionSize = new Vector2(5f, 3f);
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 targetPosition;
    private bool isChasing = false;
    private int facingDirection = 1;

    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        targetPosition = pointA.position;
        anim.SetBool("isPatrolling", true);
    }

    void Update()
    {
        DetectPlayer();
    }

    void FixedUpdate()
    {
        if (!isChasing)
        {
            MoveEnemy();
        }
    }

    void DetectPlayer()
    {
        Vector2 detectionOrigin = (Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2;
        Collider2D detectedPlayer = Physics2D.OverlapBox(detectionOrigin, detectionSize, 0, playerLayer);
        
        if (detectedPlayer && !isChasing)
        {
            player = detectedPlayer.transform;
            anim.SetBool("isPatrolling", false);
            anim.SetTrigger("detectPlayer");
            StartCoroutine(DashToPlayer());
        }
    }

    IEnumerator DashToPlayer()
    {
        isChasing = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("prepareDash");
        yield return new WaitForSeconds(1f);
        
        while (player && Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2, detectionSize, 0, playerLayer))
        {
            targetPosition = player.position;
            FaceTarget(targetPosition);
            anim.SetTrigger("dash");
            DashTowards(targetPosition);
            yield return new WaitForSeconds(2f);
            
            if (Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2, detectionSize, 0, playerLayer))
            {
                rb.linearVelocity = Vector2.zero;
                anim.SetTrigger("pause");
                yield return new WaitForSeconds(1f);
            }
        }
        StopChase();
    }

    void StopChase()
    {
        isChasing = false;
        player = null;
        targetPosition = (Vector2.Distance(transform.position, pointA.position) < Vector2.Distance(transform.position, pointB.position)) ? pointB.position : pointA.position;
        FaceTarget(targetPosition);
        
        anim.ResetTrigger("detectPlayer");
        anim.ResetTrigger("dash");
        anim.ResetTrigger("pause");
        anim.ResetTrigger("prepareDash"); // Đảm bảo không kẹt trigger
        anim.SetBool("isPatrolling", true);
    }

    void MoveEnemy()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == (Vector2)pointA.position) ? pointB.position : pointA.position;
        }
        FaceTarget(targetPosition);
        MoveTowards(targetPosition, patrolSpeed);
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void DashTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * dashSpeed;
    }

    void FaceTarget(Vector2 target)
    {
        facingDirection = (target.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 detectionOrigin = (Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2;
        Gizmos.DrawWireCube(detectionOrigin, detectionSize);
    }
}
