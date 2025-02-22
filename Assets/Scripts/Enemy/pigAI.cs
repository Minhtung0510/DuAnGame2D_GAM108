using System.Collections;
using UnityEngine;

public class pigAI : MonoBehaviour
{
    // Các điểm tuần tra của Enemy
    public Transform pointA, pointB;
    // Tốc độ di chuyển
    public float speed = 3f;
    // Tốc độ lao tới
    public float chargeSpeed = 10f;
    // Kích thước phạm vi phát hiện người chơi
    public Vector2 detectionSize = new Vector2(5f, 2f);
    // Layer của người chơi
    public LayerMask playerLayer;
    // Layer của mặt đất
    public LayerMask groundLayer;
    
    private Transform targetPoint; // Điểm di chuyển hiện tại
    private Transform player; // Người chơi
    private bool chasingPlayer = false; // Trạng thái đuổi theo người chơi
    private bool isWaiting = false; // Kiểm tra xem Enemy có đang chờ không
    private int facingDirection = 1; // Hướng mặt ban đầu (1: phải, -1: trái)
    private Rigidbody2D rb;

    public Animator anmt;
    public GameObject atk;
    
    void Start()
    {
        atk.SetActive(false);
        targetPoint = pointA; // Bắt đầu tuần tra từ pointA
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x > pointB.position.x)
        {
            Flip(); // Đảm bảo Enemy hướng đúng ngay từ đầu
        }
    }

    void Update()
    {
        if (!isWaiting)
        {
            player = DetectPlayer(); // Kiểm tra xem có người chơi trong phạm vi không
            if (player != null)
            {
                chasingPlayer = true;
                FaceTarget(player.position); // Quay mặt về phía người chơi
                StartCoroutine(ChargeAtPlayer()); // Lao nhanh tới người chơi rồi dừng lại
            }
            else if (chasingPlayer)
            {
                StartCoroutine(WaitBeforeReturning()); // Nếu mất dấu người chơi, chờ 2 giây rồi quay lại tuần tra
            }
            else
            {
                Patrol(); // Nếu không có người chơi, tiếp tục tuần tra
            }
        }
    }

    // Kiểm tra người chơi trong phạm vi phát hiện
    Transform DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, detectionSize, 0f, playerLayer);
        return hit ? hit.transform : null;
    }

    // Kiểm tra xem Enemy có đang trên mặt đất không
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + Vector3.down * 0.1f, 0.2f, groundLayer);
    }

    // Di chuyển tuần tra giữa hai điểm
    void Patrol()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(facingDirection * speed, rb.linearVelocity.y);
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                targetPoint = targetPoint == pointA ? pointB : pointA; // Đổi hướng khi đến điểm tuần tra
                FaceTarget(targetPoint.position); // Đảm bảo quay đầu đúng hướng khi tuần tra
            }
        }
    }

    // Enemy lao nhanh tới người chơi rồi dừng lại
    IEnumerator ChargeAtPlayer()
    {
        isWaiting = true;
        Vector2 playerPos = player.position;
        while (Vector2.Distance(transform.position, playerPos) > 0.2f && IsGrounded())
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(playerPos.x - transform.position.x) * chargeSpeed, rb.linearVelocity.y);
            yield return null;
            anmt.SetBool("atk", true);
            atk.SetActive(true);
        }
        atk.SetActive(false);
        anmt.SetBool("atk", false);
        anmt.SetBool("stun", true);
        yield return new WaitForSeconds(2f);
        isWaiting = false;
        anmt.SetBool("stun", false);
    }

    // Quay đầu Enemy khi thay đổi hướng
    void Flip()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Quay mặt Enemy về phía mục tiêu
    void FaceTarget(Vector2 targetPosition)
    {
        if ((targetPosition.x > transform.position.x && facingDirection < 0) ||
            (targetPosition.x < transform.position.x && facingDirection > 0))
        {
            Flip();
        }
    }

    // Chờ 2 giây nếu mất dấu người chơi rồi quay lại tuần tra
    IEnumerator WaitBeforeReturning()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        chasingPlayer = false;
        targetPoint = (Vector2.Distance(transform.position, pointA.position) < Vector2.Distance(transform.position, pointB.position)) ? pointB : pointA;
        FaceTarget(targetPoint.position); // Đảm bảo Enemy quay đúng hướng khi trở lại tuần tra
        isWaiting = false;
    }

    // Vẽ phạm vi phát hiện trong Unity Editor dưới dạng hình chữ nhật
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, detectionSize);
    }
}
