using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxSpotlights;
    [SerializeField] private int currentTotalSpotlights;
    [SerializeField] private Transform spotlight;
    [SerializeField] private int points;
    [SerializeField] private int timer;
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private GameObject canvas;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
        mainCamera = Camera.main;
    }
    private void Awake()
    {
        //Add LevelSystem in scene to save difficulty of different levels and read into here (can decide max spotlights, etc.)
        maxSpotlights = levelSystem.Difficulty + 1;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.Find("TotalPoints").GetComponent<TextMeshProUGUI>().text = "Value Left: " + points;
        canvas.transform.Find("NumSpotlights").GetComponent<TextMeshProUGUI>().text = "Spotlight X" + (maxSpotlights-currentTotalSpotlights);
    }
    public void removeScore(int value)
    {
        points -= value;      
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if(rayHit.collider.tag == "Piece" && currentTotalSpotlights < maxSpotlights && !rayHit.collider.transform.Find("Spotlight(Clone)") && rayHit.collider.tag != "Treasure" && !levelSystem.caught)
        {
            if (!rayHit.collider.transform.Find("LowValueTreasure") && !rayHit.collider.transform.Find("MediumValueTreasure") && !rayHit.collider.transform.Find("HighValueTreasure")) {
                Instantiate(spotlight, rayHit.collider.transform);
                rayHit.collider.transform.GetComponent<Piece>().lightsOn();
                currentTotalSpotlights++;
            }
           
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        Debug.Log(rayHit.collider.gameObject.name);
        if(rayHit.collider.tag == "Spotlight" && !levelSystem.caught)
        {
            if (levelSystem.begin) {rayHit.collider.transform.parent.GetComponent<Piece>().lightsOff(); }          
            Destroy(rayHit.collider.gameObject);
            currentTotalSpotlights--;
        }
    }
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
}
