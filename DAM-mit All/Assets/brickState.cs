using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickState : MonoBehaviour
{
    private float timer;
    private Renderer cubeRenderer;
    int timeToLeak = 5; //Time to check if block is leaking
    int timeToBreak = 5; //Time to break block after it's leaking
    int timeToReset = 5; //Time to reset the timer if both cases succeed
    
    //Depending on the brickCondition the color will change
    enum brickCondition {
        Good, // Green
        Leaking, // Blue
        Broken// Red
    }
    brickCondition BC;

    // Start is called before the first frame update
    void Start()
    {
        //Set Brick's initial state to Good
        setCondition(brickCondition.Good);

        //Grab renderer component from the Cube GameObject
        cubeRenderer = gameObject.GetComponent<Renderer>();

        //Set the initial color to green
        setBlockColor(Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        updateBrick(timer);
    }

    void updateBrick(float dt)
    {
        //Debug.Log((int)dt);

        //Check if timer is at 5 seconds and block is Good before attempting to set to leaking
        if ((int)dt == timeToLeak && BC == brickCondition.Good)
        {
            //Random Chance to set block to leaking
            if (Random.Range(0, 2) != 0)
            {
                setBlockColor(Color.blue);
                BC = brickCondition.Leaking;

                Debug.Log("LEAKING");
            }
            else
                Debug.Log("Didn't Leak");

            //Reset Timer to enable rechecking leaking if random failed
            timer = 0;
        }
        else if ((int)dt == timeToBreak && BC == brickCondition.Leaking)
        {
            BC = brickCondition.Broken;
            setBlockColor(Color.red);

            Debug.Log("BROKEN");

            //Reset timer after block breaks
            timer = 0;
        }

        if (dt >= timeToReset)
            timer = 0;
    }

    //Color swapper (Probably going to be replaced with Texture Swap)
    void setBlockColor(Color color)
    {
        cubeRenderer.material.SetColor("_Color", color);
    }

    void setCondition(brickCondition state)
    {
        BC = state;
    }

    void repairBlock()
    {
        //Reset to Good Block
        if (BC != brickCondition.Good)
        {
            setBlockColor(Color.green);
            setCondition(brickCondition.Good);
            //Reset Timer so it Doesn't Have a Chance to Change Back Too Soon
            timer = 0;
        }
    }
}
