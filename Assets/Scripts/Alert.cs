using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroySymbol", 2f);
    }

    private void destroySymbol()
    {
        Destroy(gameObject);
    }
}
