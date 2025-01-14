using UnityEngine;
using TMPro;

public class PlayerCoin : MonoBehaviour
{
    [Header("Coin Settings")]
    private float currentCoin;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI coinText;

    void Start()
    {

        //LoadCoins();
        UpdateCoinText();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AddCoin(1f);
            Destroy(other.gameObject);
            SaveCoins();

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
        UpdateCoinText();
    }


    // Lấy số coin hiện tại
    public float GetCurrentCoin()
    {
        return currentCoin;
    }
     public void SaveCoins()
    {
        PlayerPrefs.SetFloat("coin", currentCoin);
        PlayerPrefs.Save();

    }
    // Tải số coin đã lưu
    public void LoadCoins()
    {
        currentCoin = PlayerPrefs.GetFloat("coin", currentCoin);

    }
 
}