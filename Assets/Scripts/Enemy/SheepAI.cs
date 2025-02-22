using System.Collections;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    // Các điểm tuần tra của cừu
    public Transform pointA, pointB;
    // Tốc độ di chuyển khi tuần tra
    public float patrolSpeed = 2f;
    // Tốc độ lao tới người chơi
    public float dashSpeed = 10f;
    // Kích thước phạm vi phát hiện
    public Vector2 detectionSize = new Vector2(5f, 3f);
    // Lớp đối tượng có thể phát hiện (người chơi)
    public LayerMask playerLayer;

    // Thành phần Rigidbody2D của cừu
    private Rigidbody2D rb;
    // Người chơi đã phát hiện
    private Transform player;
    // Vị trí mục tiêu hiện tại
    private Vector2 targetPosition;
    // Cờ kiểm tra cừu có đang đuổi theo người chơi không
    private bool isChasing = false;
    // Hướng mà cừu đang quay mặt (1 = phải, -1 = trái)
    private int facingDirection = 1;

    public Animator anmt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D
        targetPosition = pointA.position; // Bắt đầu tuần tra từ điểm A
    }

    void Update()
    {
        DetectPlayer(); // Kiểm tra xem có phát hiện người chơi không
    }

    void FixedUpdate()
    {
        if (!isChasing)
        {
            MoveEnemy(); // Nếu không đuổi theo người chơi, thực hiện tuần tra
        }
    }

    void DetectPlayer()
    {
        // Xác định vị trí gốc để kiểm tra phát hiện người chơi
        Vector2 detectionOrigin = (Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2;
        // Kiểm tra có người chơi trong phạm vi phát hiện không
        Collider2D detectedPlayer = Physics2D.OverlapBox(detectionOrigin, detectionSize, 0, playerLayer);
        
        if (detectedPlayer)
        {
            if (!isChasing) // Nếu chưa đuổi theo, bắt đầu đuổi
            {
                player = detectedPlayer.transform;
                StartCoroutine(DashToPlayer());
            }
        }
    }

    IEnumerator DashToPlayer()
    {
        isChasing = true; // Đánh dấu trạng thái đang đuổi
        rb.linearVelocity = Vector2.zero; // Dừng di chuyển
        anmt.SetBool("isStand",true);
        yield return new WaitForSeconds(1f); // Dừng 1 giây trước khi lao tới
        
        while (player && Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2, detectionSize, 0, playerLayer))
        {
            targetPosition = player.position; // Cập nhật vị trí mục tiêu
            FaceTarget(targetPosition); // Quay mặt về phía mục tiêu
            anmt.SetBool("atk", true);
            DashTowards(targetPosition); // Lao nhanh tới mục tiêu
            anmt.SetBool("isHit", true);
            yield return new WaitForSeconds(2f); // Dừng lại 2 giây sau khi lao tới
            
            
            // Nếu người chơi vẫn trong phạm vi, dừng 1 giây rồi lao tiếp
            if (Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2, detectionSize, 0, playerLayer))
            {
                rb.linearVelocity = Vector2.zero;
                yield return new WaitForSeconds(1f);
                
            }
            anmt.SetBool("isRun", true);
        }
        
        StopChase(); // Nếu mất dấu người chơi, quay lại tuần tra
        
    }

    void StopChase()
    {
        isChasing = false; // Ngừng đuổi
        player = null; // Xóa mục tiêu
        // Chọn điểm tuần tra gần nhất để quay lại tuần tra
        targetPosition = (Vector2.Distance(transform.position, pointA.position) < Vector2.Distance(transform.position, pointB.position)) ? pointB.position : pointA.position;
        FaceTarget(targetPosition);
    }

    void MoveEnemy()
    {
        // Nếu đã đến điểm tuần tra, đổi hướng tới điểm còn lại
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == (Vector2)pointA.position) ? pointB.position : pointA.position;
        }
        FaceTarget(targetPosition);
        MoveTowards(targetPosition, patrolSpeed);
    }

    void MoveTowards(Vector2 target, float speed)
    {
        // Tính toán hướng di chuyển và đặt vận tốc tương ứng
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void DashTowards(Vector2 target)
    {
        // Tính toán hướng lao tới và đặt vận tốc lớn
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * dashSpeed;
    }

    void FaceTarget(Vector2 target)
    {
        // Xác định hướng quay mặt dựa trên vị trí mục tiêu
        facingDirection = (target.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Vẽ phạm vi phát hiện để debug
        Vector2 detectionOrigin = (Vector2)transform.position + Vector2.right * facingDirection * detectionSize.x / 2;
        Gizmos.DrawWireCube(detectionOrigin, detectionSize);
    }
}