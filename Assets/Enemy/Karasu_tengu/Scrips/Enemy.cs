using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Rigidbody2D rb;

    public int maxHealth = 10;
    private int currentHealth;
    private bool movingToEnd = true;
    private Vector2 currentTarget;

    void Start()
    {
        currentHealth = maxHealth;
        currentTarget = endPoint.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            FlipDirection();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary")) 
        {
            HorizontalFlip();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with the player!");
            // Gọi hàm tấn công hoặc giảm máu của người chơi tại đây.
        }
    }

    private void HorizontalFlip()
    {
        float newScaleX = -Mathf.Sign(rb.linearVelocity.x);
        transform.localScale = new Vector2(newScaleX * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        Debug.Log("Enemy flipped direction. New scale: " + transform.localScale.x);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    void FlipDirection()
    {
        movingToEnd = !movingToEnd;
        currentTarget = movingToEnd ? endPoint.position : startPoint.position;

        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
