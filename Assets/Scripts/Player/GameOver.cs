using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để load lại scene
using UnityEngine.UI; // Dùng để điều khiển UI

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel Game Over

    void Start()
    {
        // Ẩn panel khi game bắt đầu
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        // Hiển thị panel Game Over
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Dừng game
    }

    public void HideGameOverPanel()
    {
        // Ẩn panel Game Over
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục game
    }

    public void PlayAgain()
    {
        // Load lại scene hiện tại
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Thoát game
        Debug.Log("Thoát game!");
        Application.Quit();
    }
}


