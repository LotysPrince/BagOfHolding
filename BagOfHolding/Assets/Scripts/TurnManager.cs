using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int maxTargets = 1;
    public float damage = 0f;
    public float damageMult = 1f;
    public int healing = 0;
    public int numAttacks = 1;
    public int inflictBleed = 0;

    public List<GameObject> spawnableEnemies = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<GameObject> targettedEnemies = new List<GameObject>();
    public int numberEnemies = 1;

    public InventoryManager inventoryManager;
    public DeckManager deckManager;
    public PlayerManager playerManager;
    public SwitchScreen switchScreen;
    public MapControls mapControls;
    public InventoryGridGenerator inventoryGridGenScript;

    private GameObject helm1;
    private GameObject helm2;

    private bool temp;


    //status effects
    private int nextTurnCritical;
    private int currentCritical;

    
    // Start is called before the first frame update
    void Start()
    {
        //numberEnemies = Random.Range(0, 3);
        numberEnemies = 2;
        spawnEnemies();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemies()
    {
        var xCounter = 1f;
        var yCounter = 2f;
        for (int i = 0; i <= numberEnemies; i++)
        {

            var spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Count)], new Vector3(xCounter, yCounter, -1), Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy);
            xCounter += 3;
            if(yCounter == 2f)
            {
                yCounter = 0;
            }
            else if(yCounter == 0)
            {
                yCounter = 2f;
            }
        }

        targettedEnemies.Add(spawnedEnemies[0]);
        spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void endTurn()
    {
        deckManager.emptyHand();
        inventoryGridGenScript.clearInventoryGrid();

        foreach(var item in inventoryManager.InventoryItems)
        {
            if (item.Value != null)
            {
                processCards(item.Value);
            }
        }

        inventoryManager.clearInventory();
        processHealing();
        processDamage();
        initiateEnemyTurn();
        
    }

    public void preProcessCards(GameObject card)
    {
        if (card.name == "SwiftCape(Clone)")
        {
            maxTargets += 1;
        }
        if (card.name == "HelmOfCerberus(Clone)")
        {
            inventoryManager.addInventorySlot(2, "Helmet");
            /*if (helm1 == null)
            {
                helm1 = Instantiate(inventoryManager.helmPrefab, new Vector3(inventoryManager.Helmet.transform.position.x + 1.5f, inventoryManager.Helmet.transform.position.y, inventoryManager.Helmet.transform.position.z), Quaternion.identity);
                helm2 = Instantiate(inventoryManager.helmPrefab, new Vector3(inventoryManager.Helmet.transform.position.x - 1.5f, inventoryManager.Helmet.transform.position.y, inventoryManager.Helmet.transform.position.z), Quaternion.identity);
                inventoryManager.InventoryItems.Add(helm1, null);
                inventoryManager.InventoryItems.Add(helm2, null);
            }*/
        }
    }

    public void preProcessRemove(GameObject card)
    {
        if (card.name == "SwiftCape(Clone)")
        {
            maxTargets -= 1;
            if (targettedEnemies.Count > maxTargets)
            {
                var oldSelected = targettedEnemies[targettedEnemies.Count - 1];
                oldSelected.GetComponent<EnemyManager>().isTargetted = false;
                targettedEnemies.Remove(oldSelected);
                oldSelected.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (card.name == "HelmOfCerberus(Clone)")
        {
            inventoryManager.removeInventorySlot(2, "Helmet");
            //unequips slot before deleting it
               /* if (inventoryManager.InventoryItems[helm1] != null)
                {
                    inventoryManager.unsetInventory(inventoryManager.InventoryItems[helm1]);
                }
                inventoryManager.InventoryItems.Remove(helm1);
                if (inventoryManager.InventoryItems[helm2] != null)
                {
                    inventoryManager.unsetInventory(inventoryManager.InventoryItems[helm2]);
                }
                inventoryManager.InventoryItems.Remove(helm2);
                temp = true;*/
            

            //helm1 = Instantiate(inventoryManager.helmPrefab, new Vector3(inventoryManager.Helmet.transform.position.x + 1.5f, inventoryManager.Helmet.transform.position.y, inventoryManager.Helmet.transform.position.z), Quaternion.identity);
            //helm2 = Instantiate(inventoryManager.helmPrefab, new Vector3(inventoryManager.Helmet.transform.position.x - 1.5f, inventoryManager.Helmet.transform.position.y, inventoryManager.Helmet.transform.position.z), Quaternion.identity);
            //inventoryManager.InventoryItems.Add(helm1, null);
            //inventoryManager.InventoryItems.Add(helm2, null);
        }


    }
    private void processCards(GameObject card)
    {
        if (card.name == "Saber(Clone)")
        {
            damage += 2;
            //inflictBleed += 2;
        }
        if (card.name == "HealingPotion(Clone)")
        {
            healing += 2;
        }
        if (card.name == "BladedBoots(Clone)")
        {
            numAttacks += 1;
        }
        if (card.name == "CrimsonLily(Clone)")
        {
            inflictBleed += 2;
        }
        if (card.name == "CloakOfBlood(Clone)")
        {
            damageMult += 0.3f;
        }
        if (card.name == "HelmOfCerberus(Clone)")
        {
            damage += 5;
        }
        if (card.name == "RitualDagger(Clone)")
        {
            playerManager.playerTakesDamage(5);
            damage += 5;
        }
        if (card.name == "WingedDaggers(Clone)")
        {
            numAttacks += 5;
            damage = 1;
        }
        if (card.name == "BirdskullWhistle(Clone)")
        {
            postProcessCards(card);
        }
    }

    // processes cards that have an effect on next turn

    private void postProcessCards(GameObject card)
    {
        if (card.name == "BirdskullWhistle(Clone)")
        {
            nextTurnCritical = 3;
        }
    }


    private void processHealing()
    {
        playerManager.playerHealsDamage(healing);
        healing = 0;
    }

    private void processDamage()
    {
        //player attacks multiple times
        for (int i = 1; i <= numAttacks; i++)
        {
            //if player hits multiple enemies
            foreach (var enemy in targettedEnemies)
            {
                enemy.GetComponent<EnemyManager>().enemyTakesDamage(Mathf.FloorToInt(damage * currentCritical * damageMult));
                enemy.GetComponent<EnemyManager>().setBleed(inflictBleed);

            }
        }

        damage = 0;
        numAttacks = 1;
        inflictBleed = 0;
        currentCritical = 1;
        damageMult = 1;

        if (nextTurnCritical != 1)
        {
            currentCritical = nextTurnCritical;
            nextTurnCritical = 1;
        }
    }

    private void initiateEnemyTurn()
    {
        if (spawnedEnemies.Count != 0)
        {
            foreach (var enemy in spawnedEnemies)
            {
                enemy.GetComponent<EnemyManager>().enemyDealsDamage();
                enemy.GetComponent<EnemyManager>().setPower();
            }
        }
        //if all enemies are dead, return to map
        else if (spawnedEnemies.Count == 0)
        {
            switchScreen.currentScreen = "Map";
            switchScreen.changeScreen("Map");
            mapControls.inBattle = false;


            numberEnemies = Random.Range(0, 3);
            spawnEnemies();
        }
        initiatePlayerTurn();
    }

    private void initiatePlayerTurn()
    {
        deckManager.drawCards();

        foreach (var enemy in targettedEnemies)
        {
            enemy.transform.GetChild(0).gameObject.SetActive(false);
            enemy.GetComponent<EnemyManager>().isTargetted = false;
        }

        targettedEnemies.Clear();
        targettedEnemies.Add(spawnedEnemies[0]);
        spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
        spawnedEnemies[0].GetComponent<EnemyManager>().isTargetted = true;
        maxTargets = 1;
    }

    public void enemySelector(GameObject enemy)
    {
        bool enemySelected = enemy.GetComponent<EnemyManager>().isTargetted;
       // Debug.Log("enemy is selected:" + enemySelected);
        if (!enemySelected)
        {
            //selects additional enemy if able
            if (targettedEnemies.Count < maxTargets)
            {
                //Debug.Log("Less than max targets");
                enemy.GetComponent<EnemyManager>().isTargetted = true;
                targettedEnemies.Add(enemy);
                enemy.transform.GetChild(0).gameObject.SetActive(true);
            }
            //if max targets selected, replaces oldest one
            else
            {
                Debug.Log("At max targets");
                var oldSelected = targettedEnemies[0];
                oldSelected.GetComponent<EnemyManager>().isTargetted = false;
                targettedEnemies.Remove(oldSelected);
                oldSelected.transform.GetChild(0).gameObject.SetActive(false);

                enemy.GetComponent<EnemyManager>().isTargetted = true;
                targettedEnemies.Add(enemy);
                enemy.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        //if enemy selects, unselects them
        else if (enemySelected)
        {
            Debug.Log("Enemy already selected");
            if (targettedEnemies.Count > 1)
            {
                Debug.Log("At minimum selected");
                enemy.GetComponent<EnemyManager>().isTargetted = false;
                targettedEnemies.Remove(enemy);
                enemy.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    
}
