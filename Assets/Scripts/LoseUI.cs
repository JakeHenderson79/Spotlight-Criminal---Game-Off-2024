using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    private gameSystem system;
    public void menuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void nextLevelButton()
    {
        system = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        string ID = system.Right(system.currentLevelID, 1);
        int id = Int32.Parse(ID);
        SceneManager.LoadScene("Level " + (id));
    }
}
