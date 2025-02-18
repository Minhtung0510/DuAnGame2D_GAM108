using UnityEngine;

public class OpenTele : MonoBehaviour
{
    public GameObject tele;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tele.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            tele.SetActive(true);
            Destroy(gameObject);
        }
    }
}
