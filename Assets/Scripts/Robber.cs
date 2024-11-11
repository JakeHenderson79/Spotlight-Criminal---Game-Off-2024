using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(connector  != null)
        {
            Vector3 direction = connector.position - transform.position;
            direction.Normalize();
            movement = direction;
            moveRobber(movement);
        }
    
    }
    private void moveRobber(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
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
           // currentConnectors = currentPiece.Connectors;            
            foundRoute = false;
          
            while(!foundRoute)
            {
                int randNum = Random.Range(0, currentConnectors.Count);
                connector = currentConnectors[randNum];
                connectedConnector = connector.GetComponent<Connector>().connectedPiece;
                Debug.Log(connector.GetComponent<Connector>().connectedPiece.GetInstanceID() + ", " + previousPiece.GetInstanceID());
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
