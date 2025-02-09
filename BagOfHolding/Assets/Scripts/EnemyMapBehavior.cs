using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMapBehavior : MonoBehaviour
{

    public MapGenerator mapManager;
    public MapControls mapControls;
    public GameObject playerSprite;
    public GameObject enemyPrefab;
    public GameObject enemySprite;
    private GameObject currRoom;
    private int[] enemyArrayPos = new int[2];
    private int spawnNumOfEnemies;

    public GameObject shopPrefab;
    public GameObject shopSprite;
    private int[] shopArrayPos = new int[2];


    public GameObject stairsPrefab;
    public GameObject stairsSprite;
    private int[] stairsArrayPos = new int[2];

    public GameObject eventPrefab;
    public GameObject eventSprite;
    private int[] eventArrayPos = new int[2];




    // Start is called before the first frame update
    void Start()
    {
        //spawnEnemies();
        //spawnShop();



    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies()
    {
        spawnNumOfEnemies = Random.Range(1, 3);
        for (int i = 0; i <= spawnNumOfEnemies; i++)
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
            while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
            {
                currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
            }
            currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Enemy";
            enemySprite = Instantiate(enemyPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = enemySprite;
            enemyArrayPos = mapManager.findInRoomArray(currRoom);
            enemySprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
        }
    }

    public void spawnShop()
    {
        currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];

        }
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Shop";
        shopSprite = Instantiate(shopPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = shopSprite;
        shopArrayPos = mapManager.findInRoomArray(currRoom);
        shopSprite.transform.position = currRoom.transform.position + new Vector3(-.2f, 0, -1);

    }

    public void spawnEvent()
    {
        currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop")
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];

        }
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Event";
        eventSprite = Instantiate(eventPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = eventSprite;
        eventArrayPos = mapManager.findInRoomArray(currRoom);
        eventSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);

    }

    public void spawnStairs()
    {
        currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
        var playerPos = mapControls.playerArrayPos;

        if (playerPos[0] > 4)
        {
            if (playerPos[1] > 2)
            {

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        //Debug.Log("i: " + i + " j: " + j);

                        if (mapManager.spawnedRoomsArray[i,j] != null && mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().currentInhabitant == null)
                        {
                            currRoom = mapManager.spawnedRoomsArray[i, j];
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Stairs";
                            stairsSprite = Instantiate(stairsPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = stairsSprite;
                            stairsArrayPos[0] = i;
                            stairsArrayPos[1] = j;
                            stairsSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
                            return;
                            
                        }


                    }
                }
            }
            else
            {
                //stairsYPos = 2;
                //yIncrementor = -1;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 4; j > 0; j--)
                    {
                        //Debug.Log("i: " + i + " j: " + j);

                        if (mapManager.spawnedRoomsArray[i, j] != null && mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().currentInhabitant == null)
                        {
                            currRoom = mapManager.spawnedRoomsArray[i, j];
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Stairs";
                            stairsSprite = Instantiate(stairsPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = stairsSprite;
                            stairsArrayPos[0] = i;
                            stairsArrayPos[1] = j;
                            stairsSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
                            return;
                        }


                    }
                }
            }
        }
        else
        {
            //stairsXPos = 4;
            //xIncrementor = -1;
            if (playerPos[1] > 2)
            {
                //stairsYPos = -2;
                //yIncrementor = 1;
                for (int i = 8; i > 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        //Debug.Log("i: " + i + " j: " + j);
                        if (mapManager.spawnedRoomsArray[i, j] != null && mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().currentInhabitant == null)
                        {
                            currRoom = mapManager.spawnedRoomsArray[i, j];
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Stairs";
                            stairsSprite = Instantiate(stairsPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = stairsSprite;
                            stairsArrayPos[0] = i;
                            stairsArrayPos[1] = j;
                            stairsSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
                            return;
                        }


                    }
                }
            }
            else
            {
               // stairsYPos = 2;
                //yIncrementor = -1;

                for (int i = 8; i > 0; i--)
                {
                    for (int j = 4; j > 0; j--)
                    {

                        if (mapManager.spawnedRoomsArray[i, j] != null && mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().currentInhabitant == null)
                        {
                            currRoom = mapManager.spawnedRoomsArray[i, j];
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Stairs";
                            stairsSprite = Instantiate(stairsPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = stairsSprite;
                            stairsArrayPos[0] = i;
                            stairsArrayPos[1] = j;
                            stairsSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
                            return;
                        }


                    }
                }
            }
        }

        

        /*while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop")
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];

        }
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Shop";
        shopSprite = Instantiate(shopPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = shopSprite;
        shopArrayPos = mapManager.findInRoomArray(currRoom);
        shopSprite.transform.position = currRoom.transform.position + new Vector3(-.2f, 0, -1);*/

    }
}
