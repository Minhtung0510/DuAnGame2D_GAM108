using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Thêm thư viện TextMeshPro
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 5f;
    public float currentHealth;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBar;

    public GameOver _GameOver;
    public Animator anmt;

    void Start()
{
    // Nếu có dữ liệu health đã lưu, sử dụng nó, ngược lại dùng maxHealth
    if (PlayerManager.instance.health > 0)
    {
        currentHealth = PlayerManager.instance.health;
    }
    else
    {
        currentHealth = maxHealth;
        PlayerManager.instance.health = maxHealth; // Đảm bảo PlayerManager cũng cập nhật
    }
    
    UpdateHealthUI();
}


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anmt.SetBool("isHurt", true);
        Invoke("ResetAnimation", 0.5f);
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HP"))
        {
            Heal(2f);
        }
        if (other.CompareTag("Fire") || other.CompareTag("Water"))
        {
            PlayerDie();
        }
    }

    public void UpdateHealthUI()  // Đổi từ private thành public
{
    healthText.text = $"{currentHealth}/{maxHealth}";
    if (healthBar != null)
    {
        healthBar.value = currentHealth / maxHealth;
    }
}

     private void PlayerDie()
    {
        //anmt.SetBool("isDie",true);

        Invoke("ShowGameOver", 1f);

    }
    
    void ShowGameOver()
    {
        if (_GameOver != null)
        {
            _GameOver.ShowGameOverPanel(); // Hiển thị giao diện Game Over
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthUI();
    }

    public void SaveHealth()
    {
        PlayerManager.instance.health = currentHealth;
        PlayerManager.instance.SaveGame();
    }
    void ResetAnimation()
    {
        anmt.SetBool("isHurt", false);
    }
}
