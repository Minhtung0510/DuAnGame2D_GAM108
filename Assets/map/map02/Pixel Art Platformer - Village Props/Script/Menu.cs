using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hoho :MonoBehaviour
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
