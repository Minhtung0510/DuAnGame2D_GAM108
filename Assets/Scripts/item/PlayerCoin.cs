using UnityEngine;
using TMPro;

public class PlayerCoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private float currentCoin;

    public AudioSource audioSource; // Thêm AudioSource vào GameObject
    public AudioClip shootSound; // Kéo file âm thanh vào đây

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentCoin = PlayerManager.instance.coins;
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Shoot();
            AddCoin(1);
            Destroy(other.gameObject);
        }
    }

    void Shoot()
    {
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); // Phát âm thanh một lần
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = currentCoin.ToString();
        }
    }

    public void AddCoin(float amount)
    {
        currentCoin += amount;
        PlayerManager.instance.coins = currentCoin;
        UpdateCoinText();
        PlayerManager.instance.SaveGame();
    }

    public void SetCoins(float amount)
{
    currentCoin = amount;
    PlayerManager.instance.coins = amount;
    UpdateCoinText();
    PlayerManager.instance.SaveGame(); // Đảm bảo lưu ngay lập tức
}

    public void RefreshCoins()
{
    currentCoin = PlayerManager.instance.coins; // Lấy giá trị mới từ PlayerManager
    UpdateCoinText();
}

}
