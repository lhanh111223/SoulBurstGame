using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;


    private void Awake()
    {
        gameOverMenu.SetActive(false);
    }
    private void OnEnable()
    {
        gameOverMenu.SetActive(true);
    }
    public void ExitToMenu_OnClick()
    {
        Debug.Log("Exit to menu");
        SceneManager.LoadScene("CharacterSelection");
    }

    public void ExitAppButton_OnClick()
    {
        Application.Quit();
    }
}
