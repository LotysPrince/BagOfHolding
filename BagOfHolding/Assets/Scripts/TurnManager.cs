using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int maxTargets;
    public int damage = 0;
    public int damageMult = 1;
    public int damageDivider = 1;
    public int healing = 0;
    public int numAttacks = 1;
    public int inflictBleed = 0;
    public int inflictBleedOnSelf = 0;
    public int inflictPoison = 0;
    public int inflictHemmorhage = 0;

    public List<GameObject> spawnableEnemies = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<GameObject> targettedEnemies = new List<GameObject>();

    public List<GameObject> battleProcessCards = new List<GameObject>();

    public List<GameObject> tempList = new List<GameObject>();

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

    public GameObject carryingWeightObject;
    //status effects
    private int nextTurnCritical;
    public int currentCritical = 1;
    public int criticalAttacks = 0;
    public int enemiesHaveStatus;

    //private bool turnEnded;
    public int attacksFinishedAmount;
    //public bool attackIsFinished;
    //public int enemiesWithStatus;
    //private bool initiateEnemyTurnOnce;

    public int trapDamage;
    public int trapDuration;

    public bool enemyAttacking;


    //card variables
    private bool AssassinCowlActive;
    

    
    // Start is called before the first frame update
    void Start()
    {
        //numberEnemies = Random.Range(0, 3);
        numberEnemies = Random.Range(2, 4);
        //spawnEnemies();


    }

    // Update is called once per frame
    void Update()
    { /*
        //waits until attack is done on all targetted enemies
        if (targettedEnemies.Count != 0 && (attacksFinishedAmount == targettedEnemies.Count || (turnEnded && attacksFinishedAmount == 0)) && turnEnded && !attackIsFinished && !initiateEnemyTurnOnce)
        {
            attackIsFinished = true;
            attacksFinishedAmount = 0;

            //uses temp array as to not modify array while iterating if an enemy dies
            var tempArray = spawnedEnemies;
            foreach(var enemy in tempArray)
            {
                if (enemy.GetComponent<EnemyManager>().currPoison != 0 || enemy.GetComponent<EnemyManager>().currBleed != 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                {
                    enemiesWithStatus += 1;
                    enemy.GetComponent<EnemyManager>().enemyStatusControl();

                }
            }
            if (enemiesWithStatus == 0)
            {

                if (!initiateEnemyTurnOnce)
                {

                    initiateEnemyTurnOnce = true;
                    Debug.Log("KillMe");
                    //initiateEnemyTurn();
                    StartCoroutine(delayEnemyAttack());
                }
            }
        }
        //statuses are done being calculated/damaged
        else if (enemiesWithStatus != 0 && attacksFinishedAmount == enemiesWithStatus && attackIsFinished && turnEnded)
        {
            if (!initiateEnemyTurnOnce)
            {
                initiateEnemyTurnOnce = true;
                //initiateEnemyTurn();
                StartCoroutine(delayEnemyAttack());
            }
        }*/
        
    }

    public void spawnEnemies()
    {
        var xCounter = 1f;
        var yCounter = 2f;
        for (int i = 0; i < numberEnemies; i++)
        {

            var spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Count)], new Vector3(xCounter, yCounter -1, -1), Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy);
            xCounter += 3;
            if(yCounter == 2f)
            {
                yCounter = 1.5f;
            }
            else if(yCounter == 1.5f)
            {
                yCounter = 2f;
            }
        }

        targettedEnemies.Add(spawnedEnemies[0]);
        spawnedEnemies[0].GetComponent<EnemyManager>().isTargetted = true;
        spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void endTurn()
    {
        if (switchScreen.currentScreen == "Battle")
        {
            deckManager.emptyHand();
            inventoryGridGenScript.clearInventoryGrid();

            foreach (var item in inventoryManager.InventoryItems)
            {
                if (item.Value != null)
                {
                    processCards(item.Value);
                }
            }

            inventoryManager.clearInventory();
            processHealing();
            processDamage();
            //turnEnded = true;

            //initiateEnemyTurn();
        }

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
        if (card.name == "Spear(Clone)")
        {
            maxTargets += 1;
        }
        if (card.name == "BriarWhip(Clone)")
        {
            foreach (var enemy in spawnedEnemies)
            {
                maxTargets = 0;

                if (enemy.GetComponent<EnemyManager>().isTargetted)
                {
                    enemySelector(enemy);
                }
            }

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
        if (card.name == "Spear(Clone)")
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

        if (card.name == "BriarWhip(Clone)")
        {
            maxTargets = 1;

            targettedEnemies.Add(spawnedEnemies[0]);
            spawnedEnemies[0].GetComponent<EnemyManager>().isTargetted = true;
            spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
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
            damageMult += 1;
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
            numAttacks += 4;
            damage += 1;
        }
        if (card.name == "BirdskullWhistle(Clone)")
        {
            postProcessCards(card);
        }
        if (card.name == "HiddenDagger(Clone)")
        {
            damage += 100;
        }
        if (card.name == "Spear(Clone)")
        {
            damage += 2;
            maxTargets += 1;
        }
        if (card.name == "BriarWhip(Clone)")
        {
            damage += 9;
            damageDivider = spawnedEnemies.Count;
            maxTargets = 3;
            foreach(var enemy in spawnedEnemies)
            {
                if (!enemy.GetComponent<EnemyManager>().isTargetted)
                {
                    enemySelector(enemy);
                }
            }
        }
        if (card.name == "BasiliskEye(Clone)")
        {
            foreach (var enemy in targettedEnemies)
            {
                enemy.GetComponent<EnemyManager>().isStunned = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        if (card.name == "FungusCudgel(Clone)")
        {
            damage += 5;
            inflictBleed += 2;
            inflictPoison += 2;
        }
        if (card.name == "AssassinsCowl(Clone)")
        {

        }
        if (card.name == "DryadRapier(Clone)")
        {
            damage += 4;
            inflictHemmorhage += 3;
        }
        if (card.name == "MinotaurHelm(Clone)")
        {
            damage += 3;
            inflictHemmorhage += 1;
        }
        if (card.name == "Caltrops(Clone)")
        {
            trapDamage = 1;
            trapDuration = 4;
        }
        if (card.name == "PenanceCage(Clone)")
        {
            inflictBleedOnSelf += 1;
            inflictBleed += 2;
        }
        if (card.name == "GuardSaber(Clone)")
        {
            damage += 2;
            inflictBleed += 2;
        }
        if (card.name == "ShortSword(Clone)")
        {
            damage += 3;
        }
        if (card.name == "ChitinousCuisse(Clone)")
        {
            playerManager.playerArmor += 5;
            inflictPoison += 1;
        }
    }

    //processes card that have an effect during the attack
    private void processBattleCards(GameObject card)
    {
        if (card.name == "AssassinsCowl(Clone)")
        {
        }
    }
    // processes cards that have an effect on next turn

    private void postProcessCards(GameObject card)
    {
        if (card.name == "BirdskullWhistle(Clone)")
        {
            nextTurnCritical = 3;
            //criticalAttacks += 3;
        }
    }


    private void processHealing()
    {
        playerManager.playerHealsDamage(healing);
        healing = 0;
    }

    private void processDamage()
    {
        attacksFinishedAmount = 0;
        var minDamage = 1;
        tempList = targettedEnemies;
        //player attacks multiple times
        for (int i = 1; i <= numAttacks; i++)
        {

            //if player hits multiple enemies
            foreach (var enemy in targettedEnemies)
            {

                enemy.GetComponent<EnemyManager>().timesHit = numAttacks;
                //Debug.Log("This shit is adding: " + Mathf.RoundToInt(damage * currentCritical * damageMult));
                
                minDamage = Mathf.RoundToInt((damage * currentCritical * damageMult)/damageDivider);

                if (damage > 0 && minDamage == 0)
                {
                    minDamage = 1;
                }
                if (minDamage != 0)
                {
                    enemy.GetComponent<EnemyManager>().damageHits.Add(minDamage);
                }
                //enemy.GetComponent<EnemyManager>().enemyTakesDamage(Mathf.FloorToInt(damage * currentCritical * damageMult));
                /*enemy.GetComponent<EnemyManager>().setBleed(inflictBleed);
                enemy.GetComponent<EnemyManager>().setPoison(inflictPoison);
                enemy.GetComponent<EnemyManager>().setHemmorhage(inflictHemmorhage);*/

            }
            //if attacking multiple times, reduces the critical attacks until stacks disappear
            if (criticalAttacks != 0)
            {
                criticalAttacks -= 1;
                if (criticalAttacks == 0)
                {
                    currentCritical = 1;
                }
            }
        }
        //if doing damage, attack the enemies
        if (damage != 0)
        {
            Debug.Log("Enemy is taking damage");
            tempList = targettedEnemies;
            foreach (var enemy in targettedEnemies)
            {

                enemy.GetComponent<EnemyManager>().enemyTakesDamage();

            }
        }
        //if not doing any damage, checks for status damage then deals with them or continues to enemy attack routine
        else if (damage == 0)
        {
            enemiesHaveStatus = 0;
            var tempList = spawnedEnemies;
            foreach (var enemy in spawnedEnemies)
            {
                if (enemy.GetComponent<EnemyManager>().currBleed != 0 || enemy.GetComponent<EnemyManager>().currPoison != 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                {
                    //if hemorrhage is the only status, adds attackFinishedAmount since it doesnt do damage
                    /*if (enemy.GetComponent<EnemyManager>().currBleed == 0 || enemy.GetComponent<EnemyManager>().currPoison == 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                    {
                        attacksFinishedAmount += 1;
                    }*/
                    enemiesHaveStatus += 1;
                    enemy.GetComponent<EnemyManager>().enemyStatusControl();

                }
            }
            if (enemiesHaveStatus == 0)
            {
                Debug.Log("no enemy statuses");
                StartCoroutine(delayEnemyAttack());
            }
        }


        
    }

    public void trackEnemyAttackFinishes()
    {
        attacksFinishedAmount += 1;
        //checks if enemies are done being attacked or if not doing damage, checks if status are done being calculated
        if ((attacksFinishedAmount == targettedEnemies.Count && enemiesHaveStatus == 0) || (damage == 0 && attacksFinishedAmount == enemiesHaveStatus))
        {
            //if all enemies are done being attacked, calculates all statuses


            enemiesHaveStatus = 0;
            tempList = spawnedEnemies;
            foreach (var enemy in spawnedEnemies)
            {
                if (enemy.GetComponent<EnemyManager>().currBleed != 0 || enemy.GetComponent<EnemyManager>().currPoison != 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                {
                    /*
                    if (enemy.GetComponent<EnemyManager>().currBleed == 0 || enemy.GetComponent<EnemyManager>().currPoison == 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                    {
                        attacksFinishedAmount += 1;
                    }*/
                    enemiesHaveStatus += 1;
                    enemy.GetComponent<EnemyManager>().enemyStatusControl();
                }
            }

            if (enemiesHaveStatus == 0)
            {
                attacksFinishedAmount = 0;
                StartCoroutine(delayEnemyAttack());
            }

            /*if (enemiesHaveStatus == 0)
            {
                Debug.Log("no enemy statuses");
                StartCoroutine(delayEnemyAttack());
            }
            else if (enemiesHaveStatus != 0)
            {
                attacksFinishedAmount = 0;
                StartCoroutine(delayEnemyAttack());*/
            

        }
        else if (attacksFinishedAmount == targettedEnemies.Count + enemiesHaveStatus)
        {
            attacksFinishedAmount = 0;
            StartCoroutine(delayEnemyAttack());
        }
    }
    private IEnumerator delayEnemyAttack()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            enemy.GetComponent<EnemyManager>().statusDamage = false;
        }

        damage = 0;
        numAttacks = 1;
        attacksFinishedAmount = 0;
        enemiesHaveStatus = 0;
        /*inflictBleed = 0;
        inflictPoison = 0;
        inflictBleedOnSelf = 0;
        inflictHemmorhage = 0;*/
        currentCritical = 1;
        criticalAttacks = 0;
        damageMult = 1;
        damageDivider = 1;

        trapDuration -= 1;
        if (trapDuration == 0)
        {
            trapDamage = 0;
        }

        if (nextTurnCritical != 1)
        {
            currentCritical = nextTurnCritical;
            nextTurnCritical = 1;
            criticalAttacks = currentCritical;
            currentCritical = 2;
        }
        yield return new WaitForSecondsRealtime(.5f);
        initiateEnemyTurn();
    }
    private void initiateEnemyTurn()
    {
        
        //initiateEnemyTurnOnce = true;
        if (spawnedEnemies.Count != 0)
        {
            StartCoroutine(EnemyAttackSequence());
            /*foreach (var enemy in spawnedEnemies)
            {
                if (!enemy.GetComponent<EnemyManager>().isStunned)
                {
                    enemy.GetComponent<EnemyManager>().enemyDealsDamage();
                }
                enemy.GetComponent<EnemyManager>().setPower();
                enemy.GetComponent < EnemyManager>().isStunned = false;
            }*/
        }
        //if all enemies are dead, return to map
        else if (spawnedEnemies.Count == 0)
        {
            switchScreen.currentScreen = "Map";
            switchScreen.changeScreen("Map");
            targettedEnemies.Clear();
            spawnedEnemies.Clear();
            mapControls.inBattle = false;


            numberEnemies = Random.Range(1, 4);
            //spawnEnemies();
        }
        //initiatePlayerTurn();
    }

    
    private IEnumerator EnemyAttackSequence()
    {
        var allEnemiesDone = true;
        if (!enemyAttacking /*&& initiateEnemyTurnOnce && turnEnded*/)
        {
            foreach (var enemy in spawnedEnemies)
            {
                if (enemy.GetComponent<EnemyManager>().attackAnimationDone || enemy.GetComponent<EnemyManager>().isStunned == true)
                {
                    enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    continue;
                }
                else
                {
                    enemyAttacking = true;
                    allEnemiesDone = false;
                    StartCoroutine(enemy.GetComponent<EnemyManager>().AttackAnimation());
                    break;
                }
            }
        }
        else
        {
            allEnemiesDone = false;
        }

        if (allEnemiesDone)
        {
            yield return new WaitForSecondsRealtime(.5f);
            StopCoroutine(EnemyAttackSequence());
            foreach (var enemy in spawnedEnemies)
            {
                enemy.GetComponent<EnemyManager>().attackAnimationDone = false;
            }
            initiatePlayerTurn();
        }
        else if (!allEnemiesDone)
        {
            yield return new WaitForSecondsRealtime(.1f);
            StartCoroutine(EnemyAttackSequence());
        }
    }

    private void initiatePlayerTurn()
    {
        carryingWeightObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";

        deckManager.drawCards();

        foreach (var enemy in targettedEnemies)
        {
            if (enemy != null)
            {
                enemy.transform.GetChild(0).gameObject.SetActive(false);
                enemy.GetComponent<EnemyManager>().isTargetted = false;
            }
        }
        foreach (var enemy in spawnedEnemies)
        {
            enemy.GetComponent<EnemyManager>().isStunned = false;
        }


        targettedEnemies.Clear();
        targettedEnemies.Add(spawnedEnemies[0]);
        spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
        spawnedEnemies[0].GetComponent<EnemyManager>().isTargetted = true;
        maxTargets = 1;
        inflictBleed = 0;
        inflictPoison = 0;
        inflictHemmorhage = 0;
        enemiesHaveStatus = 0;
        //initiateEnemyTurnOnce = false;
        //attackIsFinished = false;
        //turnEnded = false;
        //enemiesWithStatus = 0;
    }

    public void enemySelector(GameObject enemy)
    {
        bool enemySelected = enemy.GetComponent<EnemyManager>().isTargetted;
       // Debug.Log("enemy is selected:" + enemySelected);
        if (!enemySelected && maxTargets != 0)
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
                //Debug.Log("At max targets");
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
            //Debug.Log("Enemy already selected");
            if (targettedEnemies.Count > 1 || maxTargets == 0)
            {
                //Debug.Log("At minimum selected");
                enemy.GetComponent<EnemyManager>().isTargetted = false;
                targettedEnemies.Remove(enemy);
                enemy.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    
}
