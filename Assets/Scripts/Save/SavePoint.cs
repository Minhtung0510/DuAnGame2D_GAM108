using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject _save; // UI hoặc hiệu ứng khi đến SavePoint
    private Transform playerTransform;

    void Start()
    {
        if (_save != null)
            _save.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_save != null)
                _save.SetActive(true);
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_save != null)
                _save.SetActive(false);
            playerTransform = null; // Xóa thông tin người chơi khi rời khỏi SavePoint
        }
    }

    private void Update()
    {
        if (playerTransform != null && Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        if (PlayerManager.instance != null && playerTransform != null)
        {
            
            PlayerManager.instance.position = playerTransform.position;
            PlayerManager.instance.SaveGame();
            Debug.Log("Game Saved at SavePoint!");
        }
        else
        {
            Debug.LogWarning("Không thể lưu game, PlayerManager chưa khởi tạo hoặc không có người chơi ở SavePoint.");
        }
    }
}
