using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    public GameObject volume;

    public AudioManager audioManager;

    void Start()
    {
        volume.SetActive(false);
    }
    public void Choi() {
        SceneManager.LoadScene(1);
    }
    public void ChoiLai() {

    }
    public void Setting() {
        _volume();

    }
   public void Leave() {
        Application.Quit();
    }
    public void _volume()
    {
        volume.SetActive(true);
    }
}
