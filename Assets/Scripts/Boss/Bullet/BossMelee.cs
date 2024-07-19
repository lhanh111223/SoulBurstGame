using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
    public PlayerHealthBar PlayerHealthBar;
    public int meleeDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar = PlayerHealthBar.GetComponent<PlayerHealthBar>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                PlayerHealthBar.takeDamage(meleeDamage);
            }
        }
    }
}
