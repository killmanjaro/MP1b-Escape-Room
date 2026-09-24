using UnityEngine;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    // Singleton reference for easy global access
    public static CoinUIManager Instance;

    [SerializeField] private TMP_Text coinText;
    private int currentCoins = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else;
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoin(int amount = 1)
    {
        currentCoins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + currentCoins;
        }
    }
}
