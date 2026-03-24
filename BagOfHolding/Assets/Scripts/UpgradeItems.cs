using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItems : MonoBehaviour
{
    public DeckManager deckManager;
    public SwitchScreen switchScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        switchScreen.changeScreen("UpgradeItems");
        /*
        if (gameObject.transform.name == "UpgradeItem")
        {
            UpgradeItem();
        }
        else if (gameObject.transform.name == "CurseItem")
        {
            CurseItem();
        }*/
    }

    private void UpgradeItem()
    {
        var deck = deckManager.currentLibrary;
        var item = deck[Random.Range(0, deck.Count)];
        item.GetComponent<CardManager>().isUpgraded = true;
    }

    private void CurseItem()
    {
        var deck = deckManager.currentLibrary;
        var item = deck[Random.Range(0, deck.Count)];
        item.GetComponent<CardManager>().curseOfBinding = true;
    }
}
