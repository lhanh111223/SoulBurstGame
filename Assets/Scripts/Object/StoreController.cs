using UnityEngine;
using TMPro;

public class StoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCoinbuy;
    [SerializeField] private Transform displayPosition;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int itemCost; 

    [SerializeField] private PlayerCoinBar playerCoinBar;
    [SerializeField] private float interactionRange = 2f;

    private bool isPlayerNearby = false;
    private bool canPurchase = false;
    void Start()
    {
        txtCoinbuy.text = itemCost.ToString();
        DisplayItem();
    }

    void Update()
    {
        if (isPlayerNearby && canPurchase && Input.GetKeyDown(KeyCode.Q))
        {
            TryPurchase();
        }
    }

    private void DisplayItem()
    {
        Instantiate(itemPrefab, displayPosition.position, Quaternion.identity, displayPosition);
    }

    private void TryPurchase()
    {
        if (playerCoinBar.GetCurrentCoins() >= itemCost)
        {
            playerCoinBar.DecreaseCoin(itemCost);
            txtCoinbuy.text = "0";
            canPurchase = false;
            this.itemCost = 0;
        }
        else
        {
            Debug.Log("Không đủ tiền để mua món hàng này.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            canPurchase = playerCoinBar.GetCurrentCoins() >= itemCost;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            canPurchase = false;
        }
    }
    public void setPriceWP(int value)
    {
       
    }
}
