using UnityEngine;
using TMPro;

public class PlayerCoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private float currentCoin;

    void Start()
    {
        currentCoin = PlayerManager.instance.coins;
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AddCoin(1);
            Destroy(other.gameObject);
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
    }
}
