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

    public GameObject elitePrefab;
    public GameObject eliteSprite;
    private int[] eliteArrayPos = new int[2];

    public GameObject bossPrefab;
    public GameObject bossSprite;
    private int[] bossArrayPos = new int[2];

    public GameObject currentElite;



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
        spawnNumOfEnemies = Random.Range(1, 2);
        for (int i = 0; i <= spawnNumOfEnemies; i++)
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
            while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Elite" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
            {
                currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
            }
            currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Enemy";
            enemySprite = Instantiate(enemyPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = enemySprite;
            enemyArrayPos = mapManager.findInRoomArray(currRoom);
            enemySprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
        }

        currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];

        }
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Elite";
        eliteSprite = Instantiate(elitePrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = eliteSprite;
        eliteArrayPos = mapManager.findInRoomArray(currRoom);
        eliteSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
        currentElite = eliteSprite;
    }



    public void spawnShop()
    {
        currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Elite" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs")
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
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Elite" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Stairs" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop")
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

    public void spawnBoss()
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

                        if (mapManager.spawnedRoomsArray[i, j] != null && mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().currentInhabitant == null)
                        {
                            currRoom = mapManager.spawnedRoomsArray[i, j];
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Boss";
                            bossSprite = Instantiate(bossPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = bossSprite;
                            bossArrayPos[0] = i;
                            bossArrayPos[1] = j;
                            bossSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
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
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Boss";
                            bossSprite = Instantiate(bossPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = bossSprite;
                            bossArrayPos[0] = i;
                            bossArrayPos[1] = j;
                            bossSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
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
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Boss";
                            bossSprite = Instantiate(bossPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = bossSprite;
                            bossArrayPos[0] = i;
                            bossArrayPos[1] = j;
                            bossSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
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
                            mapManager.spawnedRoomsArray[i, j].GetComponent<MapSegmentControl>().roomInhabitedBy = "Boss";
                            bossSprite = Instantiate(bossPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
                            currRoom.GetComponent<MapSegmentControl>().currentInhabitant = bossSprite;
                            bossArrayPos[0] = i;
                            bossArrayPos[1] = j;
                            bossSprite.transform.position = currRoom.transform.position + new Vector3(0, 0, -1);
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

    public void eliteMovement()
    {
        int numExits = 0;
        var potentialPaths = new List<string>();


        var leftExitOpen = mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().leftExit;
        var rightExitOpen = mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().rightExit;
        var topExitOpen = mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().topExit;
        var bottomExitOpen = mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().bottomExit;
        if (leftExitOpen)
        {
            numExits += 1;
            potentialPaths.Add("left");
        }
        if (rightExitOpen)
        {
            numExits += 1;
            potentialPaths.Add("right");
        }
        if (topExitOpen)
        {
            numExits += 1;
            potentialPaths.Add("up");
        }
        if (bottomExitOpen)
        {
            numExits += 1;
            potentialPaths.Add("down");
        }

        string pathToTake = potentialPaths[Random.Range(0, numExits)];
        Debug.Log(pathToTake);
        if (pathToTake == "left" && mapManager.spawnedRoomsArray[eliteArrayPos[0] - 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant == null)
        {
            eliteSprite.transform.position = eliteSprite.transform.position + new Vector3(-1.75f, 0, 0);
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0] - 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = eliteSprite;
            mapManager.spawnedRoomsArray[eliteArrayPos[0] - 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = "Elite";
            eliteArrayPos[0] = eliteArrayPos[0] - 1;
            eliteArrayPos[1] = eliteArrayPos[1];
        }
        if (pathToTake == "right" && mapManager.spawnedRoomsArray[eliteArrayPos[0] + 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant == null)
        {
            eliteSprite.transform.position = eliteSprite.transform.position + new Vector3(1.75f, 0, 0);
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0] + 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = eliteSprite;
            mapManager.spawnedRoomsArray[eliteArrayPos[0] + 1, eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = "Elite";
            eliteArrayPos[0] = eliteArrayPos[0] + 1;
            eliteArrayPos[1] = eliteArrayPos[1];
        }
        if (pathToTake == "up" && mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] -1].GetComponent<MapSegmentControl>().currentInhabitant == null)
        {
            eliteSprite.transform.position = eliteSprite.transform.position + new Vector3(0, 1.75f, 0);
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] - 1].GetComponent<MapSegmentControl>().currentInhabitant = eliteSprite;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] - 1].GetComponent<MapSegmentControl>().roomInhabitedBy = "Elite";
            eliteArrayPos[0] = eliteArrayPos[0];
            eliteArrayPos[1] = eliteArrayPos[1] - 1;
        }
        if (pathToTake == "down" && mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] + 1].GetComponent<MapSegmentControl>().currentInhabitant == null)
        {
            eliteSprite.transform.position = eliteSprite.transform.position + new Vector3(0, -1.75f, 0);
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().currentInhabitant = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1]].GetComponent<MapSegmentControl>().roomInhabitedBy = null;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] + 1].GetComponent<MapSegmentControl>().currentInhabitant = eliteSprite;
            mapManager.spawnedRoomsArray[eliteArrayPos[0], eliteArrayPos[1] + 1].GetComponent<MapSegmentControl>().roomInhabitedBy = "Elite";
            eliteArrayPos[0] = eliteArrayPos[0];
            eliteArrayPos[1] = eliteArrayPos[1] + 1;
        }
    }
}
