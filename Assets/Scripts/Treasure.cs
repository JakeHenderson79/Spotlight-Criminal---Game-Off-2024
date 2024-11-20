using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{ //Priority has 3 different level based on how valuable the items are
    /* Value for priority 1 - 100 - 200
     * Value for priority 2 - 200 - 400
     * Value for priority 3 - 400 - 800
     */
    public int value;
    public int priority;
    public bool hasBeenStolen;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Robber")
        {
            hasBeenStolen = true;
            //gameObject.SetActive(false);
            Destroy(gameObject);
            player.removeScore(value);
        }
    }
}
