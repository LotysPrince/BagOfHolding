using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridGenerator : MonoBehaviour
{
    public GameObject[,] Inventory = new GameObject[6, 3];

    public GameObject inventory11;
    public GameObject inventory12;
    public GameObject inventory13;
    public GameObject inventory14;
    public GameObject inventory15;
    public GameObject inventory16;
    public GameObject inventory21;
    public GameObject inventory22;
    public GameObject inventory23;
    public GameObject inventory24;
    public GameObject inventory25;
    public GameObject inventory26;
    public GameObject inventory31;
    public GameObject inventory32;
    public GameObject inventory33;
    public GameObject inventory34;
    public GameObject inventory35;
    public GameObject inventory36;

    public GameObject hiddenDagger;
    public GameObject spear;

    public int arrayXPos;
    public int arrayYPos;

    public List<GameObject> debugInventory = new List<GameObject>();
    public List<GameObject> currentInventory = new List<GameObject>();
    public List<GameObject> itemsSpawned = new List<GameObject>();
    public DeckManager deckManager;

    private GameObject itemToSpawn;
    public GameObject newItem;
    public InventoryManager inventoryManager;
    private int numItemsSpawned;

    // Start is called before the first frame update
    void Start()
    {
        //equips all physical slots to coding slots in array
        Inventory[0, 0] = inventory11;
        Inventory[1, 0] = inventory12;
        Inventory[2, 0] = inventory13;
        Inventory[3, 0] = inventory14;
        Inventory[4, 0] = inventory15;
        Inventory[5, 0] = inventory16;

        Inventory[0, 1] = inventory21;
        Inventory[1, 1] = inventory22;
        Inventory[2, 1] = inventory23;
        Inventory[3, 1] = inventory24;
        Inventory[4, 1] = inventory25;
        Inventory[5, 1] = inventory26;

        Inventory[0, 2] = inventory31;
        Inventory[1, 2] = inventory32;
        Inventory[2, 2] = inventory33;
        Inventory[3, 2] = inventory34;
        Inventory[4, 2] = inventory35;
        Inventory[5, 2] = inventory36;
        ///////////////////////////////////////////////////////
        



    }

    public void GenerateInventory() {

        bool itemCantBePlaced = true;
        bool debugInventoryDrawn = false;
        foreach (var inventorySlot in Inventory)
        {
            if (inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped == false && currentInventory.Count > 0 && itemsSpawned.Count < deckManager.handSize)
            {
                itemCantBePlaced = true;
                bool itemDeleted = false;
                if (debugInventory.Count != 0 && debugInventoryDrawn == false)
                {
                    debugInventoryDrawn = true;
                    itemToSpawn = debugInventory[0];
                }
                else if (currentInventory.Count != 0)
                {

                    itemToSpawn = currentInventory[0];
                }
                newItem = Instantiate(itemToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
                newItem.transform.name = itemToSpawn.transform.name;
                //newItem.name.Replace("(Clone)", "").Trim();
                //newItem.name.Replace("(Clone)(Clone)", "").Trim();
                //itemToSpawn = currentInventory[0];
                //Debug.Log(itemToSpawn.transform.name);
                //currentInventory.Remove(itemToSpawn);

                //spawns the item
                newItem.transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                newItem.transform.rotation = Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z));
                //var newItem = Instantiate(itemToSpawn, new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f),
                    //Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z)));

                //moves the item to where the spawn point is, using the offest of where the spawn point originally was
                Vector3 newItemPos = newItem.transform.position;
                Vector3 spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                Vector3 spawnPointPos = newItem.transform.GetChild(0).transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);;
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
                spawnPointPos = spawnPointPosOriginal;
                newItem.transform.GetChild(0).transform.localPosition = newItem.GetComponent<CardManager>().spawnPointOriginalPosition;
                findInInvArray(inventorySlot);
                var xCounter = 0;
                var yCounter = 0;
                //sets the inventory slots it needs to based off the array on its card script
                foreach (var slotToEquip in newItem.GetComponent<CardManager>().slotsToEquipStrings)
                {
                    if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                    {
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = newItem;
                        newItem.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);
                        //itemsSpawned.Add(newItem);
                        //currentInventory.Remove(itemToSpawn);

                    }

                    //if slot is out of range but doesnt need to be equipped, continue
                    else if ((arrayXPos + xCounter >= 6 && !slotToEquip)
                        || (arrayYPos + yCounter >= 3 && !slotToEquip))
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is out of range but needs to be equipped, unequip and delete it
                    else if ((arrayXPos + xCounter >= 6 && slotToEquip)
                        || (arrayYPos + yCounter >= 3 && slotToEquip))
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        Destroy(newItem);
                        break;
                    }

                    //if slot is already occupied but item doesnt need it to equip here, continue
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && !slotToEquip)
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is already occupied and item needs the slot, unequip and delete it
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && slotToEquip)
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        Destroy(newItem);
                        break;
                    }

                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }

                }
                // if the item was placed successfully, removes it from inventory pool and adds to itemSpawns pool
                if (itemDeleted == false)
                {
                    //Debug.Log("item placed");
                    itemCantBePlaced = false;
                    currentInventory.Remove(itemToSpawn);
                    itemsSpawned.Add(newItem);

                    break;
                }


            }
        }
        //if the item iterated through the entire grid and couldnt be placed, removes it from inventory
        if (itemCantBePlaced== true)
        {
            Debug.Log("itemNotSpawned");
            Debug.Log(itemToSpawn.transform.name);
            currentInventory.Remove(itemToSpawn);
            //currentInventory.Remove(itemToSpawn);
            deckManager.currentDeck.Insert(0,itemToSpawn);
            //deckManager.currentDeck.Add(itemToSpawn);
            deckManager.currentHand.Remove(itemToSpawn);
            
        }
        //if more items to spawn, continue iterating
        if (currentInventory.Count > 0 && itemsSpawned.Count < deckManager.handSize && numItemsSpawned <= 6)
        {
            numItemsSpawned += 1;
            GenerateInventory();
        }
        else
        {
            numItemsSpawned = 0;
        }
    }

    public void MoveBackToInventory(GameObject cardToAdd)
    {
        bool itemCantBePlaced = true;
        foreach (var inventorySlot in Inventory)
        {
            if (inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped == false /*&& currentInventory.Count > 0*/)
            {
                bool itemDeleted = false;
                itemToSpawn = cardToAdd;
                newItem = itemToSpawn;
                //newItem.transform.name = itemToSpawn.transform.name;
                //newItem.name.Replace("(Clone)", "").Trim();
                //newItem.name.Replace("(Clone)(Clone)", "").Trim();


                //itemToSpawn = currentInventory[0];
                //Debug.Log(itemToSpawn.transform.name);
                //currentInventory.Remove(itemToSpawn);

                //spawns the item
                newItem.transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                newItem.transform.rotation = Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z));
                //var newItem = Instantiate(itemToSpawn, new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f),
                //Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z)));

                //moves the item to where the spawn point is, using the offest of where the spawn point originally was
                /*var newItemPos = newItem.transform.position;
                var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                var spawnPointPos = newItem.transform.GetChild(0).transform.position + new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
                spawnPointPos = spawnPointPosOriginal;
                newItem.transform.GetChild(0).transform.localPosition = newItem.GetComponent<CardManager>().spawnPointOriginalPosition;*/
                newItem.transform.position = inventorySlot.transform.position;
                var newItemPos = newItem.transform.position;
                var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                var spawnPointPos = newItem.transform.GetChild(0).transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z - 5);
                //item.transform.position = newItemPos;
                spawnPointPos = spawnPointPosOriginal;
                newItem.transform.GetChild(0).transform.localPosition = newItem.GetComponent<CardManager>().spawnPointOriginalPosition;
                newItem.GetComponent<CardManager>().currPos = newItem.transform.position;
                newItem.GetComponent<CardManager>().dnPos = newItem.transform.position;
                newItem.GetComponent<CardManager>().upPos = newItem.transform.position + new Vector3(0, .1f, 0);
                



                findInInvArray(inventorySlot);
                var xCounter = 0;
                var yCounter = 0;
                //sets the inventory slots it needs to based off the array on its card script
                foreach (var slotToEquip in newItem.GetComponent<CardManager>().slotsToEquipStrings)
                {
                    if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                    {
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = newItem;
                        newItem.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);
                        //itemsSpawned.Add(newItem);
                        //currentInventory.Remove(itemToSpawn);

                    }

                    //if slot is out of range but doesnt need to be equipped, continue
                    else if ((arrayXPos + xCounter >= 6 && !slotToEquip)
                        || (arrayYPos + yCounter >= 3 && !slotToEquip))
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is out of range but needs to be equipped, unequip and delete it
                    else if ((arrayXPos + xCounter >= 6 && slotToEquip)
                        || (arrayYPos + yCounter >= 3 && slotToEquip))
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        //Destroy(newItem);
                        continue;
                    }

                    //if slot is already occupied but item doesnt need it to equip here, continue
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && !slotToEquip)
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is already occupied and item needs the slot, unequip and delete it
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && slotToEquip)
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        //Destroy(newItem);
                        continue;
                    }

                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }

                }
                // if the item was placed successfully, removes it from inventory pool and adds to itemSpawns pool
                if (itemDeleted == false)
                {
                    //Debug.Log("item placed");
                    itemCantBePlaced = false;
                    //currentInventory.Remove(itemToSpawn);
                    //itemsSpawned.Add(newItem);
                    break;
                }


            }
        }
        //if the item iterated through the entire grid and couldnt be placed, removes it from inventory
        if (itemCantBePlaced == true)
        {
            //currentInventory.Remove(itemToSpawn);
        }
    }

    public void addNewItemToInventory(GameObject cardToAdd)
    {
        bool itemCantBePlaced = true;
        foreach (var inventorySlot in Inventory)
        {
            if (inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped == false /*&& currentInventory.Count > 0*/)
            {
                bool itemDeleted = false;
                itemToSpawn = cardToAdd;
                newItem = Instantiate(itemToSpawn, new Vector3(-20, -50, 0), Quaternion.identity);
                newItem.transform.name = itemToSpawn.transform.name;
                //newItem.name.Replace("(Clone)", "").Trim();
                //newItem.name.Replace("(Clone)(Clone)", "").Trim();


                //itemToSpawn = currentInventory[0];
                //Debug.Log(itemToSpawn.transform.name);
                //currentInventory.Remove(itemToSpawn);

                //spawns the item
                newItem.transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                newItem.transform.rotation = Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z));
                //var newItem = Instantiate(itemToSpawn, new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f),
                //Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z)));

                //moves the item to where the spawn point is, using the offest of where the spawn point originally was
                /*var newItemPos = newItem.transform.position;
                var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                var spawnPointPos = newItem.transform.GetChild(0).transform.position + new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
                spawnPointPos = spawnPointPosOriginal;
                newItem.transform.GetChild(0).transform.localPosition = newItem.GetComponent<CardManager>().spawnPointOriginalPosition;*/
                newItem.transform.position = inventorySlot.transform.position;
                var newItemPos = newItem.transform.position;
                var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                var spawnPointPos = newItem.transform.GetChild(0).transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z - 5);
                //item.transform.position = newItemPos;
                spawnPointPos = spawnPointPosOriginal;
                newItem.transform.GetChild(0).transform.localPosition = newItem.GetComponent<CardManager>().spawnPointOriginalPosition;



                findInInvArray(inventorySlot);
                var xCounter = 0;
                var yCounter = 0;
                //sets the inventory slots it needs to based off the array on its card script
                foreach (var slotToEquip in newItem.GetComponent<CardManager>().slotsToEquipStrings)
                {
                    if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                    {
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                        Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = newItem;
                        newItem.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);
                        //itemsSpawned.Add(newItem);
                        //currentInventory.Remove(itemToSpawn);

                    }

                    //if slot is out of range but doesnt need to be equipped, continue
                    else if ((arrayXPos + xCounter >= 6 && !slotToEquip)
                        || (arrayYPos + yCounter >= 3 && !slotToEquip))
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is out of range but needs to be equipped, unequip and delete it
                    else if ((arrayXPos + xCounter >= 6 && slotToEquip)
                        || (arrayYPos + yCounter >= 3 && slotToEquip))
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        Destroy(newItem);
                        break;
                    }

                    //if slot is already occupied but item doesnt need it to equip here, continue
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && !slotToEquip)
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is already occupied and item needs the slot, unequip and delete it
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && slotToEquip)
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        itemDeleted = true;
                        Destroy(newItem);
                        break;
                    }

                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }

                }
                // if the item was placed successfully, removes it from inventory pool and adds to itemSpawns pool
                if (itemDeleted == false)
                {
                    //Debug.Log("item placed");
                    itemCantBePlaced = false;
                    //currentInventory.Remove(itemToSpawn);
                    itemsSpawned.Add(newItem);
                    break;
                }


            }
        }
        //if the item iterated through the entire grid and couldnt be placed, removes it from inventory
        if (itemCantBePlaced == true)
        {
            //currentInventory.Remove(itemToSpawn);
        }
    }

    public void moveItemInInventory(GameObject item, GameObject inventoryClicked)
    {
        bool itemCanBePlaced = true;
        foreach (var inventorySlot in Inventory)
        {

            if (inventorySlot.gameObject == inventoryClicked && inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped == false /*&& currentInventory.Count > 0*/)
            {
                findInInvArray(inventorySlot);
                var xCounter = 0;
                var yCounter = 0;
                //sets the inventory slots it needs to based off the array on its card script
                foreach (var slotToEquip in item.GetComponent<CardManager>().slotsToEquipStrings)
                {
                    //if slot is in the grid, and empty
                    if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                    {
                        //Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                        //Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = newItem;
                        //newItem.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);
                        //itemsSpawned.Add(newItem);
                        //currentInventory.Remove(itemToSpawn);

                    }

                    //if slot is out of range but doesnt need to be equipped, continue
                    else if ((arrayXPos + xCounter >= 6 && !slotToEquip)
                        || (arrayYPos + yCounter >= 3 && !slotToEquip))
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is out of range but needs to be equipped, unequip and delete it
                    else if ((arrayXPos + xCounter >= 6 && slotToEquip)
                        || (arrayYPos + yCounter >= 3 && slotToEquip))
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in item.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        //itemDeleted = true;
                        itemCanBePlaced = false;
                        //Destroy(newItem);
                        break;
                    }

                    //if slot is already occupied but item doesnt need it to equip here, continue
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && !slotToEquip)
                    {
                        xCounter += 1;

                        if (xCounter == 3)
                        {
                            xCounter = 0;
                            yCounter += 1;
                        }
                        continue;
                    }

                    // if slot is already occupied and item needs the slot, unequip and delete it
                    else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && slotToEquip)
                    {
                        //unequips all slots already equipped before deleting it if the item cannot fit
                        foreach (var slotToUnequip in item.GetComponent<CardManager>().equippedSlots)
                        {
                            if (slotToUnequip != null)
                            {
                                slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                                slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                            }
                        }
                        //itemDeleted = true;
                        //Destroy(newItem);
                        itemCanBePlaced = false;
                        break;
                    }

                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }
                }
            }
            else if (inventorySlot.gameObject == inventoryClicked && inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped == true /*&& currentInventory.Count > 0*/)
            {
                itemCanBePlaced = false;
            }
                //break;

      
        }
        if (itemCanBePlaced)
        {
            //removes original inventory location slots from being equipped by item
            foreach (var inventorySlots in Inventory)
            {
                if (inventorySlots.GetComponent<InventorySlotManager>().equippedItem == item)
                {
                    inventorySlots.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                    inventorySlots.GetComponent<InventorySlotManager>().equippedItem = null;
                }
            }
            //removes item from equipped item slots
            foreach (var itemSlot in inventoryManager.InventoryItems)
            {
                if (itemSlot.Value != null)
                {
                    inventoryManager.equippedSlots.Add(itemSlot.Key);
                }
            }

            //uses equipedItems list to find each item and clear it
            foreach (var slot in inventoryManager.equippedSlots)
            {
                inventoryManager.InventoryItems[slot] = null;
            }
            inventoryManager.equippedSlots.Clear();
            findInInvArray(inventoryClicked);
            var xCounter = 0;
            var yCounter = 0;
            foreach (var slotToEquip in item.GetComponent<CardManager>().slotsToEquipStrings)
            {
                if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                {
                    Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                    Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = item;
                    item.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);


                    //itemsSpawned.Add(newItem);
                    //currentInventory.Remove(itemToSpawn);

                }
                xCounter += 1;

                if (xCounter == 3)
                {
                    xCounter = 0;
                    yCounter += 1;
                }
            }
            item.transform.position = inventoryClicked.transform.position;
            var newItemPos = item.transform.position;
            var spawnPointPosOriginal = item.transform.GetChild(0).transform.position;
            var spawnPointPos = item.transform.GetChild(0).transform.position = new Vector3(inventoryClicked.transform.position.x, inventoryClicked.transform.position.y, inventoryClicked.transform.position.z - 1f);
            spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
            item.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z - 5);
            //item.transform.position = newItemPos;
            spawnPointPos = spawnPointPosOriginal;
            item.transform.GetChild(0).transform.localPosition = item.GetComponent<CardManager>().spawnPointOriginalPosition;

            item.GetComponent<CardManager>().currPos = item.transform.position;
            item.GetComponent<CardManager>().dnPos = item.transform.position;
            item.GetComponent<CardManager>().upPos = item.transform.position + new Vector3(0, .1f, 0);

            /*               
             *  var newItemPos = newItem.transform.position;
                var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
                var spawnPointPos = newItem.transform.GetChild(0).transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
                spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
                newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
                spawnPointPos = spawnPointPosOriginal;*/
        }


            /*///////////////////////////////////////////////////////

            Debug.Log("Detects slot");
            bool itemDeleted = false;
            itemToSpawn = item;
            newItem = item;
            //itemToSpawn = currentInventory[0];
            //Debug.Log(itemToSpawn.transform.name);
            //currentInventory.Remove(itemToSpawn);

            //spawns the item
            //newItem.transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
            //newItem.transform.rotation = Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z));
            //var newItem = Instantiate(itemToSpawn, new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f),
            //Quaternion.Euler(new Vector3(itemToSpawn.transform.rotation.x, itemToSpawn.transform.rotation.y, itemToSpawn.transform.rotation.z)));

            //moves the item to where the spawn point is, using the offest of where the spawn point originally was
            var newItemPos = newItem.transform.position;
            var spawnPointPosOriginal = newItem.transform.GetChild(0).transform.position;
            var spawnPointPos = newItem.transform.GetChild(0).transform.position = new Vector3(inventorySlot.transform.position.x, inventorySlot.transform.position.y, inventorySlot.transform.position.z - 1f);
            spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
            newItem.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
            spawnPointPos = spawnPointPosOriginal;


            findInInvArray(inventorySlot);
            var xCounter = 0;
            var yCounter = 0;
            //sets the inventory slots it needs to based off the array on its card script
            foreach (var slotToEquip in newItem.GetComponent<CardManager>().slotsToEquipStrings)
            {
                if (slotToEquip == true && arrayXPos + xCounter < 6 && arrayYPos + yCounter < 3 && !Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped)
                {
                    Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped = true;
                    Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().equippedItem = newItem;
                    newItem.GetComponent<CardManager>().equippedSlots.Add(Inventory[arrayXPos + xCounter, arrayYPos + yCounter]);
                    //itemsSpawned.Add(newItem);
                    //currentInventory.Remove(itemToSpawn);

                }

                //if slot is out of range but doesnt need to be equipped, continue
                else if ((arrayXPos + xCounter >= 6 && !slotToEquip)
                    || (arrayYPos + yCounter >= 3 && !slotToEquip))
                {
                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }
                    continue;
                }

                // if slot is out of range but needs to be equipped, unequip and delete it
                else if ((arrayXPos + xCounter >= 6 && slotToEquip)
                    || (arrayYPos + yCounter >= 3 && slotToEquip))
                {
                    //unequips all slots already equipped before deleting it if the item cannot fit
                    foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                    {
                        if (slotToUnequip != null)
                        {
                            slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                            slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                        }
                    }
                    itemDeleted = true;
                    Destroy(newItem);
                    break;
                }

                //if slot is already occupied but item doesnt need it to equip here, continue
                else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && !slotToEquip)
                {
                    xCounter += 1;

                    if (xCounter == 3)
                    {
                        xCounter = 0;
                        yCounter += 1;
                    }
                    continue;
                }

                // if slot is already occupied and item needs the slot, unequip and delete it
                else if (Inventory[arrayXPos + xCounter, arrayYPos + yCounter].GetComponent<InventorySlotManager>().slotIsEquipped && slotToEquip)
                {
                    //unequips all slots already equipped before deleting it if the item cannot fit
                    foreach (var slotToUnequip in newItem.GetComponent<CardManager>().equippedSlots)
                    {
                        if (slotToUnequip != null)
                        {
                            slotToUnequip.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                            slotToUnequip.GetComponent<InventorySlotManager>().equippedItem = null;
                        }
                    }
                    itemDeleted = true;
                    Destroy(newItem);
                    break;
                }

                xCounter += 1;

                if (xCounter == 3)
                {
                    xCounter = 0;
                    yCounter += 1;
                }

            }
            // if the item was placed successfully, removes it from inventory pool and adds to itemSpawns pool
            if (itemDeleted == false)
            {
                //Debug.Log("item placed");
                itemCantBePlaced = false;
                //currentInventory.Remove(itemToSpawn);
                itemsSpawned.Add(newItem);
                break;
            }


        }
        else
        {
            break;
        }
    }
    //if the item iterated through the entire grid and couldnt be placed, removes it from inventory
    if (itemCantBePlaced == true)
    {
        //currentInventory.Remove(itemToSpawn);
    }*/
        }


    public void clearInventoryGrid()
    {
        foreach (var inventorySlot in Inventory)
        {
            if (inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped)
            {
                inventorySlot.GetComponent<InventorySlotManager>().slotIsEquipped = false;
                inventorySlot.GetComponent<InventorySlotManager>().equippedItem = null;
            }
        }
        foreach (var itemSpawned in itemsSpawned)
        {
            Destroy(itemSpawned);
        }
        itemsSpawned.Clear();
        currentInventory.Clear();
    }

    public void SetInventorySlot(GameObject slot, GameObject equipment)
    {
        slot.GetComponent<InventorySlotManager>().slotIsEquipped = true;
        slot.GetComponent<InventorySlotManager>().equippedItem = equipment;

    }

    
    public void checkIfItemFitsInventory()
    {

    }

    public void rotateItem(GameObject item)
    {
        var itemPositionsArray = item.GetComponent<CardManager>().slotsToEquipStrings;
        var tempArray = itemPositionsArray;

        itemPositionsArray[0] = tempArray[7];
        itemPositionsArray[1] = tempArray[4];
        itemPositionsArray[2] = tempArray[1];
        itemPositionsArray[3] = tempArray[8];
        itemPositionsArray[4] = tempArray[5];
        itemPositionsArray[5] = tempArray[2];
        itemPositionsArray[6] = tempArray[9];
        itemPositionsArray[7] = tempArray[6];
        itemPositionsArray[8] = tempArray[3];
    }

    public int[] findInInvArray(GameObject currentSlot)
    {
        arrayXPos = 0;
        arrayYPos = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Inventory[i, j] == currentSlot)
                {
                    arrayXPos = i;
                    arrayYPos = j;
                    int[] arrayPos = new int[2];
                    arrayPos[0] = arrayXPos;
                    arrayPos[1] = arrayYPos;
                    return arrayPos;
                }


            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
