using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EvilWizardBoss2 : MonoBehaviour
{
    public float detectionRangeAttack = 3f;  // Phạm vi phát hiện người chơi attack
    public float detectionRange = 7f;  // Phạm vi phát hiện người chơi follow

    public float stopRange;
    public Transform Player;//follow player

    public float fireInterval = 6f;// Thời gian giữa các đợt tấn công
    public float attackInterval = 2f;// Thời gian giữa các đợt tấn công
    private float nextAttackTime;

    //vị trí tấn công
    public Transform fire;
    public Transform fire2;
    public Transform fire3;
    public Transform fire4;
    public Transform fire5;
    //fire ball
    public GameObject fireBall;
    public GameObject fireBall2;
    public GameObject fireBall3;
    public GameObject fireBall4;
    public GameObject fireBall5;
    //boss chết thì tắt tilemap
    public Tilemap Tilemap;


    public Transform attack;//vị trí tấn công
    public GameObject attackSkill;//skill 1  
    public GameObject attackSkill2;//skill 2

   
    public float health = 700;

    private bool isTakeAttack;
    private bool stopAttack;
    private bool right;

    public float timeAttack;
    public float durection = 0.5f;
    private bool attackActive = true;
    private bool isAttacking = false;

    Vector2 moveDirection;
    Animator animator;
    Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
        
    }

  
    void Update()
    {
        if (health > 0)
        {
            if (stopAttack)
            {               
                followPlayer();
                Attack();
            }
        }
        if (health <= 0)
        {            
            isTakeAttack = false;
            Tilemap.gameObject.SetActive(false);
        }
        else
        {
            isTakeAttack = true;
        }
    }
    void Attack()
    {
        if(stopAttack) { 
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            if (distanceToPlayer <= detectionRangeAttack && Time.time >= nextAttackTime && !isAttacking)
            {
                nextAttackTime = Time.time + attackInterval;
                int attackType = Random.Range(0, 3);  // Random số nguyên từ 0 tới 2 (0 hoặc 2)

                if (attackType == 0)
                {
                    timeAttack = Time.time + durection;
                    animator.SetTrigger("isAttack");
                    isAttacking = true;
                    attackActive = false;
}
                if (attackType == 1)
                {
                    timeAttack = Time.time + durection;
                    animator.SetTrigger("isAttack2");
                   isAttacking = true;
                    attackActive = false;
                }
                if (attackType == 2)
                {
                    var fireTmp = Instantiate(fireBall, fire.position, Quaternion.identity);
                    Destroy(fireTmp, 5f);
                    var fireTmp2 = Instantiate(fireBall2, fire2.position, Quaternion.identity);
                    Destroy(fireTmp2, 5f);
                    var fireTmp3 = Instantiate(fireBall3, fire3.position, Quaternion.identity);
                    Destroy(fireTmp3, 5f);
                    var fireTmp4 = Instantiate(fireBall4, fire4.position, Quaternion.identity);
                    Destroy(fireTmp4, 5f);
                    var fireTmp5 = Instantiate(fireBall5, fire5.position, Quaternion.identity);
                    Destroy(fireTmp5, 5f);
                }
            }
            if (isAttacking&&Time.time >= timeAttack )
            {
                var oneSkill = Instantiate(attackSkill, attack.position, Quaternion.identity);
                Destroy(oneSkill, 0.1f);

                var oneSkill2 = Instantiate(attackSkill2, attack.position, Quaternion.identity);
                Destroy(oneSkill2, 0.1f);
                attackActive = true;
                isAttacking = false;
            }
        }
              
    }
    private void followPlayer()//thấy player thì chạy theo
    {
        if (stopAttack )
        {
            
            // Tính khoảng cách giữa quái vật và người chơi
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
            // Nếu khoảng cách nhỏ hơn phạm vi phát hiện, quái vật sẽ đuổi theo người chơi
            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer > stopRange)
                {
                    moveDirection = (Player.position - transform.position).normalized;
                    transform.Translate(moveDirection * 2f * Time.deltaTime);// Tốc độ di chuyển
                    animator.SetBool("isRun", true);

                    //xoay mặt
                    Flip();
                }
                else
                {
                    animator.SetBool("isRun", false);
                }
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        }
    }
    private void Flip()//xoay mat
    {
        if (right && moveDirection.x < 0 || !right && moveDirection.x > 0)
        {
            right = !right;
            Vector3 kichThuoc = transform.localScale;
            kichThuoc.x = kichThuoc.x * -1;
            transform.localScale = kichThuoc;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTakeAttack)
        {
            //chạm shuriken
            if (other.gameObject.CompareTag("Shuriken"))
            {
                if (health > 0)
                {
                    health -= 5;
                    if (attackActive)
                    {
                        animator.SetTrigger("isTakeHit");
                    }

                }
                Destroy(other.gameObject);
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("Attack1"))
            {
                if (health > 0)
                {
                    health -= 10;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }
               
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("Attack2"))
            {
                if (health > 0)
                {
                    health -= 20;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }
               
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("Attack3"))
            {
                if (health > 0)
                {
                    health -= 30;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }
                
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("SpecialAttack"))
            {
                if (health > 0)
                {
                    health -= 100;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }
                
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if(health <= 0)
            {
                Destroy(gameObject,5f);
                animator.SetBool("isDeath",true);
            }
        }
        
    }
}
