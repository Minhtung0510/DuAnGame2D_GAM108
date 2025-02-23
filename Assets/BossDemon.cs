using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Dùng để gọi Coroutine

public class BossDemon : MonoBehaviour
{
    public float speed = 2f; // Tốc độ di chuyển
    public float detectionRange = 5f; // Phạm vi phát hiện Player
    public float attackRange = 1f; // Phạm vi tấn công
    public float attackCooldown = 1.5f; // Thời gian giữa các lần tấn công
    public Slider healthBar; // Kéo thả thanh máu vào đây trong Inspector
    public int maxHealth = 100;
    private int currentHealth;  
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isChasing = false;
    private bool canAttack = true;
    public GameObject hiddenHP;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        hiddenHP.SetActive(true);
    }

    void Update()
    {
       
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectionRange)
            {
                if (distance <= attackRange && canAttack)
                {
                    StartCoroutine(Attack());
                }
                else if (!isChasing) // Nếu không tấn công, đuổi theo Player
                {
                    isChasing = true;
                    animator.SetBool("isWalk", true);
                }
            }
            else
            {
                if (isChasing)
                {
                    isChasing = false;
                    animator.SetBool("isWalk", false);
                }
                StopMoving();
            }
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // Xoay mặt theo Player
        if ((player.position.x < transform.position.x && isFacingRight) ||
            (player.position.x > transform.position.x && !isFacingRight))
        {
            Flip();
        }
    }

    void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator Attack()
    {
        canAttack = false;
        isChasing = false; // Dừng truy đuổi khi tấn công
        animator.SetBool("isWalk", false);
        animator.SetTrigger("isAttack"); // Kích hoạt animation Attack

        rb.linearVelocity = Vector2.zero; // Dừng di chuyển khi tấn công

        yield return new WaitForSeconds(0.5f); // Đợi animation bắt đầu

        // Trừ máu Player nếu trong phạm vi
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20); // Gây 20 damage cho Player
                Debug.Log("Boss tấn công Player, trừ 20 máu!");
            }
        }

        yield return new WaitForSeconds(attackCooldown); // Chờ hết thời gian hồi chiêu

        canAttack = true;
        isChasing = true; // Tiếp tục truy đuổi nếu Player còn trong phạm vi
    }
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giới hạn từ 0 đến maxHealth
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            
            Die();
          
        }
    }
    void Die()
    {
        // Đặt trigger chết
        animator.SetTrigger("isDeath");

        // Vô hiệu hóa AI, di chuyển và các trạng thái khác
        isChasing = false;
        canAttack = false;
        rb.linearVelocity = Vector2.zero; // Dừng di chuyển

        // Ẩn thanh máu
        hiddenHP.SetActive(false);

        // Tắt các trạng thái animation khác
        animator.ResetTrigger("isAttack");
        animator.SetBool("isWalk", false);

        // Vô hiệu hóa Collider để tránh va chạm sau khi chết
        GetComponent<Collider2D>().enabled = false;

        // Gỡ script để Boss không chạy bất kỳ logic nào nữa
        this.enabled = false;

        // Debug để kiểm tra
        Debug.Log("Boss đã bị hạ gục!");

        // Hủy Boss sau 3 giây
        Destroy(gameObject, 1.5f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack1"))
        {
            TakeDamage(20);
        }
        if (collision.gameObject.CompareTag("Attack2"))
        {
            TakeDamage(20);
        }
        if (collision.gameObject.CompareTag("skill"))
        {
            TakeDamage(40);
        }
    }
}
