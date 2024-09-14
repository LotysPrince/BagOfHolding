using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> currentDeck = new List<GameObject>();
    public List<GameObject> currentLibrary = new List<GameObject>();
    public List<GameObject> currentGraveyard = new List<GameObject>();
    public List<GameObject> currentHand = new List<GameObject>();

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
        drawCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addMercenaryStarterCards()
    {
        addCardToLibrary(spear);
        addCardToLibrary(spear);
        addCardToLibrary(hiddenDagger);
        addCardToLibrary(hiddenDagger);
        addCardToLibrary(helmet);
        addCardToLibrary(helmet);
        addCardToLibrary(sacrificalDagger);
        addCardToLibrary(sacrificalDagger);
        addCardToLibrary(sacrificalDagger);
        addCardToLibrary(sacrificalDagger);
        addCardToLibrary(sacrificalDagger);
        addCardToLibrary(sacrificalDagger);


        /*addCardToLibrary(Saber);
        addCardToLibrary(Saber);
        addCardToLibrary(Saber);
        addCardToLibrary(Saber);
        addCardToLibrary(HealingPotion);
        addCardToLibrary(HealingPotion);
        addCardToLibrary(HealingPotion);
        addCardToLibrary(HealingPotion);
        addCardToLibrary(SwiftCape);
        addCardToLibrary(SwiftCape);
        addCardToLibrary(BladedBoots);
        addCardToLibrary(CloakOfBlood);
        addCardToLibrary(CrimsonLily);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);
        addCardToLibrary(HelmOfCerberus);*/


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
                //currentGraveyard.Clear();
            }
            //var cardObject = Instantiate(drawnCard, new Vector3(xCounter, -4.75f, zCounter), Quaternion.identity);
            //xCounter += 9.25f / handSize;
            //zCounter -= 0.1f;
            currentHand.Add(drawnCard);
        }
        gridGenerator.currentInventory = currentHand;
        gridGenerator.GenerateInventory();
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
            Destroy(card);
            currentGraveyard.Add(card);
        }
        currentHand.Clear();
    }
}
