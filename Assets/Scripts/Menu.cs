using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject settings;

    public void playButton()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void settingsButton()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
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
