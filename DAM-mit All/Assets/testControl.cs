using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControl : MonoBehaviour
{
    int speed = 5;
    public Rigidbody rb;
    private Vector3 moveVel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        moveVel = movement.normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVel * Time.fixedDeltaTime);
        
    }

    
}