using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<GameObject, GameObject> InventoryItems = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> tempInventory = new Dictionary<GameObject, GameObject>();
    public List<GameObject> equippedSlots = new List<GameObject>();


    public GameObject Helmet;
    public GameObject Torso;
    public GameObject Shoulder;
    public GameObject Cape;
    public GameObject Weapon;
    //public GameObject Weapon2;
    public GameObject Pants;
    public GameObject Boots;
    public GameObject Potion;
    public GameObject Potion2;
    //public GameObject Potion3;


    public GameObject helmPrefab;
    private int helmSlotAmount = 1;
    public List<GameObject> helmSlots = new List<GameObject>();
    private bool displayingExtraSlots;
    public GameObject InventoryParent;

    
    public LayerMask IgnoreCards;
    public LayerMask inventorySlotsMask;


    public int carryingCapacity;
    public GameObject carryingCapacityObject;

    public GameObject carryingCard;
    private bool dontUnequip;

    public TurnManager turnManager;
    public InventoryGridGenerator gridGenerator;


    public bool GeminiNecklaceActivated;
    public bool SlimeKingGlovesActivated;

    public List<GameObject> extraEquippedItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InventoryItems.Add(Weapon, null);
        InventoryItems.Add(Helmet, null);
        InventoryItems.Add(Torso, null);
        InventoryItems.Add(Shoulder, null);
        InventoryItems.Add(Cape, null);
        //InventoryItems.Add(Weapon2, null);
        InventoryItems.Add(Pants, null);
        InventoryItems.Add(Boots, null);
        InventoryItems.Add(Potion, null);
        InventoryItems.Add(Potion2, null);
        //InventoryItems.Add(Potion3, null);

        carryingCapacity = 5;

        helmSlots.Add(Helmet);

        
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, inventorySlotsMask);
        if (hit.collider != null && (hit.collider.transform.name == "Helmet" || hit.collider.transform.name == "ExtraHelmSlots") && helmSlots.Count > 1)
        {
            displayingExtraSlots = true;
            displayHelmets();
        }
        if (hit.collider == null && displayingExtraSlots)
        {
            undisplayHelmets();
            displayingExtraSlots = false;
        }*/
        

    }

    public void clearInventory()
    {
        //copies all equipped items into seperate list
        foreach (var item in InventoryItems)
        {
            if (item.Value != null)
            {
                equippedSlots.Add(item.Key);
            }
        }

        //uses equipedItems list to find each item and clear it
        foreach (var slot in equippedSlots)
        {
            InventoryItems[slot] = null;
        }
        equippedSlots.Clear();
        extraEquippedItems.Clear();
    }

    public void InvManagement(GameObject item, GameObject targetSlot)
    {
        Debug.Log("setting slot");
        int currCarryingCapacity = int.Parse(carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        int itemCarryingWeight = item.GetComponent<CardManager>().carryingWeight;
        var itemType = item.GetComponent<CardManager>().itemType;
        var itemEquipped = item.GetComponent<CardManager>().itemEquipped;
        var slotType = targetSlot.tag;
        //dontUnequip = false;

        /*foreach (var slot in InventoryItems)
        {
            tempInventory.Add(slot.Key, slot.Value);
        }*/



        //if right inventory type
        if (itemType == slotType || itemType == "Anywhere")
        {
            Debug.Log("Right inventory slot");
            //if inventory slot is not already occupied
            if (InventoryItems[targetSlot] == null && itemCarryingWeight <= currCarryingCapacity)
            {
                Debug.Log("Not Occupied");
                //if not equipped in another slot yet
                if (!itemEquipped)
                {

                    //currCarryingCapacity -= itemCarryingWeight;
                    setInventory(item, targetSlot);
                    
                }
                //if item is equipped in another slot, unequips it from that one and equips it to new one
                else if (itemEquipped)
                {
                    //dontUnequip = true;
                    unsetInventory(item);
                    setInventory(item, targetSlot);
                }
            }
            //if inventory slot is already occupied                             checks weight against if the item already equipped is then unequipped, adding its weight to the pool before checking.
            else if (InventoryItems[targetSlot] != null && itemCarryingWeight <= currCarryingCapacity + InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight)
            {
                Debug.Log("Slot Occupied");
                //unequips inventory first
                //Debug.Log("Help");
                //Debug.Log(InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight);
                //currCarryingCapacity += InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight;

                if (InventoryItems[targetSlot] != item)
                {
                    var oldItem = InventoryItems[targetSlot];
                    unsetInventory(oldItem);
                    gridGenerator.addNewItemToInventory(oldItem);
                    Destroy(oldItem);
                }
                //if not equipped in another slot yet
                if (!itemEquipped)
                {
                    //currCarryingCapacity -= itemCarryingWeight;
                    setInventory(item, targetSlot);
                }
                //if item is equipped in another slot, unequips it from that one and equips it to new one
                else if (itemEquipped)
                {
                    //dontUnequip = true;
                    unsetInventory(item);
                    setInventory(item, targetSlot);
                }

            }
            //carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currCarryingCapacity.ToString();
            
        }


        // if not right inventory slot
        if (itemType != slotType && itemType != "Anywhere")
        {
            if (itemEquipped)
            {
                unsetInventory(item);
            }

            //if trying to put the items on slime kings gloves
            if (itemType == "Weapon" && slotType == "Gloves" && SlimeKingGlovesActivated)
            {
                extraEquippedItems.Add(item);
                item.GetComponent<CardManager>().isSticky = true;
                item.GetComponent<CardManager>().itemEquipped = true;

                //gives the card the same position
                //item.transform.position = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);
                item.GetComponent<CardManager>().currPos = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);
                item.GetComponent<CardManager>().dnPos = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);
                //item.GetComponent<CardManager>().upPos = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);

                //sets card to the size of the slot
                Vector3 refSize = targetSlot.GetComponent<Renderer>().bounds.size;
                Vector3 refSize2 = item.GetComponent<Renderer>().bounds.size;
                float resizeX = refSize.x / refSize2.x;
                float resizeY = refSize.y / refSize2.y;
                resizeX *= item.transform.localScale.x;
                resizeY *= item.transform.localScale.y;
                item.transform.localScale = new Vector3(resizeX, resizeY, 1);

                if (item.GetComponent<CardManager>().abilityTriggered == false)
                {
                    item.GetComponent<CardManager>().abilityTriggered = true;
                    turnManager.preProcessCards(item);
                }
            }
        }





        /*else if (itemType == "Weapon" && slotType == "Gloves" && SlimeKingGlovesActivated)
        {
            extraEquippedItems.Add(item);
            item.GetComponent<CardManager>().isSticky = true;

            //gives the card the same position
            //item.transform.position = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);
            item.GetComponent<CardManager>().currPos = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);

            //sets card to the size of the slot
            Vector3 refSize = targetSlot.GetComponent<Renderer>().bounds.size;
            Vector3 refSize2 = item.GetComponent<Renderer>().bounds.size;
            float resizeX = refSize.x / refSize2.x;
            float resizeY = refSize.y / refSize2.y;
            resizeX *= item.transform.localScale.x;
            resizeY *= item.transform.localScale.y;
            item.transform.localScale = new Vector3(resizeX, resizeY, 1);

            if (item.GetComponent<CardManager>().abilityTriggered == false)
            {
                item.GetComponent<CardManager>().abilityTriggered = true;
                turnManager.preProcessCards(item);
            }
        }*/

        //foreach (var items in InventoryItems)
        //{
            //print("Slot: " + items.Key + " is equipped with: " + items.Value);
        //}
        //updateInventory();
    }

    private void updateInventory()
    {
        //InventoryItems.Clear();
        /*foreach (var slot in tempInventory)
        {
            Debug.Log(slot.Key + "       " + slot.Value);
            InventoryItems.Add(slot.Key, slot.Value);
        }
        tempInventory.Clear();*/

        foreach (var items in InventoryItems)
        {
            print("Slot: " + items.Key + " is equipped with: " + items.Value);
        }
    }





    public void setInventory(GameObject item, GameObject targetSlot)
    {
        Debug.Log("Setting Inventory");
        item.GetComponent<CardManager>().itemEquipped = true;
        InventoryItems[targetSlot] = item;

        //sets mana from card value
        //Mana -= cardMana;
        //GameObject.Find("manaAmount").GetComponent<TMPro.TextMeshPro>().text = Mana.ToString();
        int currCarryingCapacity = int.Parse(carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        int itemCarryingWeight = item.GetComponent<CardManager>().carryingWeight;
        currCarryingCapacity -= itemCarryingWeight;
        carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currCarryingCapacity.ToString();

        //gives the card the same position
        //item.transform.position = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);
        item.GetComponent<CardManager>().currPos = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);

        //sets card to the size of the slot
        Vector3 refSize = targetSlot.GetComponent<Renderer>().bounds.size;
        Vector3 refSize2 = item.GetComponent<Renderer>().bounds.size;
        float resizeX = refSize.x / refSize2.x;
        float resizeY = refSize.y / refSize2.y;
        resizeX *= item.transform.localScale.x;
        resizeY *= item.transform.localScale.y;
        item.transform.localScale = new Vector3(resizeX, resizeY, 1);

        //item.transform.GetChild(0).gameObject.SetActive(false);
        //item.transform.GetChild(1).gameObject.SetActive(true);
        if (item.GetComponent<CardManager>().abilityTriggered == false)
        {
            item.GetComponent<CardManager>().abilityTriggered = true;
            turnManager.preProcessCards(item);
        }
        if (GeminiNecklaceActivated && item.transform.name != "GeminiNecklace(Clone)")
        {
            GeminiNecklace(item);
        }

        foreach (var inventorySlots in gridGenerator.Inventory)
        {
            if (inventorySlots.GetComponent<InventorySlotManager>().equippedItem == item)
            {
                inventorySlots.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                inventorySlots.GetComponent<InventorySlotManager>().equippedItem = null;
            }
        }

        item.GetComponent<CardManager>().currPos = targetSlot.transform.position + new Vector3(0,0,-1);
        item.GetComponent<CardManager>().dnPos = targetSlot.transform.position + new Vector3(0,0,-1);
        item.GetComponent<CardManager>().upPos = item.transform.position + new Vector3(0, .1f, -1);
    }

    public void unsetInventory(GameObject item)
    {
        bool itemWasEquipped = false;
        foreach (var slot in InventoryItems)
        {
            if (slot.Value == item)
            {
                itemWasEquipped = true;
                InventoryItems[slot.Key] = null;
                break;
            }
        }
        //item.transform.GetChild(0).gameObject.SetActive(true);
        //item.transform.GetChild(1).gameObject.SetActive(false);

        int currCarryingCapacity = int.Parse(carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        int itemCarryingWeight = item.GetComponent<CardManager>().carryingWeight;
        currCarryingCapacity += itemCarryingWeight;

        if (itemWasEquipped)
        {
            carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currCarryingCapacity.ToString();
        }


        item.GetComponent<CardManager>().itemEquipped = false;
        item.GetComponent<CardManager>().currPos = item.GetComponent<CardManager>().dnPos;
        item.transform.localScale = item.GetComponent<CardManager>().originalCardSize;

        //item.GetComponent<CardManager>().cardClicked = false;
        if (!dontUnequip && item.GetComponent<CardManager>().abilityTriggered)
        {
            item.GetComponent<CardManager>().abilityTriggered = false;
            turnManager.preProcessRemove(item);
        }
        /*foreach (var items in InventoryItems)
        {
            print("Slot: " + items.Key + " is equipped with: " + items.Value);
        }*/
        



    }

    private void GeminiNecklace(GameObject item)
    {
        InventoryItems[helmSlots[0]].GetComponent<CardManager>().isSticky = true;
        var itemType = item.GetComponent<CardManager>().itemType;
        var itemClone = Instantiate(item, new Vector3(0,0,0), Quaternion.identity);
        itemClone.transform.name = item.transform.name;
        //itemClone.name.Replace("(Clone)", "").Trim();
        //itemClone.name.Replace("(Clone)(Clone)", "").Trim();


        var newSlot = helmSlots.Last();

        if (itemType == "Helmet")
        {
            addInventorySlot(1, "Helmet");
            newSlot = helmSlots.Last();
            
        }
        itemClone.transform.position = new Vector3(newSlot.transform.position.x, newSlot.transform.position.y, newSlot.transform.position.z - 1);
        itemClone.GetComponent<CardManager>().isSticky = true;
        InventoryItems[newSlot] = itemClone;
        GeminiNecklaceActivated = false;



    }

    public void addInventorySlot(int amount, string slotType)
    {
        if (slotType == "Helmet" && !dontUnequip)
        {
            var oldHelmAmount = helmSlotAmount;
            helmSlotAmount += amount;
            var tempHelmPrefab = helmPrefab;

            for (int i = 1; i < helmSlotAmount; i++)
            {
                var newHelmSlot = Instantiate(tempHelmPrefab, new Vector3(Helmet.transform.position.x, Helmet.transform.position.y, Helmet.transform.position.z), Quaternion.identity);
                newHelmSlot.name = "Helmet";
                newHelmSlot.tag = "Helmet";
                newHelmSlot.transform.SetParent(InventoryParent.transform);
                helmSlots.Add(newHelmSlot);
                InventoryItems.Add(newHelmSlot, null);
                /*if (i == 2 && i > oldHelmAmount)
                {
                    //tempHelmPrefab.transform.GetComponent<SpriteRenderer>().color = new Color(.6f, .6f, .6f, 1);
                    var newHelmSlot = Instantiate(tempHelmPrefab, new Vector3(Helmet.transform.position.x, Helmet.transform.position.y, Helmet.transform.position.z), Quaternion.identity);
                    newHelmSlot.name = "Helmet";
                    helmSlots.Add(newHelmSlot);
                    InventoryItems.Add(newHelmSlot, null);
                }
                if (i >= 3 && i > oldHelmAmount)
                {
                    //tempHelmPrefab.transform.GetComponent<SpriteRenderer>().color = new Color(.4f, .4f, .4f, 1);
                    var newHelmSlot = Instantiate(tempHelmPrefab, new Vector3(Helmet.transform.position.x + .2f, Helmet.transform.position.y - .2f, Helmet.transform.position.z), Quaternion.identity);
                    newHelmSlot.name = "Helmet";
                    helmSlots.Add(newHelmSlot);
                    InventoryItems.Add(newHelmSlot, null);
                }*/
            }
            if (helmSlotAmount == 2)
            {
                helmSlots[0].transform.position = new Vector3(-5f, 3.03f, 0);
                helmSlots[1].transform.position = new Vector3(helmSlots[0].transform.position.x + 1.56f, helmSlots[0].transform.position.y, 0);
            }
            else if (helmSlotAmount == 3)
            {
                helmSlots[0].transform.position = new Vector3(-4.36f, 3.03f, 0);
                helmSlots[1].transform.position = new Vector3(-6f, 3.03f, 0);
                helmSlots[2].transform.position = new Vector3(-2.72f, 3.03f, 0); 
            }
            InventoryItems[helmSlots[0]].GetComponent<CardManager>().currPos = new Vector3(helmSlots[0].transform.position.x, helmSlots[0].transform.position.y, helmSlots[0].transform.position.z - 5f);




        }

    }

    public void removeInventorySlot(int amount, string slotType)
    {
        if (slotType == "Helmet")
        {
            var oldHelmAmount = helmSlotAmount;
            helmSlotAmount -= amount;
            //var tempHelmPrefab = helmPrefab;

            //if the slot corresponding to the last helm slot is empty, just removes it and deletes it
            for (int i = 0; i < amount; i++)
            {
                var lastHelmSlot = helmSlots[helmSlots.Count - 1];
                if (InventoryItems[lastHelmSlot] == null)
                {
                    InventoryItems.Remove(lastHelmSlot);
                    helmSlots.Remove(lastHelmSlot);
                    Destroy(lastHelmSlot);
                }
                else if (InventoryItems[lastHelmSlot] != null)
                {
                    unsetInventory(InventoryItems[lastHelmSlot]);
                    InventoryItems.Remove(lastHelmSlot);
                    helmSlots.Remove(lastHelmSlot);
                    Destroy(lastHelmSlot);

                }
            }
            /*for (int i = 1; i <= helmSlotAmount; i++)
            {
                if (i == 2 && i > oldHelmAmount)
                {
                    tempHelmPrefab.transform.GetComponent<SpriteRenderer>().color = new Color(.6f, .6f, .6f, 1);
                    var newHelmSlot = Instantiate(tempHelmPrefab, new Vector3(Helmet.transform.position.x + .1f, Helmet.transform.position.y - .1f, Helmet.transform.position.z + .1f), Quaternion.identity);
                    newHelmSlot.name = "Helmet";
                    helmSlots.Add(newHelmSlot);
                    InventoryItems.Add(newHelmSlot, null);
                }
                if (i >= 3 && i > oldHelmAmount)
                {
                    tempHelmPrefab.transform.GetComponent<SpriteRenderer>().color = new Color(.4f, .4f, .4f, 1);
                    var newHelmSlot = Instantiate(tempHelmPrefab, new Vector3(Helmet.transform.position.x + .2f, Helmet.transform.position.y - .2f, Helmet.transform.position.z + .2f), Quaternion.identity);
                    newHelmSlot.name = "Helmet";
                    helmSlots.Add(newHelmSlot);
                    InventoryItems.Add(newHelmSlot, null);
                }
            }*/
        }
    }
    private void displayHelmets()
    {
        foreach (var items in InventoryItems)
        {
            items.Key.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        }
        Helmet.transform.GetChild(0).gameObject.SetActive(true);
        var xcounter = 1;
        var ycounter = 0;
        foreach(GameObject helmetSlot in helmSlots)
        {
            helmetSlot.transform.position = new Vector3(Helmet.transform.position.x + (1.5f * xcounter), Helmet.transform.position.y - (1.5f * ycounter), -2);
            if (InventoryItems[helmetSlot] != null)
            {
                InventoryItems[helmetSlot].transform.position = new Vector3(Helmet.transform.position.x + (1.5f * xcounter), Helmet.transform.position.y - (1.5f * ycounter), -2);
            }
            helmetSlot.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            xcounter += 1;
            if (xcounter == 4)
            {
                xcounter = 1;
                ycounter += 1;
            }
        }
    }
    
    private void undisplayHelmets()
    {
        foreach (var items in InventoryItems)
        {
            items.Key.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        Helmet.transform.GetChild(0).gameObject.SetActive(false);

        var counter = 1;
        foreach (GameObject helmetSlot in helmSlots)
        {
            if (counter == 1)
            {
                helmetSlot.transform.position = new Vector3(Helmet.transform.position.x + .1f, Helmet.transform.position.y - .1f, Helmet.transform.position.z + .1f);
                if (InventoryItems[helmetSlot] != null)
                {
                    InventoryItems[helmetSlot].transform.position = new Vector3(Helmet.transform.position.x + .1f, Helmet.transform.position.y - .1f, Helmet.transform.position.z + .1f);
                }
                helmetSlot.transform.GetComponent<SpriteRenderer>().color = new Color(.6f, .6f, .6f, 1);

            }
            if (counter >= 2)
            {
                helmetSlot.transform.position = new Vector3(Helmet.transform.position.x + .2f, Helmet.transform.position.y - .2f, Helmet.transform.position.z + .2f);
                if (InventoryItems[helmetSlot] != null)
                {
                    InventoryItems[helmetSlot].transform.position = new Vector3(Helmet.transform.position.x + .2f, Helmet.transform.position.y - .2f, Helmet.transform.position.z + .2f);
                }
                helmetSlot.transform.GetComponent<SpriteRenderer>().color = new Color(.4f, .4f, .4f, 1);


            }

            counter += 1;
        }
    }
}
