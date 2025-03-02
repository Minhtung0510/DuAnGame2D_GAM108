using UnityEngine;

public class Sounnd : MonoBehaviour
{

    public GameObject _sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sound.SetActive(false);
        
    }
    public void ToggleSound()
{
    _sound.SetActive(!_sound.activeSelf);
}


    public void OnSound()
    {
        _sound.SetActive(true);
    }
}
