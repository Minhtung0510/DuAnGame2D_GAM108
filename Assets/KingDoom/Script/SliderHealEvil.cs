using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHealthEvil : MonoBehaviour
{
    public float health = 700f;
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
            health -= 5;

        }
        if (other.gameObject.CompareTag("Attack1"))
        {
            health -= 10;
        }
        if (other.gameObject.CompareTag("Attack2"))
        {
            health -= 20;

        }
        if (other.gameObject.CompareTag("Attack3"))
        {
            health -= 30;

        }

        if (other.gameObject.CompareTag("SpecialAttack"))
        {
            health -= 100;
        }
        //animator
        if (health <= 560)
        {
            animator.SetBool("is1St", true);
        }
        if (health <= 420)
        {
            animator.SetBool("is2St", true);
        }
        if (health <= 280)
        {
            animator.SetBool("is3St", true);
        }
        if (health <= 140)
        {
            animator.SetBool("is4St", true);
        }
        if (health <= 0)
        {
            animator.SetBool("is5St", true);
        }


    }
}
