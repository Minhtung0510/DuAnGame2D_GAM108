using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject Prefab; 
    public Transform spawnPoint;    

    [Header("Motion Settings")]
    public float cooldownTime = 0.5f; // Thời gian giữa các lần bắn
    public float throwForce = 10f;    // Lực bắn lên trên

    private float cooldownTimer = 0f;     

    void Update()
    {
        // Đếm thời gian hồi chiêu
        cooldownTimer -= Time.deltaTime;

        // Khi hết hồi chiêu, bắn vật phẩm
        if (cooldownTimer <= 0f)
        {
            Throw();
        }
    }

    void Throw()
    {
        cooldownTimer = cooldownTime; // Đặt lại thời gian hồi chiêu

        // Tạo vật phẩm tại vị trí spawn
        GameObject projectile = Instantiate(Prefab, spawnPoint.position, Quaternion.identity);

        // Thêm lực đẩy lên trên
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * throwForce; // Sử dụng velocity thay vì AddForce để kiểm soát tốt hơn
        }

        // Hủy vật phẩm sau 1.5 giây để tránh rác bộ nhớ
        Destroy(projectile, 2f);
    }
}
