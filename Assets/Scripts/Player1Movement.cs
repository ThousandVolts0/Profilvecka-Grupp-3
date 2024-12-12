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

    float moveInputH;
    float moveInputV;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (moveInputH < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (moveInputH > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveInputH = Input.GetAxis("KeysOnlyH"); // Custom input manager definied under project settings.
        moveInputV = Input.GetAxis("KeysOnlyV"); // Custom input manager definied under project settings.

        Rigidbody.velocity = new Vector2(moveInputH * speed, moveInputV * speed);
    }

}
