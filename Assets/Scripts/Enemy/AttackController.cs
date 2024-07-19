using UnityEngine;
using static BulletController;

public class AttackController : MonoBehaviour
{
    public int attackDamage = 10;
    private int bulletCount = 3;
    private PlayerHealthBar playerHealthBar;

    void Start()
    {
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        if (playerHealthBar != null)
        {
            playerHealthBar = playerHealthBar.GetComponent<PlayerHealthBar>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealthBar.takeDamage(attackDamage);
        }

    }

}
