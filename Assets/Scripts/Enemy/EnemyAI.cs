using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Các điểm tuần tra của Enemy
    public Transform pointA, pointB;
    // Tốc độ di chuyển
    public float speed = 3f;
    // Tốc độ lao tới
    public float chargeSpeed = 10f;
    // Bán kính phát hiện người chơi
    public float detectionRadius = 5f;
    // Layer của người chơi
    public LayerMask playerLayer;
    
    private Transform targetPoint; // Điểm di chuyển hiện tại
    private Transform player; // Người chơi
    private bool chasingPlayer = false; // Trạng thái đuổi theo người chơi
    private bool isWaiting = false; // Kiểm tra xem Enemy có đang chờ không
    private int facingDirection = 1; // Hướng mặt ban đầu (1: phải, -1: trái)

    public Animator anmt;
    public GameObject atk;
    void Start()
    {

        atk.SetActive(false);
        targetPoint = pointA; // Bắt đầu tuần tra từ pointA
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
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        return hit ? hit.transform : null;
    }

    // Di chuyển tuần tra giữa hai điểm
    void Patrol()
    {
        MoveTowards(targetPoint.position, speed);
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA; // Đổi hướng khi đến điểm tuần tra
            FaceTarget(targetPoint.position); // Đảm bảo quay đầu đúng hướng khi tuần tra
        }
    }

    // Enemy lao nhanh tới người chơi rồi dừng lại
    IEnumerator ChargeAtPlayer()
    {
        isWaiting = true;
        Vector2 playerPos = player.position;
        while (Vector2.Distance(transform.position, playerPos) > 0.2f)
        {
            MoveTowards(playerPos, chargeSpeed);
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

    // Di chuyển Enemy đến một vị trí xác định với tốc độ tùy chỉnh
    void MoveTowards(Vector2 destination, float moveSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
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

    // Vẽ phạm vi phát hiện trong Unity Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
