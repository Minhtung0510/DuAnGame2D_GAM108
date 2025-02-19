using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public int scene;
    void Start()
    {

    }


    void Update()
    {

    }
    public void next()
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
