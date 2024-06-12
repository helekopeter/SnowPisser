using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSystem : MonoBehaviour
{
    //Reference to the BlockSystem script
    private BlockSystem BlockSys;

    //Variables to hold data for current block
    private int currentBlockID=0;
    private Block currentBlock;

    //Variables for the block template
    private GameObject blockTemplate;
    private SpriteRenderer currentRender;

    //Bool to controll building/piss
    private bool DickOut=false;
    private bool BuildBlocked=false;

    //Float to adjust the size of blocks when placed
    [SerializeField]
    private float BlockSizeMode;

    //Layer mask to controll raycasting
    [SerializeField]
    private LayerMask PissBlock;

    [SerializeField]
    private LayerMask AllBlocksLayer;

    //Referece to the player object
    private GameObject PlayerObject;

    [SerializeField]
    private float MaxPissDistance;

    private void Awake()
    {
        //Store referece to block system script
        BlockSys=GetComponent<BlockSystem>();

        //Find player and store reference
        PlayerObject=GameObject.Find("Player");
    }

private void Update()
{
    //If pissKey is pressed toggle pissmode
    if(Input.GetKeyDown("e"))
    {
        //Flip bool
        DickOut=!DickOut;

        //If we have a current template, destroy it(?----)
        if(blockTemplate !=null)
        {
            Destroy(blockTemplate);
        }
        //If we dont have a current block type set(?-----)
        if(currentBlock==null)
        {
            //Ensure AllBlocks array is ready
            if(BlockSys.AllBlocks[currentBlockID] !=null)
            {
                //Get a newcurrentBlock using the ID variable
                currentBlock=BlockSys.AllBlocks[currentBlockID];
            }
        }

            //Create a new object forr blockTemplate
            blockTemplate=new GameObject("CurrentBlockTemplate");
            //Add and store reference to a SpriteRenderer on the template object
            currentRender=blockTemplate.AddComponent<SpriteRenderer>();
            //Set the sprite to template object to match current block type
            currentRender.sprite=currentBlock.BlockSprite;
        }

    if(blockTemplate !=null)
    {
        float newPosX=Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / BlockSizeMode)*BlockSizeMode;
        float newPosY=Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / BlockSizeMode)*BlockSizeMode;
        blockTemplate.transform.position=new Vector2(newPosX, newPosY);

        RaycastHit2D rayHit;

        
        rayHit= Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), Mathf.Infinity, LayerMask.GetMask("Ground"));


            if (rayHit.collider !=null)
        {
            BuildBlocked=true;
        }
        else
        {
            BuildBlocked=false;
        }

        if(Vector2.Distance(PlayerObject.transform.position, blockTemplate.transform.position)>MaxPissDistance)
        {
            BuildBlocked=true;
        }

        if(BuildBlocked)
        {
            currentRender.color=new Color(1f,0f,0f,1f);
        }
        else
        {
            currentRender.color=new Color(1f,1f,1f,1f);
        }

        if(Input.GetMouseButtonDown(0)&&BuildBlocked==false&&DickOut)
        {
            GameObject newBlock=new GameObject(currentBlock.BlockName);
            newBlock.transform.position=blockTemplate.transform.position;
            SpriteRenderer newReand=newBlock.AddComponent<SpriteRenderer>();
            newReand.sprite=currentBlock.BlockSprite;
            BoxCollider2D Colision = newBlock.AddComponent<BoxCollider2D>();
            Colision.size = new Vector2(0.99f,0.99f);
            newBlock.layer=3;
            newReand.color = new Color(0.9395934f, 0.9622642f, 0.4039694f, 1.0f);
            }

        if (Input.GetMouseButtonDown(0) &&!DickOut)
        {
            Debug.Log("Destroy!");

            var destroyHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()),Mathf.Infinity,LayerMask.GetMask("Ground"));

           if(destroyHit.collider!=null)
           {
                    if(destroyHit.collider.gameObject.tag != "Solid")
                    {
                        Debug.Log("Destroy! " + destroyHit.collider.gameObject.name);
                        Destroy(destroyHit.collider.gameObject);
                    }
           }
           else
           Debug.Log("Destroy nothing!");
        }       
    }
    
}

}
