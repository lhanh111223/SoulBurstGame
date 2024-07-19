using TMPro;
using UnityEngine;

public class PlayerCoinBar : MonoBehaviour
{
    public TextMeshProUGUI txtCoin;
    private int currentCoins;

    void Start()
    {
        currentCoins = 100;
        UpdateCoinText();
    }

    public void IncreaseCoin(int amount)
    {
        currentCoins += amount;
        UpdateCoinText();
    }

    public void DecreaseCoin(int amount)
    {
        currentCoins -= amount;
        UpdateCoinText();
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    private void UpdateCoinText()
    {
        txtCoin.text = currentCoins.ToString();
    }
}
