using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    private float upAmount = .1f;
    private float speed = 10;

    public Vector3 dnPos;
    public Vector3 upPos;
    public Vector3 currPos;
    public bool cardClicked = false;
    public Vector3 originalCardSize;
    private Vector2 mousePosition;

    private InventoryManager inventoryManager;
    private DeckManager deckManager;
    private InventoryGridGenerator inventoryGridGenerator;
    public LayerMask clickInventory;
    public string itemType;

    public GameObject inspectionCardPrefab;
    private GameObject inspectionCardInstantiation;


    public bool itemEquipped;

    private GameObject[] enemies;
    private GameObject player;

    public bool[,] slotsToEquip = new bool[3, 3];
    public bool[] slotsToEquipStrings = new bool[9];
    public List<GameObject> equippedSlots = new List<GameObject>();

    public int carryingWeight;

    // Start is called before the first frame update
    void Start()
    {
        itemEquipped = false;
        dnPos = transform.position;
        upPos = transform.position + new Vector3(0, upAmount, -1);
        currPos = dnPos;

        originalCardSize = transform.localScale;

        inventoryManager = GameObject.Find("Scripts").GetComponent<InventoryManager>();
        deckManager = GameObject.Find("Scripts").GetComponent<DeckManager>();
        inventoryGridGenerator = GameObject.Find("Scripts").GetComponent<InventoryGridGenerator>();

        /*slotsToEquip[0, 0] = slotsToEquipStrings[0];
        slotsToEquip[1, 0] = slotsToEquipStrings[1];
        slotsToEquip[2, 0] = slotsToEquipStrings[2];
        slotsToEquip[0, 1] = slotsToEquipStrings[3];
        slotsToEquip[1, 1] = slotsToEquipStrings[4];
        slotsToEquip[2, 1] = slotsToEquipStrings[5];
        slotsToEquip[0, 2] = slotsToEquipStrings[6];
        slotsToEquip[1, 2] = slotsToEquipStrings[7];
        slotsToEquip[2, 2] = slotsToEquipStrings[8];*/

    }


    // Update is called once per frame
    void Update()
    {
        if (!cardClicked)
        {
            if (!itemEquipped)
            {
                //if not clicked, moves to inventory
                transform.position = Vector3.MoveTowards(transform.position, currPos, speed * Time.deltaTime);
                if (transform.position == dnPos)
                {
                    speed = 10;
                }

                //if inspection card is spawned, card follows mouse
                if (inspectionCardInstantiation != null)
                {
                    //inspectionCardInstantiation.transform.position = new Vector3(mousePosition.x, mousePosition.y, -2);
                }
            }
        }
        if (cardClicked)
        {
            // if clicked, follows mouse
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, -5);


        }
    }

    private void OnMouseEnter()
    {
        currPos = upPos;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inspectionCardInstantiation = Instantiate(inspectionCardPrefab, new Vector3(mousePosition.x + 1.5f, mousePosition.y + 1.5f, -5), Quaternion.identity);


    }

    private void OnMouseExit()
    {
        currPos = dnPos;

        Destroy(inspectionCardInstantiation);
        inspectionCardInstantiation = null;
    }

    private void OnMouseDown()
    {
        //var cardFrame = this.gameObject.transform.GetChild(0).gameObject;
        //var cardImage = cardFrame.transform.GetChild(2).gameObject;
        if (!cardClicked)
        {


            //when clicked, follows the mouse
            if (!itemEquipped)
            {
                cardClicked = true;
                inventoryManager.carryingCard = gameObject;


                //makes card smaller and transparent while following the mouse
                //var tempVec = new Vector3((float)originalCardSize.x / 2, (float)originalCardSize.y / 2, (float)originalCardSize.z);
                //transform.localScale = tempVec;
                //transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).gameObject.SetActive(false);
                //cardFrame.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
                //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
                
            }
            //if its currently equipped and youre not carrying another card, will follow the mouse
            else if (itemEquipped && inventoryManager.carryingCard == null)
            {
                cardClicked = true;
                inventoryManager.carryingCard = gameObject;


                //makes card smaller and transparent while following the mouse
                //var tempVec = new Vector3((float)originalCardSize.x / 2, (float)originalCardSize.y / 2, (float)originalCardSize.z);
                //transform.localScale = tempVec;
                //transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).gameObject.SetActive(false);
                //cardFrame.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
                //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            }

            //if its currently equipped but you're carrying another card, sends this card back to deck and equips the card youre carrying
            else if (itemEquipped && inventoryManager.carryingCard != null)
            {
                
                //removes item thats equipped, reseting its speed and transparency, returns it to deck
                //inventoryManager.unsetInventory(gameObject);
                cardClicked = false;
                speed = 60;
                //cardFrame.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                //equips the card you're currently carrying, sets it to not being clicked, resets its speed and transparency, removes it from card carrying aspect once equipped.
                checkIfSetInInventory(inventoryManager.carryingCard);
                inventoryManager.carryingCard.GetComponent<CardManager>().cardClicked = false;
                inventoryManager.carryingCard.GetComponent<CardManager>().itemEquipped = true;
                inventoryManager.carryingCard.GetComponent<CardManager>().speed = 60;
                //inventoryManager.carryingCard.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                //inventoryManager.carryingCard.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                inventoryManager.carryingCard = null;
            }

        }
        else if (cardClicked)
        {
            cardClicked = false;
            speed = 60;
            currPos = dnPos;
            inventoryManager.carryingCard = null;

            //returns card to original size and opacity
            transform.localScale = originalCardSize;
            //cardFrame.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            //if clicked on a inventory slot though, it equips it to the slot
            checkIfSetInInventory(gameObject);
        }
        deckManager.updateHandPosition();

    }

    private void checkIfSetInInventory(GameObject card)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, clickInventory);
        
        //clicked an inventory slot
        if (hit)
        {
            inventoryManager.InvManagement(card, hit.collider.gameObject);
        }

        //didnt click an inventory slot
        if (!hit)
        {
            if (itemEquipped)
            {
                inventoryManager.unsetInventory(gameObject);
            }
        }
    }
}
