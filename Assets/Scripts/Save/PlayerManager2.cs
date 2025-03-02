using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager2 : MonoBehaviour
{
    public void SaveGame()
    {
        // Lưu dữ liệu của bạn (coins, health, position, v.v.)
        // PlayerPrefs.SetFloat("coins", coins);
        // PlayerPrefs.SetFloat("health", health);
        // PlayerPrefs.SetFloat("posX", position.x);
        // PlayerPrefs.SetFloat("posY", position.y);
        // PlayerPrefs.SetFloat("posZ", position.z);

        // Lưu chỉ số Scene hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedSceneIndex", currentSceneIndex);

        PlayerPrefs.Save();
        Debug.Log("Game Saved! Scene: " + currentSceneIndex);
    }
}
