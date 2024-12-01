using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Pathfinding;
using System.IO;
using UnityEngine.SceneManagement;

public class Robber : MonoBehaviour
{
   [SerializeField] private Transform connector, previousConnector;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    [SerializeField] private Piece currentPiece;
    [SerializeField] private Piece previousPiece;
    [SerializeField] private List<Transform> currentConnectors = new List<Transform>();
    [SerializeField] private Transform connectedConnector;
    private bool foundRoute;
    public bool foundConnector;
    public LayerMask myLayerMask;
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite caughtSprite;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gameObject.transform.Find("Robber").GetComponent<SpriteRenderer>().sprite = idleSprite;
    }

    // Update is called once per frame
    void Update()
    {
        //Use this for potential pause function
        if (levelSystem.begin)
        {
            GameObject.Find("Target").transform.position = connector.position;
        }
        else
        {
            GameObject.Find("Target").transform.position = transform.position;
        }
     
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Spotlight")
        {
            if (currentPiece.transform.Find("Spotlight(Clone)") && previousPiece.transform.Find("Spotlight(Clone)"))
            {
                levelSystem.begin = false;
                levelSystem.caught = true;
                gameObject.transform.Find("Robber").GetComponent<SpriteRenderer>().sprite = caughtSprite;
                gameObject.transform.Find("Robber").GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
               Debug.Log("Caught!");
                StartCoroutine(wait());
            }
            Transform tempConnector = connector;
            Piece tempPiece = currentPiece;
            currentPiece = previousPiece;
            previousPiece = tempPiece;       
            connector = previousConnector;
            previousConnector = tempConnector;
            foundConnector = false;

        }
       else if (collision.tag == "Piece")
        {
            if (currentPiece != null) { previousPiece = currentPiece; }
            if(connector != null) {previousConnector = connector;}
            currentPiece = collision.transform.GetComponent<Piece>();
            currentConnectors = new List<Transform>();
            foreach (Transform connect in currentPiece.Connectors)
            {
                currentConnectors.Add(connect);
            }
            foundConnector = false;
           checkForTreasures();
            if (!foundConnector)
            {
                foundRoute = false;
                while (!foundRoute)
                {
                    int randNum = Random.Range(0, currentConnectors.Count);
                    connector = currentConnectors[randNum];
                    connectedConnector = connector.GetComponent<Connector>().connectedPiece;
                    //  Debug.Log(connector.GetComponent<Connector>().connectedPiece.GetInstanceID() + ", " + previousPiece.GetInstanceID());
                    if ((connector.GetComponent<Connector>().connectedPiece.transform == previousPiece.transform && previousPiece != null) || connector == previousConnector)
                    {
                        Debug.Log("Remove!");
                        currentConnectors.Remove(connector);
                        foundRoute = false;
                    }
                    else
                    {
                        Debug.Log("Move!");
                        foundRoute = true;
                    }

                }
             
            }
        }
   
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Win");
    }
    private void checkForTreasures()
    {
        Transform tempConnector = null;
        Treasure tempTreasure = null;
        Transform bestConnector = null;
        Treasure bestTreasure = new Treasure();
        bestTreasure.value = 0;
        foundConnector = false;
        foreach (Transform connect in currentConnectors)
        {
            if (connect.GetComponent<Connector>().connectedPiece.transform.Find("HighValueTreasure") && !connect.GetComponent<Connector>().connectedPiece.transform.Find("Spotlight(Clone)"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("HighValueTreasure").GetComponent<Treasure>();
            }
            else if (connect.GetComponent<Connector>().connectedPiece.transform.Find("MediumValueTreasure") && !connect.GetComponent<Connector>().connectedPiece.transform.Find("Spotlight(Clone)"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("MediumValueTreasure").GetComponent<Treasure>();
            }
            else if (connect.GetComponent<Connector>().connectedPiece.transform.Find("LowValueTreasure") && !connect.GetComponent<Connector>().connectedPiece.transform.Find("Spotlight(Clone)"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("LowValueTreasure").GetComponent<Treasure>();
            }
 

            if (tempTreasure != null && tempTreasure.value > bestTreasure.value)
            {
                bestConnector = tempConnector;
                bestTreasure = tempTreasure;         
            }
        }
        if(bestConnector != null)
        {
            if (bestConnector.parent.transform.Find("Spotlight(Clone)"))
            {
                foundConnector = false;
            }
            else
            {
                connector = bestConnector;
                foundConnector = true;
            }
           
        }
        else
        {
            foundConnector = false;
        }

    }
    public Piece CurrentPiece
    {
        get { return currentPiece; }
    }
}
