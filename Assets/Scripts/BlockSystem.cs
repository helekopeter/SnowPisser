using System;
using UnityEngine;

public class BlockSystem: MonoBehaviour
{
  //Arrays for blocks
  [SerializeField]
  private Sprite[] Blocks;
  [SerializeField]
  private String[] BlockName;

    //Array to store blocks in Awake()
public Block[] AllBlocks;

private void Awake()
{
    //Initialise AllBlock array
    AllBlocks=new Block[Blocks.Length];

    //Temp int to store block ID
    int newBlockID=0;

    //For loops to populate main AllBlock array
    for(int i =0;i<Blocks.Length;i++)
    {
        AllBlocks[newBlockID]=new Block(newBlockID, BlockName[i], Blocks[i], false);
        Debug.Log("Snow block: AllBlocks[" + newBlockID + "] = " + BlockName[i]);
        newBlockID++;
    }

}

}

public class Block
{
    public int BlockID;
    public string BlockName;
    public Sprite BlockSprite;
    public bool IsPiss;

    public Block(int id, string myName, Sprite mySprite, bool IAmPiss)
{
    BlockID=id;
    BlockName=myName;
    BlockSprite=mySprite;
    IsPiss=IAmPiss;
}
}
