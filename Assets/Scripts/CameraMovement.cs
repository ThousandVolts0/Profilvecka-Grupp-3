using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player1Pos;
    public Transform player2Pos;

    public Camera mainCamera;
    public Camera secondaryCamera;

    public GameObject panel;
    private Vector3 mainCamVelocity = Vector3.zero;
    private Vector3 secondaryCamVelocity = Vector3.zero;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, player1Pos.position, ref mainCamVelocity, 0.2f);
        secondaryCamera.transform.position = Vector3.SmoothDamp(secondaryCamera.transform.position, player2Pos.position, ref secondaryCamVelocity, 0.2f);
    }

   
}
