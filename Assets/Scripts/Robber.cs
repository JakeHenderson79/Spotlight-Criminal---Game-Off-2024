using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        startCast = gameObject.transform.position;
        upCast = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z);
        upCast2 = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);

        downCast = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z);
        downCast2 = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);


        leftCast = upCast;
        leftCast2 = downCast;

        rightCast = upCast2;
        rightCast2 = downCast2;

      
        Debug.DrawLine(upCast, new Vector3(transform.position.x - 0.5f, transform.position.y + 1, transform.position.z), Color.yellow);
        Debug.DrawLine(upCast2, new Vector3(transform.position.x + 0.5f, transform.position.y + 1, transform.position.z), Color.yellow);
        RaycastHit2D upMovement = Physics2D.Raycast(upCast, transform.up,1f,myLayerMask);
        RaycastHit2D upMovement2 = Physics2D.Raycast(upCast2,transform.up,1f,myLayerMask);
        if(upMovement.collider != null)
        {
            Debug.Log(upMovement.collider);
            if(upMovement.collider.tag == "Wall")
            {
                upWall = true;
            }
            
        }else if (upMovement2.collider != null)
        {
            Debug.Log(upMovement.collider);
            if (upMovement2.collider.tag == "Wall")
            {
                upWall = true;
            }
        }
        else
        {
            upWall = false;
        }

        Debug.DrawLine(downCast, new Vector3(transform.position.x - 0.5f, transform.position.y - 1, transform.position.z), Color.green);
        Debug.DrawLine(downCast2, new Vector3(transform.position.x + 0.5f, transform.position.y - 1, transform.position.z), Color.green);
        RaycastHit2D downMovement = Physics2D.Raycast(downCast,-transform.up,1f,myLayerMask);
        RaycastHit2D downMovement2 = Physics2D.Raycast(downCast2, -transform.up,1f,myLayerMask);
        if (downMovement.collider != null)
        {
            if (downMovement.collider.tag == "Wall")
            {
                downWall = true;
            }
            
        }else if(downMovement2.collider != null)
        {
            if (downMovement2.collider.tag == "Wall")
            {
                downWall = true;
            }
        }
        else
        {
            downWall = false;
        }

        Debug.DrawLine(leftCast, new Vector3(transform.position.x - 1, transform.position.y + 0.5f, transform.position.z), Color.cyan);
        Debug.DrawLine(leftCast2,new Vector3(transform.position.x - 1, transform.position.y - 0.5f, transform.position.z), Color.cyan);
        RaycastHit2D leftMovement = Physics2D.Raycast(leftCast, -transform.right, 1f, myLayerMask);
        RaycastHit2D leftMovement2 = Physics2D.Raycast(leftCast2, -transform.right, 1f, myLayerMask);
        if (leftMovement.collider != null)
        {
            if (leftMovement.collider.tag == "Wall")
            {
                leftWall = true;
            }
        }else if (leftMovement2.collider != null)
        {
            if (leftMovement2.collider.tag == "Wall")
            {
                leftWall = true;
            }
        }
        else
        {
            leftWall = false;
        }

        Debug.DrawLine(rightCast, new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z), Color.magenta);
        Debug.DrawLine(rightCast2, new Vector3(transform.position.x + 1, transform.position.y - 0.5f, transform.position.z), Color.magenta);
        RaycastHit2D rightMovement = Physics2D.Raycast(rightCast, transform.right, 1f, myLayerMask);
        RaycastHit2D rightMovement2 = Physics2D.Raycast(rightCast2, transform.right, 1f, myLayerMask);
        if (rightMovement.collider != null)
        {
            Debug.Log(rightMovement.collider + ", " + rightMovement2.collider);
            if (rightMovement.collider.tag == "Wall")
            {
                rightWall = true;
            }

        }else if (rightMovement2.collider != null)
        {
            Debug.Log(rightMovement.collider + ", " + rightMovement2.collider);
            if (rightMovement2.collider.tag == "Wall")
            {
                rightWall = true;
            }
        }
        else
        {
            rightWall = false;
        }
    }
    private void FixedUpdate()
    {
        if(connector  != null)
        {
          
            Vector3 direction = connector.position - transform.position;         
            direction.Normalize();

            if (upWall && downWall)
            {
                direction.y = 0;
            }
            else if (upWall && !downWall)
            {
                direction.y -= 1;
            }
            else if (!upWall && downWall)
            {
                direction.y += 1;
            }

            if (leftWall && rightWall)
            {
                direction.x = 0;
            }
            else if (leftWall && !rightWall)
            {
                direction.x += 1;
            }
            else if (!leftWall && rightWall)
            {
                direction.x -= 1;
            }
            Debug.Log("Up " + upWall + ", Down " + downWall + ", Left " + leftWall + ", Right " + rightWall);
            movement = direction;
           // Debug.Log(movement);
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
