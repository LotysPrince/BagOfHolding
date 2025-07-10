using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPurchasing : MonoBehaviour
{
    private int currentPrice;
    public PlayerManager playerManager;
    public DeckManager deckManager;
    public GameObject itemPurchasing;
    public GameObject displayCard;
    // Start is called before the first frame update
    void Start()
    {
        int.TryParse(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text, out currentPrice);
        playerManager = GameObject.Find("Scripts").GetComponent<PlayerManager>();
        deckManager = GameObject.Find("Scripts").GetComponent<DeckManager>();


    }

    // Update is called once per fram
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (currentPrice <= playerManager.playerGold)
        {
            playerManager.playerGoldChange(-currentPrice);
            deckManager.addCardToLibrary(itemPurchasing);
            Destroy(displayCard);
            Destroy(gameObject);

        }
    }
}
