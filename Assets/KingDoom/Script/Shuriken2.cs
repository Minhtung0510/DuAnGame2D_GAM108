using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken2 : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isBreak", true);
            
            Destroy(gameObject, 0.4f);
        }
    }
}
