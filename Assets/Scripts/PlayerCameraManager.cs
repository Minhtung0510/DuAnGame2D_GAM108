using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    private static PlayerCameraManager instance;

    private void Awake()
    {
        // Đảm bảo chỉ tồn tại 1 instance duy nhất
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại qua các scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
