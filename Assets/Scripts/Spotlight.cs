using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private Transform robberNear;
    [SerializeField] private AudioSource leftAudio;
    [SerializeField] private AudioSource rightAudio;
    private Transform warning;
    private void Start()
    {
        leftAudio = GameObject.Find("LeftAudioAlert").GetComponent<AudioSource>();
        rightAudio = GameObject.Find("RightAudioAlert").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Robber")
        {
            if (collision.transform.position.x < 0)
            {
                
                leftAudio.Play();

            }
            else
            {
                rightAudio.enabled = true;
                rightAudio.Play();
            }
            Vector3 direction = collision.transform.position - transform.position;
            direction.Normalize();
            warning = Instantiate(robberNear,transform.position  + direction * 1.25f,transform.rotation);
            warning.position = new Vector3(warning.position.x+0.2f,warning.position.y,-2f);
            warning.transform.eulerAngles = new Vector3(0,0,0);
           
           
        }
    }
 
}
