using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGenerator : MonoBehaviour
{

    public GameObject startingSpawnPoint;
    public GameObject gridPrefab;
    public List<GameObject> shopStockPool = new List<GameObject>();
    private List<GameObject> currentShopItems = new List<GameObject>();
    private List<GameObject> currentShopPurchaseButtons = new List<GameObject>();
    private DeckManager deckManager;

    private GameObject purchaseButton;
    public GameObject purchasePrefabObject;

    private float xCounter;
    private float yCounter;
    // Start is called before the first frame update
    void Start()
    {

        deckManager = GameObject.Find("Scripts").GetComponent<DeckManager>();
        shopStockPool = deckManager.obtainableCards;

        


        /*
         * 

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
        
    }*/
    }

    public void GenerateShop()
    {
        GameObject purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";

        var itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);
        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        var itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab;
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);


        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";


        itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);

        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

        itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab; itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);


        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);




        //////////////////////////////////////////////////////////////////////////////////////////////////////


        purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";


        itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);

        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

        itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab; itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);

        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);




        //////////////////////////////////////////////////////////////////////////////////////////////////////


        purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";


        itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);

        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

        itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab; itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x, startingSpawnPoint.transform.position.y - 4.5f, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);


        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);




        //////////////////////////////////////////////////////////////////////////////////////////////////////


        purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";


        itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);

        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

        itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab; itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y - 4.5f, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);


        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);




        //////////////////////////////////////////////////////////////////////////////////////////////////////


        purchase = Instantiate(purchasePrefabObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.Euler(0, 0, 90), GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        purchaseButton = purchase;
        purchaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";


        itemSpawning = shopStockPool[Random.Range(0, shopStockPool.Count)];
        itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);

        //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

        itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab; itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 4, startingSpawnPoint.transform.position.y, -5), Quaternion.identity);
        itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(startingSpawnPoint.transform.position.x + 8, startingSpawnPoint.transform.position.y - 4.5f, -5), Quaternion.identity);
        itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
        itemInspectionCard.SetActive(true);


        purchaseButton.transform.position = itemInspectionCard.transform.position + new Vector3(0, -2f, 0);
        purchaseButton.GetComponent<ItemPurchasing>().itemPurchasing = itemSpawning;
        purchaseButton.GetComponent<ItemPurchasing>().displayCard = itemInspectionCard;
        currentShopItems.Add(itemInspectionCard);
        currentShopPurchaseButtons.Add(purchaseButton);

    }

    public void DeleteShop()
    {
        //List<GameObject> tempList = new List<GameObject>();
        //tempList = currentShopItems;
        foreach (var item in currentShopItems)
        {
            Destroy(item);
        }
        foreach (var item in currentShopPurchaseButtons)
        {
            Destroy(item);
        }
    }
}
