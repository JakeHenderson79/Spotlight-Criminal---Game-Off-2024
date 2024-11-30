using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    private gameSystem system;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void menuButton()
    {
        system = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        string ID = system.Right(system.currentLevelID, 1);
        int id = Int32.Parse(ID);
        system.unlockLevel("Level" + (id + 1));
        SceneManager.LoadScene("Menu");
    }
    public void nextLevelButton()
    {
        system = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        string ID = system.Right(system.currentLevelID, 1);
        int id = Int32.Parse(ID);
        system.unlockLevel("Level" +  (id+1));
        SceneManager.LoadScene("Level" + (id + 1));
    }
}
