using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private void Update()
    {
        if (game.hasUnlockedLevel(levelID))
        {
            gameObject.transform.Find("Lock").gameObject.SetActive(false);
        }
    }

}
