using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    BoxCollider BC;
    brickState BS;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Enter");
    }

    void OnTriggerExit(Collider other)
    {
        //other.gameObject.broadcast("Repair");
        BC = other.gameObject.GetComponent<BoxCollider>();
        // BC.isTrigger = false;
        Debug.Log("Box Exit");

    }

    void OnTriggerStay(Collider other)
    {
        BS = other.gameObject.GetComponent<brickState>();
        if(Input.GetKeyDown(KeyCode.Space))
        BS.repairBlock();
    }
}
