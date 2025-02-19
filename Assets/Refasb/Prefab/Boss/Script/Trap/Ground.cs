using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    public float heal = 100f;
    public Tilemap Tilemap;
    public Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boom"))
        {
            heal -= 40;

        }
        if(heal <= 0)
        {
            Tilemap.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
        }
    }
}
