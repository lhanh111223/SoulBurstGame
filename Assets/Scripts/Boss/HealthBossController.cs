using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBossController : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI txtHealth;

    public int maxHealth = 100;
    public int currentHealth;
    private Canvas bossCanvas;
    public GameObject floatingHealthPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        setHealth(currentHealth, maxHealth);
        bossCanvas = GetComponentInChildren<Canvas>();
    }
  

    public void setHealth(int currentHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        txtHealth.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        if(floatingHealthPrefab != null)
        {
            ShowFloatingText(damage);
        }
        txtHealth.text = currentHealth.ToString() + " / " + maxHealth.ToString();

    }

    private void ShowFloatingText(int damage)
    {
        GameObject text = Instantiate(floatingHealthPrefab, bossCanvas.transform.position, Quaternion.identity, bossCanvas.transform);
        if(transform.localScale.x < 0 && bossCanvas.transform.localScale.x > 0)
        {
            
            bossCanvas.transform.localScale = new Vector3(bossCanvas.transform.localScale.x * -1,
                bossCanvas.transform.localScale.y,
                bossCanvas.transform.localScale.z);
            Debug.Log(bossCanvas.transform.localScale.x);
        }
        if (transform.localScale.x > 0 && bossCanvas.transform.localScale.x < 0)
        {

            bossCanvas.transform.localScale = new Vector3(bossCanvas.transform.localScale.x * -1,
                bossCanvas.transform.localScale.y,
                bossCanvas.transform.localScale.z);
            Debug.Log(bossCanvas.transform.localScale.x);
        }
        text.GetComponent<TextMesh>().text = damage.ToString();
    }
}
