using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{

   public GameObject player;
    public GameObject volume;

    public AudioManager audioManager;


    void Start()
    {
        volume.SetActive(false);
        player.SetActive(false);
    }
    public void _choi(){
        Choi();
        Time.timeScale = 1;
    }
    public void Choi() {

        if (PlayerPrefs.HasKey("SavedSceneIndex"))
        {
            int sceneIndex = PlayerPrefs.GetInt("SavedSceneIndex");
            Debug.Log("Loading scene index: " + sceneIndex);
            

            // Chuyển sang scene đã lưu
            SceneManager.LoadScene(sceneIndex);
            player.SetActive(true);
            
            

            // Sau khi Scene load, trong PlayerManager bạn có thể gọi LoadGame()
            // hoặc tự động gọi OnSceneLoaded để Load dữ liệu
        }
        else
        {
                    SceneManager.LoadScene(1);
                    player.SetActive(true);
                    
        }
        
    }
    public void ChoiLai() {
        OnResetGame();
        Time.timeScale = 1;

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
    public void OnResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ResetPlayerData();
        SceneManager.LoadScene(1);
    }
    public void ResetPlayerData()
{
    PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();

    if (playerHealth != null)
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.UpdateHealthUI();
    }

    PlayerCoin playerCoin = Object.FindAnyObjectByType<PlayerCoin>();
    if (playerCoin != null)
    {
        playerCoin.SetCoins(0);
        playerCoin.UpdateCoinText();
    }
}


}
