using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemScreenGen : MonoBehaviour
{

    public GameObject spawnPointObject;
    public DeckManager deckManager;
    public List<GameObject> upgradeCardsList = new List<GameObject>();
    public UpgradeItemCardScript cardScript;
    public UpgradeItemScreenGen upgradeScreen;
    public GameObject currentDisplayedCard;
    private float xIncrement = 0;
    private float yIncrement = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenerateUpgradeList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateUpgradeList()
    {
        foreach (var card in deckManager.currentLibrary)
        {
            
            var itemSpawning = card;
            upgradeCardsList.Add(itemSpawning);
            itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);
            //itemSpawning.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
            var itemInspectionCard = itemSpawning.GetComponent<CardManager>().inspectionCardPrefab;

                itemInspectionCard = Instantiate(itemInspectionCard, new Vector3(spawnPointObject.transform.position.x + xIncrement, spawnPointObject.transform.position.y + yIncrement, -5), Quaternion.identity);
                itemInspectionCard.GetComponent<BoxCollider2D>().enabled = true;
            if (itemInspectionCard.GetComponent<UpgradeItemCardScript>() == null)
            {
                itemInspectionCard.AddComponent<UpgradeItemCardScript>();
                itemInspectionCard.GetComponent<UpgradeItemCardScript>().rootCard = card;
                itemInspectionCard.GetComponent<UpgradeItemCardScript>().upgradeScreen = upgradeScreen;
            }
                itemInspectionCard.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;
                itemInspectionCard.SetActive(true);
            
            xIncrement += 2.5f;
            if (xIncrement == 10)
            {
                xIncrement = 0;
                yIncrement += -4f;
            }
        }
    }
}
