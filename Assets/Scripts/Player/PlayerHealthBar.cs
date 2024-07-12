using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI txtHp;
    private float fillAmount;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        setHealth(currentHealth, maxHealth);
    }
    void Update()
    {
        setHealth(currentHealth,maxHealth);
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
    //void Death()
    //{
    //    if(currentHealth<0)
    //    {
            
    //    }
    //}
}
