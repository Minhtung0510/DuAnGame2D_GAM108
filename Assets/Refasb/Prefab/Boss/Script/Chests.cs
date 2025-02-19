using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    Animator animator;
    public GameObject itemPrefab; // Prefab của vật phẩm
    public Transform itemSpawnPoint; // Điểm xuất hiện của vật phẩm   
    private bool isOpen = true;
    private bool isMovingItem = false;
    private GameObject spawnedItem;
    private Vector3 itemStartPos;
    private Vector3 itemEndPos;
    private float itemMoveDuration = 1f; // Thời gian vật phẩm bay lên
    private float itemMoveElapsed = 0;

    private BoxCollider2D chestsCollider;
    public float health;
    void Start()
    {
        
            animator = GetComponent<Animator>();
            chestsCollider = GetComponent<BoxCollider2D>();
            health = 3;
        
    }

    void Update()
    {
        
            if (isMovingItem && spawnedItem != null) // Kiểm tra nếu item còn tồn tại
            {
                itemMoveElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(itemMoveElapsed / itemMoveDuration);
                spawnedItem.transform.position = Vector3.Lerp(itemStartPos, itemEndPos, t);

                if (t >= 1.0f)
                {
                    isMovingItem = false;
                    chestsCollider.isTrigger = true;
                }
            }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    if(health >=0) 
    { 
        if (health <= 0)
        {
            animator.SetBool("isOpen",true);
            if (isOpen)
            {                   
                    isOpen = false;
                    spawnedItem = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
                    itemStartPos = spawnedItem.transform.position;
                    itemEndPos = itemStartPos + new Vector3(0, 1f, 0); //vi tri bay len
                    isMovingItem = true;
                    itemMoveElapsed = 0; // Đặt lại thời gian đã trôi qua cho chuyển động
            }
        }
        
            if (other.gameObject.CompareTag("Shuriken"))
            {
                health -= 1;
                Destroy(other.gameObject);//shuriken biến mất
                animator.SetTrigger("isHit");
            }

            if (other.gameObject.CompareTag("Attack1"))
            {
                health -= 1;
                animator.SetTrigger("isHit");
            }

            if (other.gameObject.CompareTag("Attack2"))
            {
                health -= 1;
                animator.SetTrigger("isHit");
            }
            if (other.gameObject.CompareTag("Attack3"))
            {
                health -= 1;
                animator.SetTrigger("isHit");
            }

            if (other.gameObject.CompareTag("SpecialAttack"))
            {
                animator.SetTrigger("isHit");
                health -= 1;
            }
        }
    }
}
