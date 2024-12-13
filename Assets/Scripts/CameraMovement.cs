using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

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
    private float mainTargetPosX;
    private float mainTargetPosY;
    private Vector3 mainTargetPos;
    private Vector3 secondaryTargetPos;

    private float secondaryTargetPosX;
    private float secondaryTargetPosY;

    private float cameraHalfHeight;
    private float cameraHalfWidth;

    public float offsetX = 0f;
    public float offsetY = 0f;

    public Transform borderR, borderL, borderU, borderD;

    void Start()
    {
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float borderLeftX = borderL.TransformPoint(Vector3.zero).x;
        float borderRightX = borderR.TransformPoint(Vector3.zero).x;
        float borderUpY = borderU.TransformPoint(Vector3.zero).y;
        float borderDownY = borderD.TransformPoint(Vector3.zero).y;

        if (player1Pos == null && player2Pos == null) // both are dead
        {
            return;
        }
        else if (player1Pos == null && player2Pos != null) // only player 2 is alive
        {
            secondaryTargetPosX = Mathf.Clamp(player2Pos.position.x, (borderLeftX + cameraHalfWidth) + offsetX, (borderRightX - cameraHalfWidth) - offsetX);
            secondaryTargetPosY = Mathf.Clamp(player2Pos.position.y, (borderDownY + cameraHalfHeight) + offsetY, (borderUpY - cameraHalfHeight) - offsetY);
            secondaryCamera.transform.position = new Vector3(secondaryTargetPosX, secondaryTargetPosY, secondaryCamera.transform.position.z);
            secondaryTargetPos = new Vector3(secondaryTargetPosX, secondaryTargetPosY, secondaryCamera.transform.position.z);
            secondaryCamera.transform.position = Vector3.SmoothDamp(secondaryCamera.transform.position, secondaryTargetPos, ref secondaryCamVelocity, 0.2f);
        }
        else if (player1Pos != null && player2Pos == null) // only player 1 is alive
        {
            mainTargetPosX = Mathf.Clamp(player1Pos.position.x, (borderLeftX + cameraHalfWidth) + offsetX, (borderRightX - cameraHalfWidth) - offsetX);
            mainTargetPosY = Mathf.Clamp(player1Pos.position.y, (borderDownY + cameraHalfHeight) + offsetY, (borderUpY - cameraHalfHeight) - offsetY);

            mainCamera.transform.position = new Vector3(mainTargetPosX, mainTargetPosY, mainCamera.transform.position.z);
            mainTargetPos = new Vector3(mainTargetPosX, mainTargetPosY, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, mainTargetPos, ref mainCamVelocity, 0.2f);
        }
        else // none are dead
        {
            mainTargetPosX = Mathf.Clamp(player1Pos.position.x, (borderLeftX + cameraHalfWidth) + offsetX, (borderRightX - cameraHalfWidth) - offsetX);
            mainTargetPosY = Mathf.Clamp(player1Pos.position.y, (borderDownY + cameraHalfHeight) + offsetY, (borderUpY - cameraHalfHeight) - offsetY);

            secondaryTargetPosX = Mathf.Clamp(player2Pos.position.x, (borderLeftX + cameraHalfWidth) + offsetX, (borderRightX - cameraHalfWidth) - offsetX);
            secondaryTargetPosY = Mathf.Clamp(player2Pos.position.y, (borderDownY + cameraHalfHeight) + offsetY, (borderUpY - cameraHalfHeight) - offsetY);

            mainCamera.transform.position = new Vector3(mainTargetPosX, mainTargetPosY, mainCamera.transform.position.z);
            secondaryCamera.transform.position = new Vector3(secondaryTargetPosX, secondaryTargetPosY, secondaryCamera.transform.position.z);

            mainTargetPos = new Vector3(mainTargetPosX, mainTargetPosY, mainCamera.transform.position.z);
            secondaryTargetPos = new Vector3(secondaryTargetPosX, secondaryTargetPosY, secondaryCamera.transform.position.z);

            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, mainTargetPos, ref mainCamVelocity, 0.2f);
            secondaryCamera.transform.position = Vector3.SmoothDamp(secondaryCamera.transform.position, secondaryTargetPos, ref secondaryCamVelocity, 0.2f);
        }


        

       
       
    }

   
}
