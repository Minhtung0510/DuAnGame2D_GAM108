using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BringerOfDeath : MonoBehaviour
{

    public float detectionRangeAttack = 2.5f;  // Phạm vi phát hiện người chơi
    public float detectionRangeAttak2 = 5f;
    public float detectionRange = 7f;  // Phạm vi phát hiện người chơi
    private float nextAttackTime;
    public GameObject Skill2;

    public Transform Player;//follow player
    private bool isAttack2;
    private float attackEndTime;
    private bool isAttacking = false;
    private int health;

    public GameObject attackSkill;//skill
    public Transform attack;//vị trí tấn công

    private bool isTakeDamage;
    private float TimeAttackRate = 3f;
    private float timeAttack;

    private bool right;
    private bool stopAttack = true;

    Animator animator;
    Rigidbody2D rb;

    public Tilemap tilemap;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = 500;    
        timeAttack = TimeAttackRate;
        
    }

    void Update()
    {
        if (stopAttack)
        {
            if (health > 0)
            {
                followPlayer();
                TimeAttack();//độ trể khi thấy player sau 3f tấn công
                Attack2();
                isTakeDamage = true;
            }
            else
            {
                isTakeDamage = false;
            }
        }
    }
    void TimeAttack()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer < detectionRangeAttack)
        {
            timeAttack -= Time.deltaTime;
            
            if (timeAttack <= 0)
            {                
                    //animation tấn công
                    animator.SetTrigger("isAttack");
                    var oneSkill = Instantiate(attackSkill, attack.position, Quaternion.identity);
                    Destroy(oneSkill, 0.1f);
                    timeAttack = TimeAttackRate;  
                    isAttack2 = false;
            }
        }
        else
        {
            isAttack2 = true;
        }
    }
    void Attack2()
    {
        float distance = Vector3.Distance(transform.position , Player.position);
        if(distance < detectionRangeAttak2 && Time.time >= nextAttackTime + 3f && !isAttacking)
        {
            nextAttackTime = Time.time;
            if (isAttack2)
            {
                attackEndTime = Time.time + 0.5f;      
                animator.SetTrigger("isAttack2");
                isAttacking = true;

            }
            
        }
        if (isAttacking && Time.time > attackEndTime)
        {           
            Vector3 skillPosition = Player.position + new Vector3(0, 0.8f, 0);
            var skillTance = Instantiate(Skill2, skillPosition, Quaternion.identity);
            Destroy(skillTance, 0.5f);
            isAttacking = false;
        }
    }

    private void followPlayer()//thấy player thì chạy theo
    {
        // Tính khoảng cách giữa quái vật và người chơi
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        // Nếu khoảng cách nhỏ hơn phạm vi phát hiện, quái vật sẽ đuổi theo người chơi
        if (distanceToPlayer < detectionRange)
        {
            tilemap.gameObject.SetActive(true);
            
            Vector2 moveDirection = (Player.position - transform.position).normalized;
                rb.linearVelocity = moveDirection * 1.5f;// Tốc độ di chuyển

                animator.SetBool("isRun",true);


                //xoay mặt
                if (right && moveDirection.x < 0 || !right && moveDirection.x > 0)
                {
                    right = !right;
                    Vector3 kichThuoc = transform.localScale;
                    kichThuoc.x = kichThuoc.x * -1;
                    transform.localScale = kichThuoc;
                    animator.SetTrigger("isTele");
                }
            
        }   
        else
        {
                animator.SetBool("isRun", false);
        }     
    } 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTakeDamage)
        {
            if (other.gameObject.CompareTag("Shuriken"))//nếu chạm shuriken thì mất máu
            {
                health -= 20;
      
                animator.SetTrigger("isHurt");
                Destroy(other.gameObject);//shuriken biến mất
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }

            if (other.gameObject.CompareTag("Attack1"))
            {
                health -= 30;             
                animator.SetTrigger("isHurt");
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("Attack2"))
            {
                health -= 40;
               
                animator.SetTrigger("isHurt");
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("Attack3"))
            {
                health -= 50;
               
                animator.SetTrigger("isHurt");
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }
            if (other.gameObject.CompareTag("SpecialAttack"))
            {

                health -= 100;
              
                animator.SetTrigger("isHurt");
                stopAttack = false;
            }
            else
            {
                stopAttack = true;
            }

            if (health <= 0)
            {
                Destroy(gameObject, 1.3f);
                tilemap.gameObject.SetActive(false);
                
                animator.SetTrigger("isDeath");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
       
       
    }
}
