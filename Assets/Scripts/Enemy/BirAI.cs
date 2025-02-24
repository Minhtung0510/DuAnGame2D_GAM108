using System.Collections;
using UnityEngine;

public class BirAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float detectionRange = 5f; // Bán kính phát hiện
    public float fireRate = 2f; // Thời gian giữa các lần bắn
    private Transform player;
    private bool isShooting = false;

    void Start()
    {
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));
        if (hit)
        {
            player = hit.transform;
            FlipTowardsPlayer();

            if (!isShooting) // Chỉ bắn khi chưa bắn
            {
                isShooting = true;
                StartCoroutine(ShootRoutine());
            }
        }
        else
        {
            player = null;
            isShooting = false;
            StopCoroutine(ShootRoutine()); // Dừng bắn khi không thấy người chơi
        }
    }

    void FlipTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = player.position.x > transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    IEnumerator ShootRoutine()
    {
        while (isShooting)
        {
            yield return new WaitForSeconds(0.2f); // Đợi 0.2s để animation bắt đầu
            Shoot();
            yield return new WaitForSeconds(fireRate - 0.2f); // Chờ thời gian bắn trừ đi phần chờ animation
        }
    }

    void Shoot()
{
    if (player == null) return;

    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    if (rb)
    {
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);
    }
}




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Vẽ vùng phát hiện
    }
}
