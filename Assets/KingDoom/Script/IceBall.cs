using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
   
    bool isPlayer;
    
    Animator animator;
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {            
        if(isPlayer)
        {
            Destroy(gameObject,0.3f);
            animator.SetTrigger("isBreak");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| (collision.gameObject.CompareTag("Ground")))
        {
           isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| (collision.gameObject.CompareTag("Ground")))
        {
            isPlayer = false;
        }
    }
}
