using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private float _moveSpeed;

    public Transform attack;
    public GameObject attackSkill;

    public Transform player;
    //tính khoản cách thấy player 
    public float detectionRange = 5f; 
    public float detectionAttack = 1f;
    public float detectionIdle = 1f;

    public float attackCooldown = 5f; // Thời gian hồi chiêu sau mỗi lần tấn công
    public float attackDuration = 2.3f; // Thời gian thực hiện đòn tấn công (tính từ lúc bắt đầu đến khi kết thúc)
    private float nextAttackTime = 0f;
    private float attackEndTime;
    private bool isAttacking = false;

    //xử lí hp
    public Slider healSlider;
    private float health;

    Animator animator;
    bool isFollow;   
    bool isRight = true;
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    void Start()
    {
        animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        health = 100;
        healSlider.maxValue = health;
    }
  
    void Update()
    {
        if (health > 0)
        {
            FollowPlayer();
            moveRun();
            Attack();
        }
    }

    private void moveRun()
    {
        if (isFollow)
        {
            var direction = Vector3.right;
            if (isRight == false)
            {
                direction = Vector3.left;
            }
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
            //flip
            if (direction.x != 0)
            {
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            animator.SetBool("isRun", true);
            Curent();
        }
        
    }
    private void Curent()
    {
        var currentPosition = transform.localPosition;
        if (currentPosition.x > rightBoundary)
        {
            isRight = false;
        }
        else if (currentPosition.x < leftBoundary)
        {
            isRight = true;    
        }        
    }
    private void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionAttack && Time.time >= nextAttackTime && !isAttacking)
        {
            nextAttackTime = Time.time + attackCooldown;
            attackEndTime = Time.time + attackDuration;
            isAttacking = true;
            animator.SetTrigger("isAttack");
        }
                         
        if (isAttacking && Time.time >= attackEndTime)
        {
            var oneSkill = Instantiate(attackSkill, attack.position, Quaternion.identity);
            Destroy(oneSkill, 0.5f);
            isAttacking = false;
            
        }
       
    }
    private void FollowPlayer()
    {
        
        float direction = Vector3.Distance(transform.position,player.position);
        if (direction < detectionRange)
        {
           
            if (direction > detectionIdle)
            {
                isFollow = false;
                animator.SetBool("isRun", true);             
                Vector2 moveDirection = (player.position - transform.position).normalized;
                transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);
                //flip
                if (isRight && moveDirection.x < 0 || !isRight && moveDirection.x > 0)
                {
                    isRight = !isRight;
                    Vector3 distance = transform.localScale;
                    distance.x = distance.x * -1;
                    transform.localScale = distance;
                }
            }
            else
            {
                animator.SetBool("isRun", false);
                
            }
        }
        else
        {
            isFollow = true;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health > 0)
        {
            //chạm shuriken
            if (other.gameObject.CompareTag("Shuriken"))
            {
                health -= 5;
                healSlider.value = health;
                animator.SetTrigger("isTakeHit");
            }
            if (other.gameObject.CompareTag("Attack1"))
            {
                health -= 10;
                healSlider.value = health;
                animator.SetTrigger("isTakeHit");

            }


            if (other.gameObject.CompareTag("Attack2"))
            {
                health -= 20;
                healSlider.value = health;
                animator.SetTrigger("isTakeHit");
            }

            if (other.gameObject.CompareTag("Attack3"))
            {
                health -= 35;
                healSlider.value = health;
                animator.SetTrigger("isTakeHit");

            }


            if (other.gameObject.CompareTag("SpecialAttack"))
            {

                health -= 50;
                healSlider.value = health;
                animator.SetTrigger("isTakeHit");
            }

            if (health <= 0)
            {
                Destroy(gameObject, 3f);
                animator.SetTrigger("isDeath");
                BoxCollider2D.isTrigger = true;
                Rigidbody2D.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    
}

