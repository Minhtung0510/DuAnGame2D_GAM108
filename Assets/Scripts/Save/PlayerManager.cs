using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
{
    // Cập nhật health từ PlayerHealth trước khi lưu
    PlayerHealth playerHealth = GameObject.FindFirstObjectByType<PlayerHealth>();
if (playerHealth != null)
{
    health = playerHealth.currentHealth;
}


    PlayerPrefs.SetFloat("coins", coins);
    PlayerPrefs.SetFloat("health", health);
    PlayerPrefs.SetFloat("posX", position.x);
    PlayerPrefs.SetFloat("posY", position.y);
    PlayerPrefs.SetFloat("posZ", position.z);
    PlayerPrefs.Save();

    Debug.Log($"Game Saved! Health: {health}");
}


    public void LoadGame()
{
    if (PlayerPrefs.HasKey("health"))
    {
        // Load dữ liệu từ PlayerPrefs
        coins = PlayerPrefs.GetFloat("coins");
        health = PlayerPrefs.GetFloat("health"); // Lấy health một lần duy nhất
        position = new Vector3(
            PlayerPrefs.GetFloat("posX"),
            PlayerPrefs.GetFloat("posY"),
            PlayerPrefs.GetFloat("posZ")
        );

        // Cập nhật vị trí nhân vật
        transform.position = position; 
        Debug.Log($"Game Loaded! Health: {health}");

        // Cập nhật lại health cho PlayerHealth
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>(); 
        if (playerHealth != null)
        {
            playerHealth.currentHealth = health; // Áp dụng giá trị health vừa load
            playerHealth.UpdateHealthUI();
            Debug.Log($"PlayerHealth Updated! New Health: {playerHealth.currentHealth}");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy PlayerHealth!");
        }
    }
    else
    {
        Debug.LogWarning("Không tìm thấy dữ liệu Health!");
    }
}



}
