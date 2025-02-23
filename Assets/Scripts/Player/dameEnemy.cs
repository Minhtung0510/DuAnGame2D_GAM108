using UnityEngine;

public class dameEnemy : MonoBehaviour
{
    [SerializeField] private float damageAmount = 20f; // Lượng sát thương gây ra
    [SerializeField] private bool destroyOnHit = true; // Có tự hủy khi va chạm không (ví dụ: đạn)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có EnemyHealth hay không
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount); // Gây sát thương

            if (destroyOnHit)
            {
                Destroy(gameObject); // Hủy đối tượng này nếu cần (đạn, phép,...)
            }
        }
    }
}
