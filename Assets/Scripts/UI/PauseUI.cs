using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject PauseMenu;

    private bool isPaused = false;

    void Awake()
    {

        PauseMenu.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeButton_OnClick();
            }
            else
            {
                PauseButton_OnClick();
            }
        }
    }

    public void ResumeButton_OnClick()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void ExitButton_OnClick()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("CharacterSelection");
    }


    public void PauseButton_OnClick()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitButton_OnClick()
    {
        Application.Quit();
    }
}
