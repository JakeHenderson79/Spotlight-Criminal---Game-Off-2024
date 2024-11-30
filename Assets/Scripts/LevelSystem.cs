using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    /*Will include:
     * Timer for robber
     * Difficulty level
     * Generate locations for treasures/robber
     * Keep record score
     * win coniditions for the robber
     */
    [SerializeField] private GameObject[] mapPieces;
    [SerializeField] private GameObject[] treasures;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject robber;
    [SerializeField] private GameObject Timer;
    [SerializeField] private int numOfTreasures;
    [SerializeField] private int difficulty;
    [SerializeField] private GameObject breakingIn;
    [SerializeField] private Transform robberNear;
    private float timer;
    private float fiveIncrementTimer =5;
    [SerializeField] private int initalTimer;
    public bool begin;
    public bool caught;
    // Start is called before the first frame update
    void Start()
    {
        timer = initalTimer; //It will decrease in increments of five
        Timer = canvas.transform.Find("Timer").gameObject;
        mapPieces = GameObject.FindGameObjectsWithTag("Piece");
        Debug.Log(mapPieces.Length);
        treasures = GameObject.FindGameObjectsWithTag("Treasure");
        numOfTreasures = treasures.Length;
        int count = 0;
        int rng;
        while (count != numOfTreasures)
        {
            rng = UnityEngine.Random.Range(0, mapPieces.Length);
            if (!mapPieces[rng].transform.Find("LowValueTreasure") && !mapPieces[rng].transform.Find("MediumValueTreasure") && !mapPieces[rng].transform.Find("HighValueTreasure"))
            {
                if (mapPieces[rng].GetComponent<Piece>().NumOfConnectors > 2)
                {
                   
                        treasures[count].transform.parent = mapPieces[rng].transform;
                        treasures[count++].transform.localPosition = new Vector3(0, 0, -0.03f);                                                      
                }
            }
        }
       
        initalValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (begin)
        {
            fiveIncrementTimer -=  Time.deltaTime;
            if( fiveIncrementTimer <= 0)
            {
                timer -= 5;
                fiveIncrementTimer = 5;
            }
            if(timer <= 0)
            {
                SceneManager.LoadScene("Win");
            }
            if(player.GetComponent<Player>().Points <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
        }
        canvas.transform.Find("TimerTxt").GetComponent<TextMeshProUGUI>().text = "Time Left: " + timer;
    }
    private void initalValue()
    {
        int points = 0;
        for (int i = 0; i < numOfTreasures; i++)
        {
            points += treasures[i].GetComponent<Treasure>().value;
        }
        player.GetComponent<Player>().Points = points;
    }
    public void beginBtn()
    {
        begin = true;
        robber.GetComponent<CircleCollider2D>().enabled = true;
        robber.transform.GetChild(0).gameObject.SetActive(true);
        bool robberPlaced = false;
        int rng;
        while (!robberPlaced)
        {
            rng = UnityEngine.Random.Range(0, mapPieces.Length);
            if (!mapPieces[rng].transform.Find("LowValueTreasure") && !mapPieces[rng].transform.Find("MediumValueTreasure") && !mapPieces[rng].transform.Find("HighValueTreasure"))
            {
                robber.transform.position = mapPieces[rng].transform.position;
                robberPlaced = true;
            }
        }
        canvas.transform.Find("Button").gameObject.SetActive(false);
        foreach (GameObject piece in mapPieces )
        {
            if (!piece.transform.Find("Spotlight(Clone)"))
            {
                piece.GetComponent<Piece>().lightsOff();
            }
            
        }
        breakingIn.SetActive(true);
        Timer.SetActive(true);
        Debug.Log(robber.GetComponent<Robber>().CurrentPiece.transform.position);
        Instantiate(robberNear,robber.GetComponent<Robber>().CurrentPiece.transform.position, transform.rotation);
        
    }
    public int Difficulty
    {
        get { return difficulty; }
    }
    public int NumOfTreasures
    {
        get { return numOfTreasures;}
    }
    public GameObject[] Treasures
    {
        get { return treasures; }
    }
    
}
