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
    public Vector3 spawnPointOriginalPosition;
    

    private InventoryManager inventoryManager;
    private DeckManager deckManager;
    private InventoryGridGenerator inventoryGridGenerator;
    private TurnManager turnManager;
    private SwitchScreen switchScreen;
    private MapControls mapControls;
    public LayerMask clickInventory;
    public LayerMask clickDeckSlot;
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

    public bool isSticky;
    public int StickyAmount;

    public bool abilityOnEquip;
    private bool abilityOnEquipActivated;
    public bool abilityTriggered;

    public GameObject worldCanvas;
    public GameObject rewardScreenExtras;

    public bool isReward;

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
        turnManager = GameObject.Find("Scripts").GetComponent<TurnManager>();
        switchScreen = GameObject.Find("genScripts").GetComponent<SwitchScreen>();
        mapControls = GameObject.Find("mapScripts").GetComponent<MapControls>();
        worldCanvas = GameObject.Find("WorldCanvas");
        rewardScreenExtras = GameObject.Find("RewardScreenExtras");
        clickDeckSlot |= 1 << 7;

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

    private void Awake()
    {
        spawnPointOriginalPosition = gameObject.transform.GetChild(0).transform.localPosition;

    }


    // Update is called once per frame
    void Update()
    {
        upPos = dnPos + new Vector3(0, .1f, 0);
        if (!cardClicked)
        {

                //if not clicked, moves to inventory
                transform.position = Vector3.MoveTowards(transform.position, currPos, speed * Time.deltaTime);
                if (transform.position == dnPos)
                {
                    speed = 60;
                }

                //if inspection card is spawned, card follows mouse
                if (inspectionCardInstantiation != null)
                {
                    //inspectionCardInstantiation.transform.position = new Vector3(mousePosition.x, mousePosition.y, -2);
                }
            
        }
        if (cardClicked)
        {
            // if clicked, follows mouse
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, -5);


        }


        if (itemEquipped && abilityOnEquip && !abilityOnEquipActivated)
        {
            abilityOnEquipActivated = true;
            turnManager.processOnEquip(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (!itemEquipped && !isReward)
        {
            currPos = upPos;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inspectionCardInstantiation = Instantiate(inspectionCardPrefab, new Vector3(mousePosition.x + 1.5f, mousePosition.y + 1.5f, -15), Quaternion.identity, worldCanvas.transform);
        


    }

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (!itemEquipped)
            {
                
                var targetSlot = GameObject.FindGameObjectWithTag(itemType);
                inventoryManager.InvManagement(gameObject, targetSlot);
            }
            else if (itemEquipped)
            {

                inventoryManager.unsetInventory(gameObject);
                if (!isReward)
                {
                    currPos = dnPos;
                }

            }
        }
    }

    private void OnMouseExit()
    {
        if (!itemEquipped && !isReward)
        {
            currPos = dnPos;
        }
        
        Destroy(inspectionCardInstantiation);
        inspectionCardInstantiation = null;
        
        
    }

    private void OnMouseDown()
    {
        //var cardFrame = this.gameObject.transform.GetChild(0).gameObject;
        //var cardImage = cardFrame.transform.GetChild(2).gameObject;
        if (!cardClicked && !isReward && inventoryManager.carryingCard == null)
        {

            var inventorySlots = inventoryManager.InventoryItems;
            foreach (var slot in inventorySlots)
            {
                if (slot.Key.transform.tag == itemType)
                {
                    slot.Key.GetComponent<SlotHighlighting>().startAnimation();
                }
            }
            //when clicked, follows the mouse
            if (!itemEquipped)
            {
                cardClicked = true;
                inventoryManager.carryingCard = gameObject;
                foreach (var inventorySlot in inventoryGridGenerator.Inventory)
                {
                    if (inventorySlot.GetComponent<InventorySlotManager>().equippedItem == gameObject)
                    {
                        inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                        inventorySlot.GetComponent<InventorySlotManager>().equippedItem = null;
                    }
                }


                //makes card smaller and transparent while following the mouse
                //var tempVec = new Vector3((float)originalCardSize.x / 2, (float)originalCardSize.y / 2, (float)originalCardSize.z);
                //transform.localScale = tempVec;
                //transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).gameObject.SetActive(false);
                //cardFrame.GetComponent<SpriteRenderer>(). = new Color(1, 1, 1, .5f);
                //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
                
            }
            //if its currently equipped and youre not carrying another card, will follow the mouse
            else if (itemEquipped && inventoryManager.carryingCard == null && !isSticky)
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

            //if its currently equipped but you're carrying another card, sends equipped card back to deck and equips the card youre carrying
            else if (itemEquipped && inventoryManager.carryingCard != null /*&& !isSticky */)
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
        else if (cardClicked && !isReward)
        {
            var inventorySlots = inventoryManager.InventoryItems;
            foreach (var slot in inventorySlots)
            {
                if (slot.Key.transform.tag == itemType)
                {
                    slot.Key.GetComponent<SlotHighlighting>().StopAnimation();
                }
            }
            cardClicked = false;
            speed = 60;
            currPos = dnPos;
            inventoryManager.carryingCard = null;

            //returns card to original size and opacity
            transform.localScale = originalCardSize;
            //cardFrame.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //cardImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            //if clicked on a inventory slot though, it equips it to the slot
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, clickDeckSlot);
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, clickInventory);
            if (hit)
            {
                checkIfSetInDeckSlot(gameObject, hit.transform.gameObject);
            }
            else if (hit2)
            {
                checkIfSetInInventory(gameObject);
            }
            else
            {
                
                inventoryGridGenerator.MoveBackToInventory(gameObject);
                inventoryManager.unsetInventory(gameObject);
            }
            //checkIfSetInInventory(gameObject);
            //checkIfSetInDeckSlot(gameObject);
        }

        else if (isReward)
        {
            isReward = false;
            foreach (var card in deckManager.obtainableCards)
            {
                if(card.transform.name == gameObject.transform.name)
                {
                    deckManager.addCardToLibrary(card);
                }
            }
            //deckManager.addCardToLibrary(gameObject);
            Destroy(inspectionCardInstantiation);
            rewardScreenExtras.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(false);

            switchScreen.currentScreen = "Map";
            switchScreen.changeScreen("Map");
            mapControls.inBattle = false;
        }
        //deckManager.updateHandPosition();

    }

    private void checkIfSetInDeckSlot(GameObject card, GameObject deckSlot)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, clickDeckSlot);
        //clicked an inventory slot
        if (hit)
        {
            inventoryGridGenerator.moveItemInInventory(card, deckSlot);
            //inventoryManager.InvManagement(card, hit.collider.gameObject);
        }

        //didnt click an inventory slot
        if (!hit)
        {
            if (itemEquipped)
            {
                //inventoryManager.unsetInventory(gameObject);
            }
        }

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
