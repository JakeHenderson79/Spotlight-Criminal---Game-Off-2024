using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameSystem : MonoBehaviour
{
    public bool[] hasUnlocked = new bool[9];
    // Start is called before the first frame update
    void Start()
    {
        hasUnlocked[0] = true;
    }
    public string Right(string str, int Length)
    {
        Debug.Log(str.Length + " " + Length);
        return str.Substring(str.Length - Length);
    }
    public void unlockLevel(string levelID)
    {
        string ID = Right(levelID, 1);
        int id = Int32.Parse(ID);
        hasUnlocked[id - 1] = true;
    }
    public bool hasUnlockedLevel(string levelID)
    {
        string ID = Right(levelID, 1);
        Debug.Log(ID);
        int id = Int32.Parse(ID);
        Debug.Log(id);
        return hasUnlocked[id - 1];
    }
}
