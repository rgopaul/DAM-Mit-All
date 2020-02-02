using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    private float timer;
    private float limitTimer; //The amount of time before we disable the block limit
    private int randomBlock;
    private GameObject[] blocks;
    private brickState BS;
    private int[] activeBlocks; //Index of blocks currently active
    public int maxActiveBlocks = 4; //Max amount of blocks allowed to be active
    private bool ignoreBlockLimit = false; //Ignore maxActiveBlocks Limit towards end of the Game
    public int maxFailures = 3; //Max amount of times an activate can fail before forcing a block to activate
    int Failures = 0; //Amount of times a block has failed to activate
    public int timeLimit;

    public int upperRange; //Increase this to lower probability of a block activating
    public int speed; //The rate at which a block is attempted to be activated
    private void Start()
    {
        //Set limit to amount of active blocks allowed to be active
        activeBlocks = new int[maxActiveBlocks];
        for (int i = 0; i < activeBlocks.Length; i++)
        {
            activeBlocks[i] = -1;
        }

        //Create an Array of Blocks to Control
        blocks = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks[i] = transform.GetChild(i).gameObject;
        }

        //Randomly activates the first block
        StartFirstBlock();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        ToggleBrick(timer);

        /*Repair ALL bricks debug feature (COMMENT OUT LATER)!!!!
        if(Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                blocks[i].SendMessage("repairBlock");
            }
        }*/
    }

    //Randomly activates blocks based on various conditions.
    //A lil gross to look at :c
    void ToggleBrick(float dt)
    {
        if ((int)dt == speed)
        { 
            for (int i = 0; i < transform.childCount; i++)
            {
                //Don't attempt to activate broken blocks (redundant + wastes a turn)
                if (CheckIfBroken(i))
                {
                    //Debug.Log("Skip Broken");
                    continue;
                }
                            
                //Random Chance to set block to active
                if (Random.Range(0, upperRange) == 0 && !BlockLimitReached() || Failures == maxFailures)
                {
                    if (BlockAlreadyActive(i))
                        break;
                    blocks[i].SendMessage("toggleBrick", i);
                    activeBlocks[AvailableIndex()] = i;
                    Failures = 0;
                    break;
                }
                else if(Random.Range(0, upperRange) == 0 && BlockLimitReached() && !ignoreBlockLimit || Failures == maxFailures)
                {
                    //start deactivating blocks but stay withing the block limit
                    if (BlockAlreadyActive(i))
                        break;
                    Debug.Log("Limit has been Reached. Swapping blocks.");
                    
                    int available = AvailableIndex(); //Grab available index
                    
                    if (available != -1)
                    {
                        blocks[i].SendMessage("toggleBrick", i); //Activate new block
                        blocks[activeBlocks[available]].SendMessage("toggleBrick", activeBlocks[available]); //Disable old block
                        Debug.Log("We got a new Block Activated ");
                        activeBlocks[available] = i; //Store Newly Enabled Block
                        
                    }
                    Failures = 0;
                    break;
                    
                }
                else if(ignoreBlockLimit) //Just go ham and activate everything for chaos
                {
                    BS = blocks[i].GetComponent<brickState>();
                    if(!BS.isEnabled)
                    {
                        blocks[i].SendMessage("toggleBrick", i);
                    }
                }
                else
                {
                    Failures++;
                }

            }
            limitTimer += (int)dt;
            //Debug.Log("Limit: " + limitTimer);
            timer = 0; 
        }

        if (limitTimer >= timeLimit && !ignoreBlockLimit)
        {
            ignoreBlockLimit = true;
            Debug.Log("BLOCK LIMIT HAS BEEN DISABLED WEE WOO WEE WOO");
        }
    }

    //Randomly activates the first block to be able to leak
    void StartFirstBlock()
    {
        randomBlock = Random.Range(0, transform.childCount);
        blocks[randomBlock].SendMessage("toggleBrick", randomBlock);

        //Store the index of the active block
        activeBlocks[0] = randomBlock;

    }
    
    //Checks if the maximum amount of active blocks has been reached
    bool BlockLimitReached()
    {
        for(int i = 0; i < activeBlocks.Length; i++)
        {
            if (activeBlocks[i] == -1)
                return false;
        }
        return true;
    }

    //Searches for the latest available index otherwise replace a random one
    int AvailableIndex()
    {
        for (int i = 0; i < activeBlocks.Length; i++)
        {
            if (activeBlocks[i] == -1)
                return i;
        }

        //Looks for available slot to activate a block while avoiding blocks that are leaking
        for(int i = 0; i < activeBlocks.Length; i++)
        {
           // Debug.Log("Index " +i+ " " +activeBlocks[i]);
            BS = blocks[activeBlocks[i]].GetComponent<brickState>();
            Debug.Log(BS.isLeaking);
            if (BS.isBroken)          
            {
                Debug.Log("Index " + i + " is Broken and has been replaced");
                return i;
            }
            if(!BS.isLeaking)
            {
                Debug.Log("Index " + i + " is not Leaking or Broken and has been replaced");
                return i;
            }
            
        }

        return -1; //All are leaking don't replace

    }

    //Checks if a block we are trying to activate is already active
    bool BlockAlreadyActive(int index)
    {
        for (int i = 0; i < activeBlocks.Length; i++)
        {
            if (activeBlocks[i] == index)
                return true;
        }

        return false;
    }

    //Returns true is a child block is broken
    bool CheckIfBroken(int index)
    {
        BS = blocks[index].GetComponent<brickState>();
        return BS.isBroken;
    }
}
