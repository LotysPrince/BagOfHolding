using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{

    public string currentScreen;
    public DeckManager deckManager;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        currentScreen = "Map";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScreen(string newScreen)
    {
        foreach (GameObject card in deckManager.currentLibrary)
        {
            card.GetComponent<CardManager>().inspectionCardPrefab.SetActive(true);
            card.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = false;


        }
        if (newScreen == "Map")
        {
            foreach (GameObject card in deckManager.currentLibrary)
            {
                card.GetComponent<CardManager>().inspectionCardPrefab.SetActive(false);
                card.GetComponent<CardManager>().inspectionCardPrefab.GetComponent<InspectionCardSpawning>().stopFollowMouse = true;

            }
            cam.transform.position = new Vector3(0, -17f, -30);
            currentScreen = "Map";
        }
        else if (newScreen == "Battle")
        {
            cam.transform.position = new Vector3(0, 0f, -30);
            currentScreen = "Battle";
        }
        else if (newScreen == "Shop")
        {
            cam.transform.position = new Vector3(0, -34f, -30);
            currentScreen = "Shop";
        }
        else if (newScreen == "Event")
        {
            cam.transform.position = new Vector3(0, -51f, -30);
            currentScreen = "Event";
        }
    }

    public void returnToMap()
    {
        if (currentScreen == "Map")
        {
            cam.transform.position = new Vector3(0, 0f, -30);
            currentScreen = "Battle";
        }
        else if (currentScreen == "Battle")
        {
            cam.transform.position = new Vector3(0, -17f, -30);
            currentScreen = "Map";
        }
        else if (currentScreen == "Shop")
        {
            cam.transform.position = new Vector3(0, -17f, -30);
            currentScreen = "Map";
        }
        else if (currentScreen == "Event")
        {
            cam.transform.position = new Vector3(0, -17f, -30);
            currentScreen = "Map";
        }
    }
}
