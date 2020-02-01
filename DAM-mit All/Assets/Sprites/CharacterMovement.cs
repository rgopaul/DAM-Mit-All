using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 2.5f;
    private Vector2 direction;
    private Vector3 pos;


    // Start is called before the first frame update
    void Start() {
        direction = Vector2.up;
        pos = transform.position;
    }

    // Update is called once per frame

    void FixedUpdate () {
        if(Input.GetKey(KeyCode.W) && transform.position == pos) { // Up
            pos += Vector3.up;
        }
        if(Input.GetKey(KeyCode.A) && transform.position == pos) { // Left
            pos += Vector3.left;
        }
        if(Input.GetKey(KeyCode.S) && transform.position == pos) { // Down
            pos += Vector3.down;
        }
        if(Input.GetKey(KeyCode.D) && transform.position == pos) { // Right
            pos += Vector3.right;
        }
         transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }

    public void Move() {
        GetInput();
        // WASD movement
        transform.Translate(direction*speed*Time.deltaTime);
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
        }

    }
}
