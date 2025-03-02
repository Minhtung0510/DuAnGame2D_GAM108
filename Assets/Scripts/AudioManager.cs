using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic; 
    public Slider volumeSlider;        

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // G�n gi� tr? �m l??ng ban ??u
        if (backgroundMusic != null)
        {
            backgroundMusic.Play(); 
            backgroundMusic.volume = PlayerPrefs.GetFloat("Volume", 1f); 
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = backgroundMusic.volume; 
            volumeSlider.onValueChanged.AddListener(SetVolume); 
        }
    }

    // H�m ?i?u ch?nh �m l??ng
    public void SetVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume; 
            PlayerPrefs.SetFloat("Volume", volume); 
        }
    }
}
