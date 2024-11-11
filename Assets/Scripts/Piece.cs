using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
  [SerializeField]private int numOfConnectors;
    [SerializeField] private string typeOfPiece;
    [SerializeField] private List<Transform> connectors = new List<Transform>(); 
    private void Start()
    {
        for (int i = 0; i < numOfConnectors; i++)
        {           
            connectors.Add(gameObject.transform.Find("connector" + (i + 1)));
        }
    }
    public int NumOfConnectors
    {
        get { return numOfConnectors; }
    }
    public string TypeOfPiece
    {
        get { return typeOfPiece; }
    }
    public List<Transform> Connectors
    {
        get { return connectors; }
    }
}
