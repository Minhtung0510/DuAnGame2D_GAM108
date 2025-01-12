using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    public AudioManager audioManager;
    public void Choi() {
        SceneManager.LoadScene(1);
    }
    public void ChoiLai() {

    }
    public void Setting() {


    }
   public void Leave() {
        Application.Quit();
    }
}
