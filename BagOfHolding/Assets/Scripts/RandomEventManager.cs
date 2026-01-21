using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    [TextArea(4, 10)]
    public List<string> possibleEventText = new List<string>();
    public GameObject eventTextBox;
    private string currentEventText;
    private int eventNum;
    public SwitchScreen switchScreenScript;
    public RandomEventManager mainEventManager;
    public MapControls mapControls;
    public DeckManager deckManager;
    public TurnManager turnManager;
    public ShopGenerator shopGenerator;
    public GameObject option1;
    public GameObject option2;
    public GameObject exitButton;
    public string optionChosen;
    private string optionClicked;

    public PlayerManager playerManager;

    public GameObject werewolfPrefab;
    public GameObject dryadPrefab;
    public GameObject dryadRapier;
    //public GameObject[] spawnEventEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //currentEventText = eventTextBox.GetComponent<TMPro.TextMeshPro>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateRandomEvent()
    {
        option1.transform.parent.gameObject.SetActive(true);
        option2.transform.parent.gameObject.SetActive(true);
        exitButton.transform.parent.gameObject.SetActive(false);
        optionChosen = "";
        eventNum = Random.Range(0, possibleEventText.Count);
        currentEventText = possibleEventText[eventNum];
        eventTextBox.GetComponent<TMPro.TextMeshPro>().text = currentEventText;
        setOptions();
    }

    public void setOptions()
    {
        option1.transform.parent.gameObject.SetActive(true);
        option2.transform.parent.gameObject.SetActive(true);
        if (eventNum == 0)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Run away from the source of the howling";
            option2.GetComponent<TMPro.TextMeshPro>().text = "Hide away";
        }
        if (eventNum == 1)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Remove the root";
            option2.GetComponent<TMPro.TextMeshPro>().text = "Continue on your path";
        }
        if (eventNum == 2)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Pray";
            option2.GetComponent<TMPro.TextMeshPro>().text = "Offer blood";
        }
        if (eventNum == 3)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Help it";
            option2.GetComponent<TMPro.TextMeshPro>().text = "Leave it";
        }
        if (eventNum == 4)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Search the camp";
            option2.transform.parent.gameObject.SetActive(false);
        }
        if (eventNum == 5)
        {
            option1.GetComponent<TMPro.TextMeshPro>().text = "Browse his wares";
            option2.GetComponent<TMPro.TextMeshPro>().text = "No thanks";
        }
    }

    public void randomEventResults()
    {
        if (eventNum == 0)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "The creatures heard the sounds of you running, gladly giving chase";
                optionChosen = "1";
            }
            if (optionClicked == "Confirmation2")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "The creatures searched for you for a moment, before getting distracted by another prey and scurrying off";
                optionChosen = "2";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    mapControls.inBattle = true;
                    switchScreenScript.currentScreen = "Battle";
                    switchScreenScript.changeScreen("Battle");
                    deckManager.currentHand.Clear();
                    deckManager.currentDeck.Clear();
                    deckManager.currentGraveyard.Clear();
                    deckManager.createDeckFromLibrary();
                    deckManager.drawCards();
                    GameObject[] spawnEventWolves = { werewolfPrefab,werewolfPrefab,werewolfPrefab};
                    turnManager.spawnSpecificEnemies(spawnEventWolves);
                }
                if (optionChosen == "2")
                {
                    switchScreenScript.returnToMap();
                }
            }

        }
        if (eventNum == 1)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "The root was actually a spear, clearly fashioned from a dryad. You taking it awakens the same dryad that killed the adventurer.";
                optionChosen = "1";
            }
            if (optionClicked == "Confirmation2")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "Deciding not to tempt the same fate that befell the adventurer, you continue along your path.";
                optionChosen = "2";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    deckManager.addCardToLibrary(dryadRapier);
                    mapControls.inBattle = true;
                    switchScreenScript.currentScreen = "Battle";
                    switchScreenScript.changeScreen("Battle");
                    deckManager.currentHand.Clear();
                    deckManager.currentDeck.Clear();
                    deckManager.currentGraveyard.Clear();
                    deckManager.createDeckFromLibrary();
                    deckManager.drawCards();
                    GameObject[] spawnEventWolves = { dryadPrefab };
                    turnManager.spawnSpecificEnemies(spawnEventWolves);
                }
                if (optionChosen == "2")
                {
                    switchScreenScript.returnToMap();
                }
            }
        }
        if (eventNum == 2)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "You kneel before the god, though he is unknown to you, you seem compelled to pay respects. After a moment, a few gold coins rise from the depths of the bloody basin. You take the 8-armed god's blessing and continue on your journey.";
                optionChosen = "1";
            }
            if (optionClicked == "Confirmation2")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "You feel compelled to offer a more devoted pledge to this mysterious god. Whether by your own will or anothers, you slice your palm above the basin, letting the blood join the collection of countless others. When you withdraw your hand, you feel slightly weaker, but your bag feels significantly heavier.";
                optionChosen = "2";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    playerManager.playerGoldChange(15);
                    switchScreenScript.returnToMap();

                }
                if (optionChosen == "2")
                {
                    switchScreenScript.returnToMap();
                    playerManager.playerTakesDamage(3);
                    var randomItem = deckManager.currentDeck[Random.Range(0, deckManager.currentDeck.Count)];
                    deckManager.addCardToLibrary(randomItem); 
                    deckManager.addCardToLibrary(randomItem);
                    deckManager.addCardToLibrary(randomItem);

                }
            }
        }
        if (eventNum == 3)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "With some sheer force, you're able to pry the trap open releasing the wolf's leg as it whines painfully, snapping at you instinctually. It immediately recoils to lick the wound you've freed. As you try to leave, content to let the wolf fend for itself, it limps behind you, desperate to stay by your side. It seems the wolf will sluggishly help you in battle should you need it.";
                optionChosen = "1";
            }
            if (optionClicked == "Confirmation2")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "You decide to leave the wolf, fearing the noise will attract the person who set the trap, or even bring back the wolf's pack that has since left it to die. Though you've avoided danger, you still don't feel right about it.";
                optionChosen = "2";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    playerManager.playerTakesDamage(2);
                    switchScreenScript.returnToMap();

                }
                if (optionChosen == "2")
                {
                    switchScreenScript.returnToMap();


                }
            }
        }
        if (eventNum == 4)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "You search the camp, finding two sets of equipment which still seem viable to use. You store them in your bag.";
                optionChosen = "1";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    var randomItem = deckManager.obtainableCards[Random.Range(0, deckManager.obtainableCards.Count)];
                    deckManager.addCardToLibrary(randomItem);
                    switchScreenScript.returnToMap();
                }

            }
        }
        if (eventNum == 5)
        {

            if (optionClicked == "Confirmation")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "He chuckles roughly, nearly making you regret your decision";
                optionChosen = "1";
            }
            if (optionClicked == "Confirmation2")
            {
                eventTextBox.GetComponent<TMPro.TextMeshPro>().text = "Heheheh. Suit yourself stranger. 'Til we meet again.";
                optionChosen = "2";
            }
            if (optionClicked == "ExitConfirm")
            {
                if (optionChosen == "1")
                {
                    shopGenerator.GenerateShop("3");
                    switchScreenScript.changeScreen("Shop");
                }
                if (optionChosen == "2")
                {
                    switchScreenScript.returnToMap();
                }

            }
        }


        option1.transform.parent.gameObject.SetActive(false);
        option2.transform.parent.gameObject.SetActive(false);
        exitButton.transform.parent.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        mainEventManager.optionClicked = gameObject.transform.name;
        mainEventManager.randomEventResults();
        //switchScreenScript.returnToMap();

    }
}
