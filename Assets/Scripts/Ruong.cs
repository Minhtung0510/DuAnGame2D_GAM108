using UnityEngine;
using System;

public class Ruong : MonoBehaviour
{
    [Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;
        [Range(70f, 100f)]
        public float dropChance; // Tỉ lệ rơi (%)
        [Range(1, 10)]
        public int maxQuantity = 10; // Số lượng tối đa có thể rơi
    }

    [SerializeField] private Animator animator;
    [SerializeField] private DropItem[] possibleDrops;
    [SerializeField] private float dropForce = 5f;
    [SerializeField] private float dropSpread = 2f;
    [SerializeField] private int guaranteedDrops = 5; // Số item chắc chắn sẽ rơi
    
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip itemDropSound;

    private bool playerInRange = false;
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && !isOpened && Input.GetKeyDown(KeyCode.Q))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        animator.SetTrigger("open");
        
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        DropItems();
    }

    private void DropItems()
    {
        if (possibleDrops.Length == 0) return;

        // Đảm bảo ít nhất guaranteedDrops items sẽ rơi
        int droppedItems = 0;
        while (droppedItems < guaranteedDrops)
        {
            DropItem randomDrop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Length)];
            if (randomDrop != null && randomDrop.itemPrefab != null)
            {
                int quantity = UnityEngine.Random.Range(1, randomDrop.maxQuantity + 1);
                for (int i = 0; i < quantity; i++)
                {
                    SpawnItem(randomDrop.itemPrefab);
                }
                droppedItems++;
            }
        }

        // Kiểm tra xác suất cho các items còn lại
        foreach (DropItem drop in possibleDrops)
        {
            if (UnityEngine.Random.Range(70f, 100f) <= drop.dropChance)
            {
                int quantity = UnityEngine.Random.Range(1, drop.maxQuantity + 1);
                for (int i = 0; i < quantity; i++)
                {
                    SpawnItem(drop.itemPrefab);
                }
            }
        }
    }

    private void SpawnItem(GameObject itemPrefab)
    {
        // Tạo vị trí ngẫu nhiên xung quanh rương
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * dropSpread;
        Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, 1, 0);

        // Tạo item
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        
        // Thêm lực đẩy ngẫu nhiên
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * dropForce, ForceMode2D.Impulse);
        }

        // Phát âm thanh khi item rơi
        if (audioSource != null && itemDropSound != null)
        {
            audioSource.PlayOneShot(itemDropSound);
        }
    }
}