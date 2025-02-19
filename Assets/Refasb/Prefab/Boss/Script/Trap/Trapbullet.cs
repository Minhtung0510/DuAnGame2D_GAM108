using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Trapbullet : MonoBehaviour
{
    [SerializeField] private Transform target;
    public GameObject bullet;
    public Transform bulletPos;   
    public float work;

    public float timer;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("HitBox").transform;

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < work)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                shoot();
            }
        }
    }
    void shoot()
    {
        Instantiate(bullet,bulletPos.position,Quaternion.identity);
    }

}
