using UnityEngine;

public class PigMovement : MonoBehaviour
{
    // Biến công khai để điều chỉnh tốc độ di chuyển và tốc độ húc
    public float speed = 2f; // Tốc độ di chuyển thông thường
    public float chargeSpeed = 8f; // Tốc độ húc khi phát hiện người chơi
    public float detectionRange = 3f; // Phạm vi phát hiện người chơi
    public LayerMask playerLayer; // Layer dùng để xác định người chơi

    private Rigidbody2D rb; // Thành phần Rigidbody2D để điều khiển vật lý
    private int direction = 1; // Hướng di chuyển ban đầu (1 là phải, -1 là trái)
    private bool isCharging = false; // Trạng thái húc
    private bool isStunned = false; // Trạng thái bất động sau khi húc

    public Animator anmt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của đối tượng
    }

    void FixedUpdate()
    {
        // Nếu không húc và không bị choáng, tiếp tục di chuyển và kiểm tra người chơi
        if (!isCharging && !isStunned)
        {
            
            Move();
            anmt.SetBool("stun", false);
            DetectPlayer();
        }
    }

    void Move()
    {
        // Di chuyển theo hướng hiện tại với tốc độ cố định
        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);
    }

    void DetectPlayer()
    {
        // Xác định khu vực quét trước mặt nhân vật
        Vector2 origin = transform.position;
        Vector2 size = new Vector2(detectionRange, 1f);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector2.right * direction, detectionRange, playerLayer);

        if (hit.collider != null) // Nếu phát hiện người chơi
        {
            FacePlayer(hit.collider.transform); // Quay mặt về phía người chơi
            StopAndCharge(); // Dừng lại trước khi húc
        }
    }

    void FacePlayer(Transform player)
    {
        // Xác định hướng của người chơi so với Pig
        int playerDirection = player.position.x > transform.position.x ? 1 : -1;
        if (playerDirection != direction)
        {
            direction = playerDirection; // Cập nhật hướng
            Flip(); // Lật nhân vật để đối mặt người chơi
        }
    }

    void StopAndCharge()
    {
        isStunned = true; // Đặt trạng thái bất động
        rb.linearVelocity = Vector2.zero; // Dừng di chuyển
        anmt.SetBool("is1", true);
        Invoke(nameof(Charge), 1f); // Dừng 1 giây trước khi húc
    }

    void Charge()
    {
        isStunned = false; // Hết bất động
        isCharging = true; // Bắt đầu húc
        rb.linearVelocity = new Vector2(chargeSpeed * direction, 0f); // Di chuyển nhanh về phía trước
        anmt.SetBool("isAtk", true);
        Invoke(nameof(StopCharge), 1f); // Húc trong 0.5 giây rồi dừng lại
    }

    void StopCharge()
    {
        isCharging = false; // Kết thúc húc
        rb.linearVelocity = Vector2.zero; // Dừng lại hoàn toàn
        isStunned = true; // Bắt đầu trạng thái bất động
        anmt.SetBool("stun", true);
        Invoke(nameof(Recover), 2f); // Dừng 2 giây trước khi di chuyển lại
    }

    void Recover()
    {
        isStunned = false; // Hết bất động, tiếp tục di chuyển bình thường
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu không húc mà va chạm, đổi hướng di chuyển
        if (!isCharging)
        {
            direction *= -1;
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f); // Quay ngang nhân vật khi đổi hướng
    }

    void OnDrawGizmos()
    {
        // Vẽ khu vực quét phát hiện người chơi trong chế độ Scene
        Gizmos.color = Color.red;
        Vector2 origin = transform.position;
        Vector2 size = new Vector2(detectionRange, 1f);
        Gizmos.DrawWireCube(origin + Vector2.right * direction * detectionRange / 2, size);
    }
}
