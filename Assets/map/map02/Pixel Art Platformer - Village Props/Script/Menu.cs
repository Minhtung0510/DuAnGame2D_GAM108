using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu :MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayAgain()
    {

    }
    public void Setting()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
