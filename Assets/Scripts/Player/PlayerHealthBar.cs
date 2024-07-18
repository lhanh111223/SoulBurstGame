using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Parameter;
public class PlayerHealthBar : MonoBehaviour
{
    static GameParameterPlayerHealthBar _param = new();

    public Image fillBar;
    public TextMeshProUGUI txtHp;
    public int maxHealth = _param.MAX_HEALTH;
    public int currentHealth;

    private MovementController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<MovementController>();
        currentHealth = maxHealth;
        setHealth(currentHealth, maxHealth);
    }


    public void setHealth(int currentHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        txtHp.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }


    public void takeDamage(int damage)
    {
        if (player.GetInvincible())
            return;

        currentHealth -= damage;
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        txtHp.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    //void Death()
    //{
    //    if(currentHealth<0)
    //    {

    //    }
    //}
}
