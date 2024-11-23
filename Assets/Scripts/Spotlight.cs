using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private Transform robberNear;
    private Transform warning;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Robber")
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.Normalize();
            warning = Instantiate(robberNear,transform.position  + direction * 1.25f,transform.rotation);
            warning.position = new Vector3(warning.position.x,warning.position.y,-2f);
            warning.transform.eulerAngles = new Vector3(0,0,0);
            Invoke("destroySymbol", 2f);
        }
    }
    private void destroySymbol()
    {
        Destroy(warning.gameObject);
    }
}
