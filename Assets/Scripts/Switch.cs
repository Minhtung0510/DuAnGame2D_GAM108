using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject fire; // Đối tượng lửa cần tắt
    private bool isPlayerInTrigger = false; // Biến cờ kiểm tra Player có trong trigger không
    public Animator anmt;

    void Start()
    {
        fire.SetActive(true); // Khởi tạo: bật lửa khi bắt đầu
    }

    void Update()
    {
        // Kiểm tra nếu Player trong trigger và nhấn phím Q
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Q))
        {
            fire.SetActive(false); // Tắt lửa
            anmt.SetBool("flase", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi Player vào trigger
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Đặt cờ thành true
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Khi Player rời trigger
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Đặt cờ thành false
        }
    }
}