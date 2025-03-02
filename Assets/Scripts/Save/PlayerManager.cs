using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float coins;
    public float health;
    public Vector3 position;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public float coins;
    public float health;
    public Vector3 position;

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
        LoadGame(); // Chạy load game ngay khi khởi động
    }
}


    public void SaveGame()
    {
        // Cập nhật dữ liệu trước khi lưu
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            health = playerHealth.currentHealth;
        }

        position = transform.position; // Cập nhật vị trí trước khi lưu

        // Lưu bằng JSON để mở rộng dễ dàng
        PlayerData data = new PlayerData
        {
            coins = coins,
            health = health,
            position = position
        };

        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();

        Debug.Log($"Game Saved! Health: {health}, Position: {position}");
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedSceneIndex", currentSceneIndex);

        PlayerPrefs.Save();
        Debug.Log("Game Saved! Scene: " + currentSceneIndex);
    }

    public void LoadGame()
{
    if (PlayerPrefs.HasKey("PlayerData"))
    {
        string jsonData = PlayerPrefs.GetString("PlayerData");
        PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

        coins = data.coins;
        health = data.health;
        position = data.position;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.position = position;
        }
        else
        {
            transform.position = position;
        }

        Debug.Log($"Game Loaded! Coins: {coins}, Health: {health}, Position: {position}");

        // Cập nhật UI coins
        PlayerCoin playerCoin = FindAnyObjectByType<PlayerCoin>();
        if (playerCoin != null)
        {
            playerCoin.RefreshCoins();
        }

        // Cập nhật lại health cho PlayerHealth
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth = health;
            playerHealth.UpdateHealthUI();
        }
    }
}


// Hàm cập nhật UI coins
private void UpdateCoinUI()
{
PlayerCoin playerCoin = FindFirstObjectByType<PlayerCoin>();
if (playerCoin != null)
{
    playerCoin.RefreshCoins();
}

}

}
