using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMapBehavior : MonoBehaviour
{

    public MapGenerator mapManager;
    public GameObject playerSprite;
    public GameObject enemyPrefab;
    public GameObject enemySprite;
    private GameObject currRoom;
    private int[] enemyArrayPos;
    private int spawnNumOfEnemies;

    public GameObject shopPrefab;
    public GameObject shopSprite;
    private int[] shopArrayPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies();
        spawnShop();
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
            while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Shop")
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
        while (currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Player" || currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy == "Enemy")
        {
            currRoom = mapManager.spawnedRooms[Random.Range(0, mapManager.spawnedRooms.Count)];

        }
        currRoom.GetComponent<MapSegmentControl>().roomInhabitedBy = "Shop";
        shopSprite = Instantiate(shopPrefab, new Vector3(currRoom.transform.position.x, currRoom.transform.position.y, -1), Quaternion.identity);
        currRoom.GetComponent<MapSegmentControl>().currentInhabitant = shopSprite;
        shopArrayPos = mapManager.findInRoomArray(currRoom);
        shopSprite.transform.position = currRoom.transform.position + new Vector3(-.2f, 0, -1);

    }
}
