using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGenerator : MonoBehaviour
{

    public GameObject startingSpawnPoint;
    public GameObject gridPrefab;
    public GameObject[] shopStockPool;
    private GameObject[] currentShopItems;

    private float xCounter;
    private float yCounter;
    // Start is called before the first frame update
    void Start()
    {

        var itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x, startingSpawnPoint.transform.position.y -2.5f, -5), Quaternion.identity);
        var itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x , startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        //spawns the grid display behind the item
        var gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {
            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);
            }
            xCounter += 1.1f;


        }

        Destroy(itemToSpawn.GetComponent<CardManager>());

////////////////////////////////////////////////////////////////////////////////////////////////////////
        itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y -2.5f, -5), Quaternion.identity); 
        itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {

            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);

            }
            xCounter += 1.1f;

        }

        Destroy(itemToSpawn.GetComponent<CardManager>());
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
        itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y -2.5f, -5), Quaternion.identity);
        itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {

            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);

            }
            xCounter += 1.1f;

        }

        Destroy(itemToSpawn.GetComponent<CardManager>());
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x, startingSpawnPoint.transform.position.y - 7.5f, -5), Quaternion.identity);
        itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x, startingSpawnPoint.transform.position.y -5f, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {

            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);

            }
            xCounter += 1.1f;

        }

        Destroy(itemToSpawn.GetComponent<CardManager>());
        ///////////////////////////////////////////////////////////////////////////////////////
        itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y - 7.5f, -5), Quaternion.identity);
        itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y - 5, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {

            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);

            }
            xCounter += 1.1f;

        }

        Destroy(itemToSpawn.GetComponent<CardManager>());
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        itemToSpawn = Instantiate(shopStockPool[Random.Range(0, shopStockPool.Length)], new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y - 7.5f, -5), Quaternion.identity);
        itemInspectionCard = itemToSpawn.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y -5, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        gridSpawnPoint = itemToSpawn.transform.GetChild(0);
        xCounter = 0;
        yCounter = 0;
        foreach (var slot in itemToSpawn.GetComponent<CardManager>().slotsToEquipStrings)
        {

            if (xCounter > 3f)
            {
                xCounter = 0;
                yCounter += 1.1f;
            }

            if (slot == true)
            {
                var newInvSlotDisplay = Instantiate(gridPrefab, new Vector3(gridSpawnPoint.transform.position.x + xCounter, gridSpawnPoint.transform.position.y - yCounter, -4), Quaternion.identity);
                newInvSlotDisplay.transform.SetParent(itemToSpawn.transform);

            }
            xCounter += 1.1f;

        }

        Destroy(itemToSpawn.GetComponent<CardManager>());

    }


    public void RestockShop()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
