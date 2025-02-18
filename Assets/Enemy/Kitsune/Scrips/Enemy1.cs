
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform startPoint; // Điểm bắt đầu
    [SerializeField] Transform endPoint;   // Điểm kết thúc
    [SerializeField] Rigidbody2D rb;
    public int maxHealth = 100;  // Máu tối đa của kẻ địch
    private int currentHealth;   // Máu hiện tại của kẻ địch
    private bool movingToEnd = true; // Xác định hướng di chuyển
    private Vector2 currentTarget;  // Mục tiêu hiện tại

    void Start()
    {
        // Khởi tạo máu hiện tại bằng máu tối đa
        currentHealth = maxHealth;
        // Khởi tạo mục tiêu là endPoint
        currentTarget = endPoint.position;
    }
    void Update()
    {
        Move();
    }

    void Move()
{
    Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    rb.linearVelocity = direction * moveSpeed;
}


    void OnTriggerExit2D(Collider2D collision)
    {
        HorizontalFlip();
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
    transform.localScale = new Vector2(-6f, 5f);
    Debug.Log("Enemy flipped direction. New scale: " + transform.localScale.x);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    // Instantiate(damageEffect, transform.position, Quaternion.identity);
    Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Hàm chết
    void Die()
    {
        Debug.Log("Enemy died!");
        // audioSource.PlayOneShot(deathSound);
        Destroy(gameObject);
    }
     void FlipDirection()
    {
        // Đổi hướng di chuyển
        movingToEnd = !movingToEnd;
        currentTarget = movingToEnd ? endPoint.position : startPoint.position;

        // Lật hình ảnh để quái hướng về đúng phía
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
