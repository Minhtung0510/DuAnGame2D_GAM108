using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Thêm thư viện TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 5f;
    private float currentHealth;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI healthText; // Hiển thị số máu
    [SerializeField] private UnityEngine.UI.Slider healthBar; // Thanh máu (tùy chọn)
    public GameOver _GameOver;
    public Animator anmt;

    void Start()
    {
        // Khởi tạo máu ban đầu
        currentHealth = maxHealth;

        UpdateHealthUI();
        
    }
    void Update(){

    }

    // Hàm nhận sát thương từ enemy
    public void TakeDamage(float damage)
    {
        // Trừ máu
        currentHealth -= damage;
        //StartCoroutine(FlashRed());

        
        // Giới hạn máu không xuống dưới 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Cập nhật UI
        UpdateHealthUI();

        // Kiểm tra nếu player chết
        if (currentHealth <= 0)
        {

            PlayerDie();
        }

        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 
        if (other.CompareTag("HP"))
        {
            Heal(2f);
           
        }
        if (other.CompareTag("Fire"))
        {
            PlayerDie();
        }
        if (other.CompareTag("Water"))
        {
            PlayerDie();
        }
        //if (other.CompareTag("chaps"))
        //{
        //Destroy(gameObject);
    }

    // Cập nhật hiển thị máu trên UI
    private void UpdateHealthUI()
    {
        // Cập nhật text hiển thị máu
        healthText.text = $"{currentHealth}/{maxHealth}";

        // Cập nhật thanh máu
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    private void PlayerDie()
    {
        anmt.SetBool("isDie",true);
        Invoke("ShowGameOver", 1f);
        Destroy(gameObject, 1f);
    }
    void ShowGameOver()
    {
        if (_GameOver != null)
        {
            _GameOver.ShowGameOverPanel(); // Hiển thị giao diện Game Over
        }
    }

    // hồi máu
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthUI();
    }
}
