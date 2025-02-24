using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Godzilla : MonoBehaviour
{
    public float detectionRange = 10f;  // Phạm vi phát hiện người chơi
    public float stopRange = 2f;
    public float AppearRange = 15f;
    public float detectionAttack = 2f;
    public float attack3Range = 30f;
    public float stopAttack3Range = 5f;

    private float timeCoolDown;
    private float attackEndTime;
    public float attackDuration;
    private float summonEndTime;

    private bool attacking;
    private bool isAppear;
    private bool isAttack;
    private float timeAppear;
    public float timeDirection = 2f;

    private bool isAttacking;
    private bool isAttacking3;
    private bool isAttacking4;
    private bool right;
    private bool attackActive = true;

    public Transform TransformAttack;
    public GameObject Attack1;
    public Transform TransformAttack3;
    public GameObject attack3;
    public GameObject hp;
    public GameObject mana;
    //boom
    public Transform boomTransform;
    public GameObject Boom;
    public Transform boomTransform2;
    public GameObject Boom2;
    public Transform boomTransform3;
    public GameObject Boom3;

    public Transform player;
    Animator animator;
    Vector2 moveDiretion;
    //xử lý hp
    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;
    public float health = 1000;
    void Start()
    {
        animator = GetComponent<Animator>();
        sliderHealth.maxValue = health;
        textHealth.text = health.ToString() + "/" + "1000";
    }  
    void Update()
    {
        if (health > 0)
        {
            FollowPlayer();
            Appear();
            Attack();
            Attack3();
            Flip();
        }
        if(health < 0)
        {
            health = 0;
            textHealth.text = health.ToString() + "/" + "1000";
        }
    }
    private void Appear()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance < AppearRange && !attacking)
        {
            timeAppear = Time.time  +timeDirection;
            animator.SetTrigger("isAppear");           
            attacking = true;
        }if(attacking && Time.time >= timeAppear)
        {
            attacking = false;
            isAppear = true;
        }
        
    }
   
    private void FollowPlayer()
    {
        if (isAppear)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < detectionRange)
            {
                if (distance > stopRange)
                {
                     moveDiretion = (player.position - transform.position).normalized;
                    transform.Translate(moveDiretion * 1 * Time.deltaTime);
                    animator.SetBool("isRun", true);

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

    private void Attack()
    {
        
        if(isAttack && Time.time >= timeCoolDown +2f && !isAttacking)
        {
            timeCoolDown = Time.time;
            int typeAttack = Random.Range(0, 2);
            if(typeAttack == 0)
            {
                attackEndTime = Time.time + attackDuration;
                animator.SetTrigger("isAttack1");
                isAttacking = true;
                attackActive = false;
            }
            if (typeAttack == 1)
            {
                attackEndTime = Time.time + attackDuration;
                animator.SetTrigger("isAttack2");
                isAttacking = true;
                attackActive = false;
            }
           
        }
        if (isAttacking && Time.time >= attackEndTime)
        {
            var oneSkill = Instantiate(Attack1, TransformAttack.position, Quaternion.identity);
            Destroy(oneSkill, 0.1f);
            isAttacking = false;
            attackActive = true;

        }
        
    }
    private void Attack3()
    {
        if (isAppear)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < attack3Range && Time.time >= timeCoolDown + 3 && !isAttacking3)
            {
                timeCoolDown = Time.time;
                if (distance > stopAttack3Range)
                {
                    int typeAttack = Random.Range(0, 2);
                    if (typeAttack == 0)
                    {
                        attackEndTime = Time.time + attackDuration;
                        animator.SetTrigger("isAttack3");
                        isAttacking3 = true;
                    }
                    if (typeAttack == 1)
                    {
                        summonEndTime = Time.time + attackDuration;
                        isAttacking4 = true;
                    }
                }
               
            }
            if (isAttacking3 && Time.time >= attackEndTime)
            {
                var oneSkill = Instantiate(attack3, TransformAttack3.position, Quaternion.identity);
                Destroy(oneSkill, 1.5f);
                var velocity = new Vector2(-20f, 0);
                if (right == false)
                {
                    velocity.x = -20;
                }
                oneSkill.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(!right ? -20 : 20, 0);
                isAttacking3 = false;
                // Xoay viên đạn theo hướng di chuyển của boss
                oneSkill.transform.localScale = new Vector3(right ? -1 : 1, -1, 1);
                
            }
            if (isAttacking4 && Time.time >= summonEndTime)
            {
                float randomX = Random.Range(-3f, 3f);
                Vector3 skillPosition = player.position + new Vector3(randomX, 8f, 0);

                GameObject[] items = { hp, mana };
                GameObject item = items[Random.Range(0, items.Length)];
                var skillTance = Instantiate(item, skillPosition, Quaternion.identity);
                Destroy(skillTance, 3f);
                isAttacking4 = false;
            }
        }
    }
    void Flip()
    {
        if (right && moveDiretion.x < 0 || !right && moveDiretion.x > 0)
        {
            right = !right;
            Vector3 direction = transform.localScale;
            direction.x = direction.x * -1;
            transform.localScale = direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health > 0 && isAppear)
        {
            if (attackActive)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    isAttack = true;
                }
                if (other.gameObject.CompareTag("Shuriken"))
                {
                    health -= 10;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isHit");
                }

                if (other.gameObject.CompareTag("Attack1"))
                {
                    health -= 15;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isTakeHit");
                }

                if (other.gameObject.CompareTag("Attack2"))
                {
                    health -= 20;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isTakeHit");
                }

                if (other.gameObject.CompareTag("Attack3"))
                {
                    health -= 30;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isTakeHit");
                }

                if (other.gameObject.CompareTag("SpecialAttack"))
                {
                    health -= 100;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isHit");
                }
                if (other.gameObject.CompareTag("FireBall1"))
                {
                    health -= 20;
                    sliderHealth.value = health;
                    textHealth.text = health.ToString() + "/" + "1000";
                    animator.SetTrigger("isHit");
                }
            }
            if (health <= 0)
            {               
               Destroy(gameObject,5f);
               animator.SetTrigger("isDeath");
                //boom
               var oneBoom = Instantiate(Boom, boomTransform.position, Quaternion.identity);
                var oneBoom2 = Instantiate(Boom2, boomTransform2.position, Quaternion.identity);
                var oneBoom3 = Instantiate(Boom3, boomTransform3.position, Quaternion.identity);
               
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttack = false;
           
        }
    }
}
