using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint; // Kéo thả spawnPoint trong Inspector, hoặc tìm bằng code

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi script bị disable
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    
            GameObject sp = GameObject.FindWithTag("SpawnPoint");
            if (sp != null)
            {
                transform.position = sp.transform.position;
            }
    
    }
}
