using UnityEngine;

public class soundWater : MonoBehaviour
{

    public GameObject water;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        water.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            water.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            water.SetActive(false);
        }
    }
}
