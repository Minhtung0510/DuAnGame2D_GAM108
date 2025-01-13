using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DoorTele : MonoBehaviour
{
    [SerializeField] private string sceneName; // Tên scene muốn chuyển đến
    [SerializeField] private float transitionDelay = 0.5f; // Độ trễ trước khi chuyển scene
    [SerializeField] private GameObject LoadingCanvar; // Canvas loading
    [SerializeField] private UnityEngine.UI.Slider slLoading; // Slider hiển thị tiến trình
    [SerializeField] private TextMeshProUGUI textLoading; // Text hiển thị phần trăm
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            LoadingCanvar.SetActive(true); // Hiển thị canvas loading
            StartCoroutine(Loading()); // Bắt đầu Coroutine loading
        }
    }

    private IEnumerator Loading()
    {
        float progress = 0f; // Tiến trình bắt đầu từ 0
        slLoading.value = 0; // Đặt giá trị slider về 0
        textLoading.text = "0%"; // Hiển thị phần trăm bắt đầu

        while (progress < 100f)
        {
            progress += 10f; // Tăng tiến trình
            slLoading.value = progress; // Cập nhật giá trị cho slider
            textLoading.text = $"{(int)progress}%"; // Hiển thị phần trăm
            yield return new WaitForSeconds(0.2f); // Đợi 0.2 giây
        }

        // Đợi thêm một chút trước khi chuyển scene
            yield return new WaitForSeconds(transitionDelay);

    // Chuyển sang scene tiếp theo
    SceneManager.LoadScene(sceneName);
    }

}
