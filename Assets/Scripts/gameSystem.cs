using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameSystem : MonoBehaviour
{
    public bool[] hasUnlocked = new bool[9];
    [SerializeField] private float volume = 1f;
    public string currentLevelID;
    // Start is called before the first frame update
    void Start()
    {
        hasUnlocked[0] = true;
    }
    private void Update()
    {
        foreach (AudioSource audio in GameObject.FindObjectsOfType(typeof(AudioSource)))
        {
            audio.volume = volume;
        }
    }
    public string Right(string str, int Length)
    {
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
        int id = Int32.Parse(ID);
        return hasUnlocked[id - 1];
    }
    public void updateVolume(Slider s)
    {
        volume = s.value;
    }
  
}
