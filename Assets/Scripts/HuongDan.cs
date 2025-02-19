using UnityEngine;

public class HuongDan : MonoBehaviour
{
    public GameObject huongDan;
    private bool isPaused = false;

    void Start()
    {
        huongDan.SetActive(false);
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Menu();
        }
        else
        {
            ResumeGame();
        }
    }

    public void Menu(){
       huongDan.SetActive(true);
       Time.timeScale=0f;
    }
    public void ResumeGame()
    {
        huongDan.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
    }
}
