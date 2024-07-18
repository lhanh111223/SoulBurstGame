using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI txtHp;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
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
        currentHealth -= damage;
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        txtHp.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        setHealth(currentHealth, maxHealth);
    }


}
