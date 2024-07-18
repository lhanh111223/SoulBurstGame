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
    private Transform playerTransform;

    void Start()
    {
        txtCoinbuy.text = itemCost.ToString();
        DisplayItem();
    }

    void Update()
    {
        // Update logic here if needed
    }

    private void DisplayItem()
    {
        Instantiate(itemPrefab, displayPosition.position, Quaternion.identity, displayPosition);
    }

    public void TryPurchaseItem() // Changed to public
    {
        if (playerCoinBar.GetCurrentCoins() >= itemCost)
        {
            playerCoinBar.DecreaseCoin(itemCost);
            // Instantiate the item in the player's inventory or handle it as needed
            Debug.Log("Item purchased!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerTransform = null;
        }
    }
}
