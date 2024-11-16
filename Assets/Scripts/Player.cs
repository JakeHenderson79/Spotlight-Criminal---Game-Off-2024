using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxSpotlights;
    [SerializeField] private int currentTotalSpotlights;
    [SerializeField] private Transform spotlight;
    [SerializeField] private int points;
    [SerializeField] private int timer;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //Add LevelSystem in scene to save difficulty of different levels and read into here (can decide max spotlights, etc.)

        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if(rayHit.collider.tag == "Piece" && currentTotalSpotlights < maxSpotlights && !rayHit.collider.transform.Find("Spotlight(Clone)"))
        {
            Instantiate(spotlight, rayHit.collider.transform);
            currentTotalSpotlights++;
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        Debug.Log(rayHit.collider.gameObject.name);
        if(rayHit.collider.tag == "Spotlight")
        {
            Destroy(rayHit.collider.gameObject);
            currentTotalSpotlights--;
        }
    }
}
