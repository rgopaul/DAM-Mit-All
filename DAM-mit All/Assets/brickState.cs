using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickState : MonoBehaviour
{
    private float timer;
    private Renderer cubeRenderer;
    public bool isEnabled = false;
    int timeToLeak = 3; //Time to check if block is leaking
    int timeToBreak = 5; //Time to break block after it's leaking
    int maxFailures = 3; //Max amount of times a leak can fail before forcing a block to leak
    int Failures = 0; //Amount of times a leak has failed to occur
    public bool isBroken = false; //If true stop checking timer
    public bool isLeaking = false;
    public Animator animator;
    public AudioClip leaking;
    public AudioClip broken;

    //Depending on the brickCondition the color will change
    public enum brickCondition
    {
        Good, // Green
        Leaking, // Blue
        Broken// Red
    }
    public brickCondition BC;

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
        if (isEnabled && !isBroken)
        {
            timer += Time.deltaTime;
            updateBrick(timer);
        }
    }

    void updateBrick(float dt)
    {
        //Check if timer is at 5 seconds and block is Good before attempting to set to leaking
        if ((int)dt == timeToLeak && BC == brickCondition.Good)
        {
            //Random Chance to set block to leaking
            if (Random.Range(0, 2) != 0)
            {
                setBlockColor(Color.blue);
                BC = brickCondition.Leaking;

                Debug.Log("LEAKING");
                isLeaking = true;
                animator.SetBool("isLeaking", true);
                GetComponent<AudioSource>().PlayOneShot(leaking);
                Failures = 0;
            }
            else if (maxFailures == Failures)
            {
                setBlockColor(Color.blue);
                BC = brickCondition.Leaking;
                isLeaking = true;
                animator.SetBool("isLeaking", true);
                GetComponent<AudioSource>().PlayOneShot(leaking);
                Debug.Log("MAX FAILURE LEAK");
            }
            else
            {
                Debug.Log("Didn't Leak");
                Failures++;
            }


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

            isLeaking = false;
            animator.SetBool("isBroken", true);
            GetComponent<AudioSource>().PlayOneShot(broken);
            //Block cannot be repaired therefore stop updating
            isBroken = true;
            BoxCollider BoC = GetComponent<BoxCollider>();
            BoC.isTrigger = false;
        }
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

    public void repairBlock()
    {
        //Reset to Good Block
        if (BC != brickCondition.Good && BC != brickCondition.Broken)
        {
            setBlockColor(Color.green);
            setCondition(brickCondition.Good);
            isLeaking = false;
            animator.SetBool("isLeaking", false);
            GetComponent<AudioSource>().Stop();
            isBroken = false; //Shouldn't happen since a broken block is inaccessable
            //Reset Timer so it Doesn't Have a Chance to Change Back Too Soon
            timer = 0;
        }
    }

    // Enables / *Disables a block from breaking
    // Used to prevent multiple blocks from activating at once
    public void toggleBrick(int blockNum)
    {
        if (!isEnabled)
        {
            isEnabled = true;

        }
        else
        {
            isEnabled = false;
            Debug.Log("Block " + blockNum + " Toggled " + isEnabled);
        }
    }
}