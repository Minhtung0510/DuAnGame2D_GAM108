using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Trapground : MonoBehaviour
{
    public float timetoshot;
    public float speep;
    public float x , y,z;
    public GameObject pipePrefab; 
    public List<GameObject> pipes;
    public float timeToCreatePipes;
    int numberOfPipes = 0;
    void Start()
    {
        timeToCreatePipes = 2f;
    }
    void Update()
    {
        timeToCreatePipes -= Time.deltaTime;
        if (timeToCreatePipes <= 0)
        {
            CreateRandomPipes();
            timeToCreatePipes = timetoshot;
        }
    }
    private void CreateRandomPipes()
    {

        
        var randomY = Random.Range(y, z);
        var pipe = Instantiate(pipePrefab,new Vector3(x, randomY , 0), Quaternion.identity);
        Destroy(pipe,2);
        MovePipes(pipe);
    }
    private void MovePipes(GameObject pipe)
    {
        pipe.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * speep;
    }
}
