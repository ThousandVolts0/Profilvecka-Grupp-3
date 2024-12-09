using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Player1Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Range (0, 1000)] public float speed = 10f;
    private Rigidbody2D Rigidbody;
    private bool isGrounded;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveInputH = Input.GetAxis("KeysOnlyH"); // Custom input manager definied under project settings.
        float moveInputV = Input.GetAxis("KeysOnlyV"); // Custom input manager definied under project settings.

        Rigidbody.velocity = new Vector2(moveInputH * speed, moveInputV * speed);
    }

}
