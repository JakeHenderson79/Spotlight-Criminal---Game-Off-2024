using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public void playButton()
    {
        mainMenu.SetActive(false);
        GameObject.Find("LevelSelection").SetActive(true);
    }
    public void tutorialButton()
    {
        mainMenu.SetActive(false);
        GameObject.Find("Tutorial").SetActive(true);
    }
    public void settingsButton()
    {
        mainMenu.SetActive(false);
        GameObject.Find("Settings").SetActive(true);
    }
    public void exitButton()
    {
        Application.Quit();
    }
    public void backButton()
    {
        GameObject.Find("LevelSelection").SetActive(false);
        mainMenu.SetActive(true);

    }
}
