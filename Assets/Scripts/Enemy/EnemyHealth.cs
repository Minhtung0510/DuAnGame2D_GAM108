using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    public Slider healthSlider;
    public Gradient healthGradient;
    public Image fillImage;
    public GameObject healthBarUI;
    public Animator animator;

    private Transform healthBarTransform;
    private Vector3 originalHealthBarScale; // Lưu tỷ lệ gốc của thanh máu

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        healthBarUI.SetActive(true);

        healthBarTransform = healthBarUI.transform; // Lấy Transform của thanh máu
        originalHealthBarScale = healthBarTransform.localScale; // Lưu lại tỷ lệ gốc
    }

    void Update()
    {
        // Giữ thanh máu không bị lật theo Enemy
        if (transform.localScale.x < 0)
        {
            healthBarTransform.localScale = new Vector3(-originalHealthBarScale.x, originalHealthBarScale.y, originalHealthBarScale.z);
        }
        else
        {
            healthBarTransform.localScale = originalHealthBarScale;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead || this == null || gameObject == null) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(SmoothHealthBarUpdate());
        }
    }

    private void UpdateHealthBar()
    {
        if (this == null || gameObject == null) return;

        if (healthSlider != null && fillImage != null)
        {
            healthSlider.value = currentHealth / maxHealth;
            fillImage.color = healthGradient.Evaluate(healthSlider.value);
        }
    }

    private IEnumerator SmoothHealthBarUpdate()
    {
        float targetValue = currentHealth / maxHealth;
        while (Mathf.Abs(healthSlider.value - targetValue) > 0.01f)
        {
            if (this == null || gameObject == null) yield break;

            healthSlider.value = Mathf.Lerp(healthSlider.value, targetValue, Time.deltaTime * 5f);
            fillImage.color = healthGradient.Evaluate(healthSlider.value);
            yield return null;
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        animator.SetTrigger("isDie"); 
        StopAllCoroutines();
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
