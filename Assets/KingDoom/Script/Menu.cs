using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void Choi()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Options()
    {

    } 
   public void thoatramenu()
    {
        SceneManager.LoadScene(0);
    }    
    public void Thoat()
    {
      Application.Quit();
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level" + levelID;
        SceneManager.LoadScene(levelName);

    }

}
