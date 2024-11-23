using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int numOfTreasures;
    [SerializeField] private int difficulty;
    public bool begin;
    // Start is called before the first frame update
    void Start()
    {
        mapPieces = GameObject.FindGameObjectsWithTag("Piece");
        Debug.Log(mapPieces.Length);
        treasures = GameObject.FindGameObjectsWithTag("Treasure");
        numOfTreasures = treasures.Length;
        int count = 0;
        int rng;
        while (count != numOfTreasures)
        {
            rng = Random.Range(0, mapPieces.Length);
            if (!mapPieces[rng].transform.Find("LowValueTreasure") && !mapPieces[rng].transform.Find("MediumValueTreasure") && !mapPieces[rng].transform.Find("HighValueTreasure"))
            {
                if (mapPieces[rng].GetComponent<Piece>().NumOfConnectors > 2)
                {
                    treasures[count].transform.parent = mapPieces[rng].transform;
                    treasures[count++].transform.localPosition = new Vector3(0,0,-0.03f);
                    
                }
            }
        }
        initalValue();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        canvas.transform.Find("Button").gameObject.SetActive(false);
        foreach (GameObject piece in mapPieces )
        {
            if (!piece.transform.Find("Spotlight(Clone)"))
            {
                piece.GetComponent<Piece>().lightsOff();
            }
            
        }
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
