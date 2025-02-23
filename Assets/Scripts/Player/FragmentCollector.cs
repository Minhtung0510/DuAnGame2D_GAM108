using UnityEngine;
using UnityEngine.SceneManagement;

public class FragmentCollector : MonoBehaviour
{
    private int fragmentCount = 0;
    public int requiredFragments = 3;
    public string bossSceneName = "Boss"; // Đổi tên scene boss của bạn

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fragment")) // Đặt tag "Fragment" cho mảnh vỡ
        {
            Destroy(other.gameObject); // Xóa mảnh vỡ
            fragmentCount++;

            if (fragmentCount >= requiredFragments)
            {
                SceneManager.LoadScene(bossSceneName); // Dịch chuyển đến boss scene
            }
        }
    }
}
