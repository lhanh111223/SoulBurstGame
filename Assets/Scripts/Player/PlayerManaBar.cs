using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Parameter;

public class PlayerManaBar : MonoBehaviour
{
    static GameParameterPlayerManaBar _param = new();   

    public Image fillBar;
    public TextMeshProUGUI txtMana;
    private float fillAmount;
    public int maxMana = _param.MAX_MANA;
    private Player player;
    private bool canClick = true;

    void Start()
    {
        player = FindObjectOfType<Player>();
        SetMana(player.Mana, maxMana);
    }

    void Update()
    {
        SetMana(player.Mana, maxMana);
        
        //if (Input.GetKeyDown(KeyCode.Space) && canClick)
        //{
        //    if (player.Mana > 0)
        //    {
        //        player.DecreaseMana(50);
        //        SetMana(player.Mana, maxMana);
        //    }
        //    else
        //    {
        //        canClick = false;
        //    }
        //} 
    }
    public void SetMana(int currentMana, int maxMana)
        {
            fillBar.fillAmount = (float)currentMana / (float)maxMana;
            txtMana.text = currentMana.ToString() + " / " + maxMana.ToString();
        }
    }

