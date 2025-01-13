using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private float damage = 20f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            PlayerMove player = collision.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.TakeDamage(damage); 
            }

            Debug.Log("Player mất máu");
        }
    }
}
