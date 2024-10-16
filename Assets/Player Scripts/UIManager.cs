using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject settings;

    private void Start()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
        settings.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlayEnter()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }
    
    public void BackToMenu()
    {
        howToPlay.SetActive(false);
        credits.SetActive(false);
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
