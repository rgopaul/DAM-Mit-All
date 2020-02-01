using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickState : MonoBehaviour
{
    private float timer;
    private Renderer cubeRenderer;
    Random rand = new Random();

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
        BC = brickCondition.Good;

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
        //Debug.Log(dt);
        if(BC == brickCondition.Good){setBlockColor(Color.green);}

        if (dt == 5)
        {
            //if (rand.Next(0, 2) != 0)
                BC = brickCondition.Leaking;
        }

        if (BC == brickCondition.Leaking){setBlockColor(Color.blue);}

        if (Input.GetKeyDown("2"))
            BC = brickCondition.Broken;

        if (BC == brickCondition.Broken){setBlockColor(Color.red);}
    }

    //Color swapper (Probably going to be replaced with Texture Swap)
    void setBlockColor(Color color)
    {
        cubeRenderer.material.SetColor("_Color", color);
    }
}
