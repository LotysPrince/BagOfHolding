using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControls : MonoBehaviour
{

    public MapGenerator mapManager;
    public TurnManager turnManager;
    public DeckManager deckManager;
    public RandomEventManager eventManager;
    public GameObject playerSprite;
    public GameObject currRoom;
    public int[] playerArrayPos = new int[2];
    public SwitchScreen switchScreenScript;
    public bool inBattle;
    // Start is called before the first frame update
    void Awake()
    {
        inBattle = false;
        //spawnPlayer();
    }



    // Update is called once per frame
    void Update()
    {
        if (!inBattle)
        {
            if (Input.GetKeyDown("up"))
            {
                if (currRoom.GetComponent<MapSegmentControl>().topExit)
                {
                    playerSprite.transform.position = playerSprite.transform.position + new Vector3(0, 2, 0);
                    playerArrayPos[1] = playerArrayPos[1] - 1;
                    currRoom = mapManager.spawnedRoomsArray[playerArrayPos[0], playerArrayPos[1]];
                }
            }

            if (Input.GetKeyDown("down"))
            {
                if (currRoom.GetComponent<MapSegmentControl>().bottomExit)
                {
                    playerSprite.transform.position = playerSprite.transform.position + new Vector3(0, -2, 0);
                    playerArrayPos[1] = playerArrayPos[1] + 1;
                    currRoom = mapManager.spawnedRoomsArray[playerArrayPos[0], playerArrayPos[1]];
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (currRoom.GetComponent<MapSegmentControl>().leftExit)
                {
                    playerSprite.transform.position = playerSprite.transform.position + new Vector3(-2, 0, 0);
                    playerArrayPos[0] = playerArrayPos[0] - 1;
                    currRoom = mapManager.spawnedRoomsArray[playerArrayPos[0], playerArrayPos[1]];
                }
            }

            if (Input.GetKeyDown("right"))
            {
                if (currRoom.GetComponent<MapSegmentControl>().rightExit)
                {
                    playerSprite.transform.position = playerSprite.transform.position + new Vector3(2, 0, 0);
                    playerArrayPos[0] = playerArrayPos[0] + 1;
                    currRoom = mapManager.spawnedRoomsArray[playerArrayPos[0], playerArrayPos[1]];
                }

            }

            if (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy")
            {
                inBattle = true;
                switchScreenScript.currentScreen = "Battle";
                switchScreenScript.changeScreen("Battle");
                Destroy(currRoom.GetComponent<MapSegmentControl>().currentInhabitant);
                currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Player";
                deckManager.currentHand.Clear();
                deckManager.currentDeck.Clear();
                deckManager.createDeckFromLibrary();
                deckManager.drawCards();
                turnManager.spawnEnemies();
            }
            else if(currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop")
            {
                switchScreenScript.currentScreen = "Shop";
                switchScreenScript.changeScreen("Shop");
                Destroy(currRoom.GetComponent<MapSegmentControl>().currentInhabitant);
                currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Player";
            }
            else if (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
            {
                mapManager.deleteMap();
            }
            else if (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Event")
            {
                switchScreenScript.currentScreen = "Event";
                switchScreenScript.changeScreen("Event");
                eventManager.generateRandomEvent();
                Destroy(currRoom.GetComponent<MapSegmentControl>().currentInhabitant);
                currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Player";
            }
        }
    }

    public void spawnPlayer()
    {
        currRoom = mapManager.spawnedRooms[mapManager.spawnedRooms.Count - 1];
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Player";
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = playerSprite;
        playerArrayPos = mapManager.findInRoomArray(currRoom);
        playerSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
    }
}
