using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class IceBoss4 : MonoBehaviour
{
    public float detectionRange = 10f;  // Phạm vi phát hiện người chơi
    private float StopRange = 2.5f;
    public float detectionAttack = 5f;
    public Tilemap tilemap;
    public float attackCooldown = 2f; // Thời gian hồi chiêu sau mỗi lần tấn công
    public float attackDuration = 2.3f; // Thời gian thực hiện đòn tấn công (tính từ lúc bắt đầu đến khi kết thúc)
    private float nextAttackTime = 0f;
    private float attackEndTime;
    private bool isAttacking = false;
   
   
    //skill 1
    public GameObject attackSkill;
    public Transform attack;

    public GameObject iceBall;
    public Transform iceTransform;

    public Slider healSlider;
    public float health;
    private bool right;
    private bool isTakeDamage;

   
    
    public Transform player;
    Animator animator;
    Vector2 moveSpeed;

    
    private bool attackActive = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 600;
        healSlider.maxValue = health;
       
    }
    void Update()
    {
        if (health > 0)
        {
            FollowPlayer();
            Attack();
            Flip();
            isTakeDamage = true;
        }
        else
        {
            isTakeDamage = false;
        }
        
    }
    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            if (distance > StopRange)
            {
               moveSpeed = (player.position - transform.position).normalized;
                transform.Translate(moveSpeed * 1f * Time.deltaTime);
                animator.SetBool("isWalk", true);

            }
            else
            {
                animator.SetBool("isWalk", false);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
    void Attack()
    {
        
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
           
            if (distanceToPlayer <= detectionAttack && Time.time >= nextAttackTime && !isAttacking)
            {
                nextAttackTime = Time.time + attackCooldown;
                int attackType = Random.Range(0, 2);
                if(attackType == 0 )
                {
                    attackEndTime = Time.time + attackDuration;                   
                    isAttacking = true;
                    animator.SetTrigger("isAttack");
                    attackActive = false;
                }
                
                if (attackType == 1 ) 
                {
                    var oneBullet = Instantiate(iceBall, iceTransform.position, Quaternion.identity);
                    Destroy(oneBullet,2f);
                    var velocity = new Vector2(-20f, 0);
                    if (right == false)
                    {
                        velocity.x = -20;
                    }
                    oneBullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(!right ? 20 : -20, 0);                    
                }                            
            }
            if (isAttacking && Time.time >= attackEndTime)
            {
                var oneSkill = Instantiate(attackSkill, attack.position, Quaternion.identity);
                Destroy(oneSkill, 0.5f);
                isAttacking = false;
                attackActive = true;
            }
            
        
    }
    void Flip()
    {
        if (right && moveSpeed.x < 0|| !right && moveSpeed.x>0)
        {
            right = !right;
            Vector3 direction = transform.localScale;
            direction.x = direction.x * -1;
            transform.localScale = direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTakeDamage)
        {
           
            //chạm shuriken
            if (other.gameObject.CompareTag("Shuriken"))
            {
                if (health > 0)
                {
                    health -= 5;
                    healSlider.value = health;
                    if (attackActive)
                    {
                        animator.SetTrigger("isTakeHit");
                    }
                    if (right)
                    {
                        transform.Translate(Vector2.left * 6f * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector2.right * 6f * Time.deltaTime);
                    }
                    Destroy(other.gameObject);//shuriken biến mất

                }
            }
           
            if (other.gameObject.CompareTag("Attack1"))
            {
                if (health > 0)
                {
                    health -= 10;
                    healSlider.value = health;
                    if(attackActive)
                    {
                        animator.SetTrigger("isHurt");
                        
                    }
                    
                }
                      
            }
           
            if (other.gameObject.CompareTag("Attack2"))
            {
                if (health > 0)
                {
                    health -= 20;
                    healSlider.value = health;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }

               
                
            }
            
            if (other.gameObject.CompareTag("Attack3"))
            {
                if (health > 0)
                {
                    health -= 35;
                    healSlider.value = health;
                    if (attackActive)
                    {
                        animator.SetTrigger("isHurt");
                    }
                }

                
               
            }
           
            if (other.gameObject.CompareTag("SpecialAttack"))
            {
                if (health > 0)
                {
                    health -= 40;
                    healSlider.value = health;
                    if (attackActive)
                    {
                        animator.SetTrigger("isTakeHit");
                    }
                }
               
               
            }
            
            if (health <= 0)
            {
                Destroy(gameObject, 5f);
                animator.SetBool("isDeath", true);
                tilemap.gameObject.SetActive(false);
            }
        }
        
             
    }
}