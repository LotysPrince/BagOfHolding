using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    public List<string> possibleEventText = new List<string>();
    public GameObject eventTextBox;
    private string currentEventText;
    private int eventNum;
    public SwitchScreen switchScreenScript;
    public RandomEventManager mainEventManager;

    public PlayerManager playerManager;

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
        eventNum = Random.Range(0, possibleEventText.Count);
        currentEventText = possibleEventText[eventNum];
        eventTextBox.GetComponent<TMPro.TextMeshPro>().text = currentEventText;
    }

    public void randomEventResults()
    {
        if (eventNum == 0)
        {

        }
        else if (eventNum == 1)
        {

        }
        else if (eventNum == 2)
        {
            playerManager.playerTakesDamage(2);
        }
        else if (eventNum == 3)
        {
            playerManager.playerHealsDamage(5);
        }
    }

    private void OnMouseDown()
    {
        mainEventManager.randomEventResults();
        switchScreenScript.returnToMap();

    }
}
