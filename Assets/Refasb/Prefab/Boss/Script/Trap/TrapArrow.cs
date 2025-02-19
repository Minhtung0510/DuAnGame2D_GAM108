using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    public GameObject projectile;
    public Transform SpawnLocation;
    public Quaternion SpawnRotation;
    public float spawnTime = 0.5f;
    private float timeSinceSpawned = 0f;
    private Animator an;
    private void Start()
    {
      an  = GetComponent<Animator>();
    }
    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            an.SetTrigger("Shot");
        }
    }
    private void shoot()
    {
        Instantiate(projectile, SpawnLocation.position, SpawnRotation);
    }
}
