using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform p1Pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Camera>().transform.position = p1Pos.position;
    }
}
