using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Trap Settings")]
    public float speed;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float tocdoxoay; 
    void Start()
    {
        transform.position = waypoints[currentWaypointIndex].position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, tocdoxoay);
    }
}
