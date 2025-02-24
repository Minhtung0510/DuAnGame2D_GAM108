using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float moveSpeed =1f;
    public float timetolive = 5f; 
    public float timeSinceSpawned =1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime; 
        timeSinceSpawned += Time.deltaTime;
        if(timeSinceSpawned > timetolive)
        {
            Destroy(gameObject);
        }
    }
}
