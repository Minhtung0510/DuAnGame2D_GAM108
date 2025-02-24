using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public AudioSource pauseSound;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pauseSound.gameObject.SetActive(false);
    }
    public void Home()
    {
        SceneManager.LoadScene("MENU");
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pauseSound.gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
