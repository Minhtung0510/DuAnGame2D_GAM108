using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    [Header("Di Chuyển")]
    [SerializeField] private float leftLimit;   // Giới hạn bên trái
    [SerializeField] private float rightLimit;  // Giới hạn bên phải
    [SerializeField] private float speed = 2f;  // Tốc độ di chuyển
    private bool movingRight = true;
    private Rigidbody2D rb;
    private Vector3 initialScale;

    [Header("Tấn Công")]
    public float attackRange = 1.5f;   // Phạm vi tấn công
    public float attackCooldown = 1.5f;// Thời gian giữa các đòn đánh
    public int attackDamage = 20;      // Sát thương gây ra
    private bool canAttack = true;

    [Header("Máu")]
    public Slider healthSlider;
    public int maxHealth = 100;
    private int currentHealth;

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Thiết lập máu
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (transform.position.x <= leftLimit)
        {
            movingRight = true;
        }
        else if (transform.position.x >= rightLimit)
        {
            movingRight = false;
        }

        float moveDirection = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        // Đổi hướng quái
        transform.localScale = new Vector3(movingRight ? -initialScale.x : initialScale.x, initialScale.y, initialScale.z);
    }

    private void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero; // Dừng di chuyển

        if (canAttack)
        {
            canAttack = false;
            Debug.Log("Quái tấn công!");

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Quái đã chết!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))  // Kiểm tra nếu trúng đạn
        {
            TakeDamage(20);
            Destroy(collision.gameObject); // Hủy đạn sau khi trúng
        }
    }
}
