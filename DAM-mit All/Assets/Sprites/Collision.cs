using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    BoxCollider BC;
    brickState BS;
    public repairSound RS;

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
        if(Input.GetKeyDown(KeyCode.Space) && BS.isLeaking)
        {
            BS.repairBlock();
            RS.PlayAudio();
        }
        
    }
}
