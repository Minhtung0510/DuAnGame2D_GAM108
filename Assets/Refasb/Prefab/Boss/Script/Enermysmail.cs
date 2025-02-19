using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enermysmail : MonoBehaviour
{
    public float movespeed;
    public GameObject[] wayPoints;
     int nextwaypoint = 1;
    float disToPoint;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        disToPoint = Vector2.Distance(transform.position, wayPoints[nextwaypoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextwaypoint].transform.position, movespeed * Time.deltaTime);

        if (disToPoint < 0.002f)
        {
            taketurn();
        }
    }
    void taketurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextwaypoint].transform.eulerAngles.z; 
        transform.eulerAngles = currRot;
        ChooseNextWaypon();
    }
    void ChooseNextWaypon()
    {
        nextwaypoint++;
        if(nextwaypoint == wayPoints.Length)
        {
            nextwaypoint = 0;
        }
    }
}
