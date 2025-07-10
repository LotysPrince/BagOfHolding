using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> currentDeck = new List<GameObject>();
    public List<GameObject> currentLibrary = new List<GameObject>();
    public List<GameObject> currentGraveyard = new List<GameObject>();
    public List<GameObject> currentHand = new List<GameObject>();
    public List<GameObject> obtainableCards = new List<GameObject>();

    public GameObject pouchIconText;
    public GameObject graveyardIconText;

    public int handSize;
    public InventoryGridGenerator gridGenerator;

    // public GameObject Saber;
    //public GameObject SwiftCape;
    //public GameObject HealingPotion;
    //public GameObject CloakOfBlood;
    //public GameObject BladedBoots;
    //public GameObject CrimsonLily;
    //public GameObject HelmOfCerberus;
    public GameObject spear;
    public GameObject hiddenDagger;
    public GameObject helmet;
    public GameObject sacrificalDagger;


    // Start is called before the first frame update
    void Start()
    {
        //handSize = 6;
        addMercenaryStarterCards();
        createDeckFromLibrary();
        //drawCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addMercenaryStarterCards()
    {

    }

    public void addCardToLibrary(GameObject newLibraryCard)
    {
        currentLibrary.Add(newLibraryCard);
    }

    public void createDeckFromLibrary()
    {
        //duplicates library into deck
        foreach (var card in currentLibrary)
        {

            currentDeck.Add(card);
        }

        //shuffles the deck
        for (int i = 0; i < currentDeck.Count; i++)
        {
            var temp = currentDeck[i];
            int randomIndex = Random.Range(i, currentDeck.Count);
            currentDeck[i] = currentDeck[randomIndex];
            currentDeck[randomIndex] = temp;
        }
    }

    public void drawCards()
    {
        //float xCounter = -.5f;
        //float zCounter = -2;

        //adds handsize amount of cards to hand
        while (currentHand.Count < handSize)
        {
            var drawnCard = currentDeck[0];
            currentDeck.Remove(drawnCard);
            //if last card of deck is drawn, reshuffles a new one
            if (currentDeck.Count == 0)
            {
                createDeckFromLibrary();
                currentGraveyard.Clear();
                //currentGraveyard.Clear();
            }
            //var cardObject = Instantiate(drawnCard, new Vector3(xCounter, -4.75f, zCounter), Quaternion.identity);
            //xCounter += 9.25f / handSize;
            //zCounter -= 0.1f;
            
            currentHand.Add(drawnCard);
        }

        gridGenerator.currentInventory = new List<GameObject>(currentHand);
        gridGenerator.GenerateInventory();
        pouchIconText.GetComponent<TextMeshProUGUI>().text = currentDeck.Count.ToString();
        graveyardIconText.GetComponent<TextMeshProUGUI>().text = currentGraveyard.Count.ToString();
    }
    public void drawCardsFromGraveyard(int drawAmount)
    {
        int cardsDrawn = 0;
        while (cardsDrawn < drawAmount)
        {
            if (currentGraveyard.Count != 0)
            {
                var drawnCard = currentGraveyard[Random.Range(0, currentGraveyard.Count)];
                currentGraveyard.Remove(drawnCard);
                currentHand.Add(drawnCard);
                gridGenerator.addNewItemToInventory(drawnCard);
                cardsDrawn += 1;
            }
            else if (currentGraveyard.Count == 0)
            {
                break;
            }
        }
    }

    public void updateHandPosition()
    {
        float xCounter = -.5f;
        float zCounter = -2;
        int cardsLeftInHand = 0;
        foreach (var card in currentHand)
        {
            if (card.GetComponent<CardManager>().itemEquipped == false)
            {
                cardsLeftInHand += 1;
            }
        }
        foreach (var card in currentHand)
        {
            if (card.GetComponent<CardManager>().itemEquipped == false)
            {
                var dnPos = card.GetComponent<CardManager>().dnPos = new Vector3(xCounter, -4.75f, zCounter);
                card.GetComponent<CardManager>().upPos = dnPos + new Vector3(0, 2, -1);
                card.GetComponent<CardManager>().currPos = dnPos;
                xCounter += 9.25f / cardsLeftInHand;
                zCounter -= 0.1f;
            }
        }
    }

    public void emptyHand()
    {
        foreach (var card in currentHand)
        {
            currentGraveyard.Add(card);
        }
        foreach (var card in gridGenerator.itemsSpawned)
        { 
            Destroy(card);
        }
        currentHand.Clear();
        gridGenerator.itemsSpawned.Clear();
    }
}
