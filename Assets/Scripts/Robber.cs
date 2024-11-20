using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Pathfinding;
using System.IO;

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
    private Vector3 startCast;
    private Vector3 upCast ,upCast2;
    private Vector3 leftCast, leftCast2;
    private Vector3 rightCast, rightCast2;
    private Vector3 downCast, downCast2;
    public LayerMask myLayerMask;
   [SerializeField] private bool upWall, downWall, leftWall, rightWall;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Target").transform.position = connector.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Spotlight")
        {
            Transform tempConnector = connector;
            Piece tempPiece = currentPiece;
            currentPiece = previousPiece;
            previousPiece = tempPiece;       
            connector = previousConnector;
            previousConnector = tempConnector;
            Debug.Log("H");
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
            if (connect.GetComponent<Connector>().connectedPiece.transform.Find("HighValueTreasure"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("HighValueTreasure").GetComponent<Treasure>();
            }
            else if (connect.GetComponent<Connector>().connectedPiece.transform.Find("MediumValueTreasure"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("MediumValueTreasure").GetComponent<Treasure>();
            }
            else if (connect.GetComponent<Connector>().connectedPiece.transform.Find("LowValueTreasure"))
            {
                tempConnector = connect;
                tempTreasure = connect.GetComponent<Connector>().connectedPiece.transform.Find("LowValueTreasure").GetComponent<Treasure>();
            }
 
            Debug.Log(tempTreasure);
            //if(bestTreasure == null)
            //{
            //    bestConnector = tempConnector;
            //    bestTreasure = tempTreasure;

            //}else
            if (tempTreasure != null && tempTreasure.value > bestTreasure.value)
            {
                bestConnector = tempConnector;
                bestTreasure = tempTreasure;         
            }
        }
        if(bestConnector != null)
        {
            connector = bestConnector;
            foundConnector = true;
        }
        else
        {
            foundConnector = false;
        }

    }
}
