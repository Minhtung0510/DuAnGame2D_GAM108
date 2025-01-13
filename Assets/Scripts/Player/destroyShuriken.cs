using UnityEngine;

public class destroyShuriken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    
}
