using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] public Transform connectedPiece;
    [SerializeField] public Transform connectedConnector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Connector")
        {
            connectedConnector = collision.gameObject.GetComponent<Transform>();
            connectedPiece = collision.transform.parent;
        }
    }
}
