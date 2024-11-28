using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string levelName;
    public void levelOpener()
    {
        SceneManager.LoadScene(levelName);
    }
   
}
