using System;   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player1Pos;
    public Transform player2Pos;
    public Camera mainCamera;

    private float horizontalDistance = 0;
    private float verticalDistance = 0;
    float maxDistance = 0;
    private Vector3 posMidpoint = new Vector3(0,0,0);

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        posMidpoint = (player1Pos.transform.position + player2Pos.transform.position) / 2; // Finds the average position between player1 and player2
        mainCamera.transform.position = posMidpoint;

        horizontalDistance = Mathf.Abs(player1Pos.transform.position.x - player2Pos.transform.position.x); // Gets the horizontal distance between player1 and player2
        verticalDistance = Mathf.Abs(player1Pos.transform.position.y - player2Pos.transform.position.y) + 5; // Gets the horizontal distance between player1 and player2 with a slight offset

        maxDistance = Mathf.Max(horizontalDistance, verticalDistance); // Checks which distance is the largest and which one to modify

        mainCamera.orthographicSize = Mathf.Clamp(maxDistance / 2, 8, 50); // Makes sure the size stays within range

        
    }
}
