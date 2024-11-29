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
    private void Start()
    {
        gameObject.transform.Find("LevelName").GetComponent<TextMeshProUGUI>().text = levelName;
    }
    public void levelOpener()
    {
        SceneManager.LoadScene(levelID);
    }
   
}
