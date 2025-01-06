using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<GameObject> helmSlots = new List<GameObject>();
    private bool displayingExtraSlots;

    
    public LayerMask IgnoreCards;
    public LayerMask inventorySlotsMask;


    public int carryingCapacity;
    public GameObject carryingCapacityObject;

    public GameObject carryingCard;
    private bool dontUnequip;

    public TurnManager turnManager;

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

        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, inventorySlotsMask);
        if (hit.collider != null && (hit.collider.transform.name == "Helmet" || hit.collider.transform.name == "ExtraHelmSlots") && helmSlots.Count > 1)
        {
            displayingExtraSlots = true;
            displayHelmets();
        }
        if (hit.collider == null && displayingExtraSlots)
        {
            undisplayHelmets();
            displayingExtraSlots = false;
        }
        

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
    }

    public void InvManagement(GameObject item, GameObject targetSlot)
    {
        int currCarryingCapacity = int.Parse(carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        int itemCarryingWeight = item.GetComponent<CardManager>().carryingWeight;
        var itemType = item.GetComponent<CardManager>().itemType;
        var itemEquipped = item.GetComponent<CardManager>().itemEquipped;
        var slotType = targetSlot.tag;
        dontUnequip = false;

        /*foreach (var slot in InventoryItems)
        {
            tempInventory.Add(slot.Key, slot.Value);
        }*/



        //if right inventory type
        if (itemType == slotType || itemType == "Anywhere")
        {
            //if inventory slot is not already occupied
            if (InventoryItems[targetSlot] == null && itemCarryingWeight <= currCarryingCapacity)
            {
                //if not equipped in another slot yet
                if (!itemEquipped)
                {
                    //currCarryingCapacity -= itemCarryingWeight;
                    setInventory(item, targetSlot);
                    
                }
                //if item is equipped in another slot, unequips it from that one and equips it to new one
                else if (itemEquipped)
                {
                    dontUnequip = true;
                    unsetInventory(item);
                    setInventory(item, targetSlot);
                }
            }
            //if inventory slot is already occupied                             checks weight against if the item already equipped is then unequipped, adding its weight to the pool before checking.
            else if (InventoryItems[targetSlot] != null && itemCarryingWeight <= currCarryingCapacity + InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight)
            {
                //unequips inventory first
                Debug.Log("Help");
                //Debug.Log(InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight);
                //currCarryingCapacity += InventoryItems[targetSlot].GetComponent<CardManager>().carryingWeight;

                if (InventoryItems[targetSlot] != item)
                {
                    unsetInventory(InventoryItems[targetSlot]);
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
                    dontUnequip = true;
                    unsetInventory(item);
                    setInventory(item, targetSlot);
                }

            }
            //carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currCarryingCapacity.ToString();
            
        }


        // if not right inventory slot
        if(itemType != slotType && itemType != "Anywhere")
        {
            if (itemEquipped)
            {
                unsetInventory(item);
            }
        }

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
        item.transform.position = new Vector3(targetSlot.transform.position.x, targetSlot.transform.position.y, targetSlot.transform.position.z - 5f);


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
        turnManager.preProcessCards(item);
    }

    public void unsetInventory(GameObject item)
    {
        foreach (var slot in InventoryItems)
        {
            if (slot.Value == item)
            {
                InventoryItems[slot.Key] = null;
                break;
            }
        }
        //item.transform.GetChild(0).gameObject.SetActive(true);
        //item.transform.GetChild(1).gameObject.SetActive(false);

        int currCarryingCapacity = int.Parse(carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        int itemCarryingWeight = item.GetComponent<CardManager>().carryingWeight;
        currCarryingCapacity += itemCarryingWeight;
        carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currCarryingCapacity.ToString();


        item.GetComponent<CardManager>().itemEquipped = false;
        item.transform.localScale = item.GetComponent<CardManager>().originalCardSize;

        //item.GetComponent<CardManager>().cardClicked = false;
        if (!dontUnequip)
        {
            turnManager.preProcessRemove(item);
        }
        /*foreach (var items in InventoryItems)
        {
            print("Slot: " + items.Key + " is equipped with: " + items.Value);
        }*/


    }

    public void addInventorySlot(int amount, string slotType)
    {
        if (slotType == "Helmet" && !dontUnequip)
        {
            var oldHelmAmount = helmSlotAmount;
            helmSlotAmount += amount;
            var tempHelmPrefab = helmPrefab;
            for (int i = 1; i <= helmSlotAmount; i++)
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
            }
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
