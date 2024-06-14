using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BuildSystem : MonoBehaviour
{
    //Reference to the BlockSystem script
    private BlockSystem BlockSys;

    private PissManeger PissManeger;

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

    public int SnowInPiss = 2;
    private int SnowCollected;

    private GameObject Hand;
    private bool HandGrabbing = false;
    private GameObject GrabbingThis;
    private GameObject Mouth;
    private LineRenderer HandLine;
    private LineRenderer HandOutline;
    private Transform HandParent;
    public GameObject LastBlockPlaced;

    private void Awake()
    {
        //Store referece to block system script
        BlockSys =GetComponent<BlockSystem>();
        PissManeger=GetComponent<PissManeger>();
        Hand = GameObject.Find("Hand");
        Mouth = GameObject.Find("Mouth");
        HandLine = GameObject.Find("HandLine").GetComponent<LineRenderer>();
        HandOutline = GameObject.Find("HandOutline").GetComponent<LineRenderer>();
        HandLine.enabled = false;
        HandOutline.enabled = false;
        //Find player and store reference
        PlayerObject =GameObject.Find("Player");
        HandParent = Hand.transform.parent;
    }

    private void Update()
{
    if (HandGrabbing)
        {
            Hand.transform.position = Vector2.Lerp(Hand.transform.position, GrabbingThis.transform.position, Time.deltaTime*8);
            HandLine.SetPosition(0, HandParent.position+new Vector3(0,0.3f,0));
            HandLine.SetPosition(1, Hand.transform.position);
            HandOutline.SetPosition(0, HandParent.position + new Vector3(0, 0.3f, 0));
            HandOutline.SetPosition(1, Hand.transform.position);
        }

        //If pissKey is pressed toggle pissmode
        if (Input.GetKeyDown("e"))
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
            currentRender.color=new Color(1f,0f,0f,0f);
        }
        else
        {
            currentRender.color=new Color(1f,1f,1f,0f);
        }

        if(Input.GetMouseButtonDown(0)&&BuildBlocked==false&&DickOut)
        {
                //PlaceBlock();
            }   
    }
        if (Input.GetMouseButtonDown(0) && !DickOut)
        {
            Debug.Log("Destroy!");

            var destroyHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), Mathf.Infinity, LayerMask.GetMask("Ground"));

            if (destroyHit.collider != null)
            {
                if (destroyHit.collider.gameObject.tag != "Solid")
                {
                    if (!HandGrabbing)
                    {
                        Debug.Log("Destroy! " + destroyHit.collider.gameObject.name);
                        StartCoroutine(GrabBlock(destroyHit.collider.gameObject));
                        SnowCollected++;
                        if (SnowCollected >= SnowInPiss)
                        {
                            SnowCollected = 0;
                            if (PissManeger.Piss < PissManeger.MaxPiss)
                                PissManeger.PissCounterUpdate(PissManeger.Piss + 1);
                            else
                                PissManeger.Piss=PissManeger.MaxPiss/2;
                        }
                    }
                }
            }
            else
                Debug.Log("Destroy nothing!");
        }

    }
    public void PlaceBlock()
    {
        GameObject newBlock = new GameObject(currentBlock.BlockName);
        newBlock.transform.position = blockTemplate.transform.position;
        SpriteRenderer newReand = newBlock.AddComponent<SpriteRenderer>();
        newReand.sprite = currentBlock.BlockSprite;
        BoxCollider2D Colision = newBlock.AddComponent<BoxCollider2D>();
        Colision.size = new Vector2(0.99f, 0.99f);
        newBlock.layer = 3;
        newReand.color = new Color(0.9395934f, 0.9622642f, 0.4039694f, 1.0f);
        LastBlockPlaced = newBlock;
    }
    public void PlaceBlockHere(Vector3 Pos)
    {
        GameObject newBlock = new GameObject(currentBlock.BlockName);
        newBlock.transform.position = new Vector3(Mathf.Round(Pos.x), Mathf.Round(Pos.y), Mathf.Round(Pos.z));
        SpriteRenderer newReand = newBlock.AddComponent<SpriteRenderer>();
        newReand.sprite = currentBlock.BlockSprite;
        BoxCollider2D Colision = newBlock.AddComponent<BoxCollider2D>();
        Colision.size = new Vector2(0.99f, 0.99f);
        newBlock.layer = 3;
        newReand.color = new Color(0.9395934f, 0.9622642f, 0.4039694f, 1.0f);
        LastBlockPlaced = newBlock;
    }

    IEnumerator GrabBlock(GameObject Block)
    {
        Hand.transform.parent = null;
        GrabbingThis = Block;
        HandGrabbing = true;
        HandLine.enabled = true;
        HandOutline.enabled = true;
        Vector2 towardsBlock = GrabbingThis.transform.position - Hand.transform.position;
        Hand.transform.rotation = Quaternion.LookRotation(towardsBlock);
        Hand.transform.localRotation = Hand.transform.localRotation * Quaternion.Euler(0f,90f,-90f);
        Hand.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = GrabbingThis.GetComponent<SpriteRenderer>().color;
        yield return new WaitForSeconds(0.25f);
        Destroy(GrabbingThis);
        Hand.transform.GetChild(0).gameObject.SetActive(true);
        GrabbingThis = Mouth;
        towardsBlock = GrabbingThis.transform.position - Hand.transform.position;
        Hand.transform.rotation = Quaternion.LookRotation(towardsBlock);
        Hand.transform.localRotation = Hand.transform.localRotation * Quaternion.Euler(0f, 90f, -90f);
        yield return new WaitForSeconds(0.25f);
        Hand.transform.rotation = Quaternion.Euler(0f,0f,0f);
        HandLine.enabled = false;
        HandOutline.enabled = false;
        Hand.transform.GetChild(0).gameObject.SetActive(false);
        Hand.transform.parent = HandParent;
        Hand.transform.localPosition = Vector3.zero;
        HandGrabbing = false;
    }
}
