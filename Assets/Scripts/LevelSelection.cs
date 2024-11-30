using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private string levelID;
    [SerializeField] private gameSystem game;
    private void Start()
    {
        game = GameObject.Find("GameSystem").GetComponent<gameSystem>();
        gameObject.transform.Find("LevelName").GetComponent<TextMeshProUGUI>().text = levelName;
    }
    public void levelOpener()
    {
        if (game.hasUnlockedLevel(levelID))
        {
        SceneManager.LoadScene(levelID);
        }

    }
   
}
