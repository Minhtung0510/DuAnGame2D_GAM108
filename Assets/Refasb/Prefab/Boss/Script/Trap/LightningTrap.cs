using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    public GameObject Lightning; 

    public void trap()
    {
        Lightning.SetActive(true);
    }
    public void stop()
    {
        Lightning.SetActive(false);
    }
}
