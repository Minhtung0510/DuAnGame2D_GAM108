using UnityEngine;

public class Dame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other2D)
    {
        if(other2D.CompareTag("skill"))
        {
            Debug.Log("Dame");
        }
    }
    
}
