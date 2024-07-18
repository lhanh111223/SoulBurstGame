using UnityEngine;

public class HealthManaManage : MonoBehaviour
{
    public static HealthManaManage Instance { get; private set; }
    public PlayerHealthBar playerHealthBar;
    public PlayerManaBar playerManaBar;
    public PlayerCoinBar playerCoinBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
