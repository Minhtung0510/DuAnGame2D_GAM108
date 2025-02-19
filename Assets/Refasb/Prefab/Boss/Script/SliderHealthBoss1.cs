using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHealthBoss1 : MonoBehaviour
{
    public float health = 500f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Shuriken"))
        {
            health -= 20;

        }
        if (other.gameObject.CompareTag("Attack1"))
        {
            health -= 30;
        }
        if (other.gameObject.CompareTag("Attack2"))
        {
            health -= 40;

        }
        if (other.gameObject.CompareTag("Attack3"))
        {
            health -= 50;

        }

        if (other.gameObject.CompareTag("SpecialAttack"))
        {
            health -= 100;
        }
        //animator
        if (health <= 400)
        {
            animator.SetBool("is1St", true);
        }
        if (health <= 300)
        {
            animator.SetBool("is2St", true);
        }
        if (health <= 200)
        {
            animator.SetBool("is3St", true);
        }
        if (health <= 100)
        {
            animator.SetBool("is4St", true);
        }
        if (health <= 0)
        {
            animator.SetBool("is5St", true);
        }


    }
}
