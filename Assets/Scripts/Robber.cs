using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Pathfinding;
using System.IO;

public class Robber : MonoBehaviour
{
   [SerializeField] private Transform connector;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;
    [SerializeField] private Piece currentPiece;
    [SerializeField] private Piece previousPiece;
    [SerializeField] private List<Transform> currentConnectors = new List<Transform>();
    [SerializeField] private Transform connectedConnector;
    private bool foundRoute;
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

            if(collision.tag == "Piece")
        {
            previousPiece = currentPiece;
            currentPiece = collision.transform.GetComponent<Piece>();
            currentConnectors = new List<Transform>();
            foreach(Transform connect in currentPiece.Connectors)
            {
                currentConnectors.Add(connect);
            }        
            foundRoute = false;
          
            while(!foundRoute)
            {
                int randNum = Random.Range(0, currentConnectors.Count);
                connector = currentConnectors[randNum];
                connectedConnector = connector.GetComponent<Connector>().connectedPiece;
              //  Debug.Log(connector.GetComponent<Connector>().connectedPiece.GetInstanceID() + ", " + previousPiece.GetInstanceID());
                if(connector.GetComponent<Connector>().connectedPiece.transform == previousPiece.transform && previousPiece != null)
                {
                    Debug.Log("Remove!");
                    currentConnectors.Remove(connector);
                    foundRoute = false;
                }
                else
                {
                    Debug.Log("Move!");
                    foundRoute = true; //Temp line of code, will be replace when spotlights added
                }
                
            }
        }
    }
}
