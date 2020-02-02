using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControl : MonoBehaviour
{
    int speed = 2;
    public Rigidbody rb;
    private Vector3 moveVel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"), 0);

        moveVel = movement.normalized * speed;
        //Repair action for player object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("FIXIT");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVel * Time.fixedDeltaTime);

    }


}