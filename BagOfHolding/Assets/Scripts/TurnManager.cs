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
    public int inflictBleedMultiplier = 1;
    public int inflictPoison = 0;
    public int inflictHemmorhage = 0;
    public int numAttacksMin = 1;
    public int inflictBleedMin = 0;

    public List<GameObject> spawnableEnemies = new List<GameObject>();
    public List<GameObject> spawnableElites = new List<GameObject>();
    public List<GameObject> spawnableBosses = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<GameObject> targettedEnemies = new List<GameObject>();
    public bool spawnElites;
    public bool spawnBoss;


    public List<GameObject> battleProcessCards = new List<GameObject>();

    public List<GameObject> tempList = new List<GameObject>();

    public int numberEnemies = 1;

    public InventoryManager inventoryManager;
    public DeckManager deckManager;
    public PlayerManager playerManager;
    public SwitchScreen switchScreen;
    public MapControls mapControls;
    public InventoryGridGenerator inventoryGridGenScript;
    public GameObject levelUpScreen;
    public GameObject rewardScreen;
    public GameObject rewardScreenExtras;

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
    private bool CloakofBloodActive;
    private bool PinealEyeActive;
    public bool NecromancerAmuletActive;
    

    
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
        /*if (spawnBoss)
        {
            numberEnemies = 1;
        }*/
        for (int i = 0; i < numberEnemies; i++)
        {
            GameObject spawnedEnemy = null;
            if (!spawnElites && !spawnBoss)
            {
                spawnedEnemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Count)], new Vector3(xCounter, yCounter - 1, -1), Quaternion.identity);
            }
            else if (spawnElites && !spawnBoss)
            {
                spawnedEnemy = Instantiate(spawnableElites[Random.Range(0, spawnableElites.Count)], new Vector3(xCounter, yCounter - 1, -1), Quaternion.identity);
            }
            else if (!spawnElites && spawnBoss)
            {
                spawnedEnemy = Instantiate(spawnableBosses[Random.Range(0, spawnableBosses.Count)], new Vector3(4, 1, -1), Quaternion.identity);
                spawnedEnemies.Add(spawnedEnemy);
                spawnBoss = false;
                break;
            }
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

    public void spawnNewEnemy(GameObject enemyToSpawn)
    {
        var position1filled = false;
        var position2filled = false;
        var position3filled = false;
        var enemySpawned = false;
        GameObject spawnedEnemy = null;
        foreach (var enemy in spawnedEnemies)
        {
            Debug.Log("enemy name" + enemy.transform.name + " and x pos: " + enemy.transform.localPosition.x);
            if (enemy.transform.localPosition.x > 0.5f &&  enemy.transform.localPosition.x < 1.5f)
            {
                position1filled = true;
                Debug.Log("place 1 filled");
            }
            if (enemy.transform.localPosition.x > 3.5f && enemy.transform.localPosition.x < 4.5f)
            {
                position2filled = true;
                Debug.Log("place 2 filled");
            }
            if (enemy.transform.localPosition.x > 6.5f && enemy.transform.localPosition.x < 7.5f)
            {
                position3filled = true;
                Debug.Log("place 3 filled");
            }
        }

        if (!position1filled && !enemySpawned)
        {
            Debug.Log("placing in 1");
            spawnedEnemy = Instantiate(enemyToSpawn, new Vector3(1, 1, -1), Quaternion.identity);
            spawnedEnemies.Insert(0, spawnedEnemy);
            enemySpawned = true;
        }
        if (!position2filled && !enemySpawned)
        {
            Debug.Log("placing in 2");

            spawnedEnemy = Instantiate(enemyToSpawn, new Vector3(4, 0.5f, -1), Quaternion.identity);
            spawnedEnemies.Insert(1, spawnedEnemy);

            enemySpawned = true;
        }
        if (!position3filled && !enemySpawned)
        {
            Debug.Log("placing in 3");

            spawnedEnemy = Instantiate(enemyToSpawn, new Vector3(7, 1, -1), Quaternion.identity);
            spawnedEnemies.Insert(2, spawnedEnemy);

            enemySpawned = true;
        }
        
    }

    public void endTurn()
    {
        /*foreach (var inventorySlot in inventoryManager.InventoryItems)
        {
            Debug.Log("Slot: " + inventorySlot.Key + "     Item: " + inventorySlot.Value);
        }*/
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
            foreach (var item in inventoryManager.extraEquippedItems)
            {
                processCards(item);
            }

            playerManager.playerTakesDamage(playerManager.selfBleed);
            playerManager.playerTakesDamage(playerManager.selfPoison);
            playerManager.playerPoisonControl(-1);
            playerManager.playerBleedControl(-1);
            inventoryManager.clearInventory();
            processHealing();
            processDamage();
            //turnEnded = true;

            //initiateEnemyTurn();
        }

    }

    public void preProcessCards(GameObject card)
    {
        if (card.name == "SwiftCape")
        {
            maxTargets += 1;
        }
        if (card.name == "HelmOfCerberus")
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
        if (card.name == "GeminiNecklace")
        {
            inventoryManager.GeminiNecklaceActivated = true;
        }
        if (card.name == "Spear")
        {
            maxTargets += 1;
        }
        if (card.name == "BriarWhip")
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
        if (card.name == "SlimeKingsGloves")
        {
            inventoryManager.SlimeKingGlovesActivated = true;
        }
        if (card.name == "ReinforcementRing")
        {
            inventoryManager.carryingCapacity += 3;
            carryingWeightObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventoryManager.carryingCapacity.ToString();

            //inventoryManager.carryingCapacityObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventoryManager.carryingCapacity.ToString();

        }
    }

    public void preProcessRemove(GameObject card)
    {
        if (card.name == "SwiftCape")
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
        if (card.name == "GeminiNecklace")
        {
            inventoryManager.GeminiNecklaceActivated = false;
        }
        if (card.name == "HelmOfCerberus")
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
        if (card.name == "Spear")
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

        if (card.name == "BriarWhip")
        {
            maxTargets = 1;

            targettedEnemies.Add(spawnedEnemies[0]);
            spawnedEnemies[0].GetComponent<EnemyManager>().isTargetted = true;
            spawnedEnemies[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        if (card.name == "ReinforcementRing")
        {
            inventoryManager.carryingCapacity -= 3;
            carryingWeightObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inventoryManager.carryingCapacity.ToString();

        }


    }
    private void processCards(GameObject card)
    {
        if (card.name == "Saber")
        {
            damage += 2;
            //inflictBleed += 2;
        }
        if (card.name == "BladedBoots")
        {
            numAttacks += 1;
        }
        if (card.name == "CrimsonLily")
        {
            inflictBleed += 2;
        }
        if (card.name == "CloakOfBlood")
        {
            damageMult += 1;
        }
        if (card.name == "HelmOfCerberus")
        {
            //damage += 5;
        }
        if (card.name == "RitualDagger")
        {
            playerManager.playerTakesDamage(5);
            damage += 5;
        }
        if (card.name == "WingedDaggers")
        {
            numAttacks += 4;
            damage += 1;
        }
        if (card.name == "BirdskullWhistle")
        {
            postProcessCards(card);
        }
        if (card.name == "HiddenDagger")
        {
            damage += 100;
        }
        if (card.name == "Spear")
        {
            damage += 2;
            maxTargets += 1;
        }
        if (card.name == "BriarWhip")
        {
            damage += 9;
            damageDivider = spawnedEnemies.Count;
            maxTargets = 3;
            foreach (var enemy in spawnedEnemies)
            {
                if (!enemy.GetComponent<EnemyManager>().isTargetted)
                {
                    enemySelector(enemy);
                }
            }
        }
        if (card.name == "BasiliskEye")
        {
            foreach (var enemy in targettedEnemies)
            {
                enemy.GetComponent<EnemyManager>().isStunned = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        if (card.name == "FungusCudgel")
        {
            damage += 5;
            foreach (GameObject enemy in spawnedEnemies)
            {
                enemy.GetComponent<EnemyManager>().setBleed(100);
                enemy.GetComponent<EnemyManager>().setPoison(100);
            }
        }
        if (card.name == "AssassinCowl")
        {
            AssassinCowlActive = true;
        }
        if (card.name == "DryadRapier")
        {
            damage += 4;
            inflictHemmorhage += 3;
        }
        if (card.name == "MinotaurHelm")
        {
            damage += 3;
            inflictHemmorhage += 1;
        }
        if (card.name == "Caltrops")
        {
            trapDamage = 1;
            trapDuration = 4;
        }
        if (card.name == "PenanceCage")
        {
            playerManager.playerBleedControl(1);
            inflictBleed += 2;
        }
        if (card.name == "GuardSaber")
        {
            damage += 2;
            inflictBleed += 2;
        }
        if (card.name == "ShortSword")
        {
            damage += 3;
        }
        if (card.name == "ChitinousCuisse")
        {
            playerManager.playerArmor += 5;
            inflictPoison += 1;
        }
        if (card.name == "SeersJack")
        {
            deckManager.handSize += 3;
        }
        if (card.name == "OwlBearTalons")
        {
            inflictBleedMultiplier = 2;
        }
        if (card.name == "MinorPotion")
        {
            healing += 2;
        }
        if (card.name == "HealingPotion")
        {
            healing += 5;
        }
        if (card.name == "GreaterHealingPotion")
        {
            healing += 10;
        }
        if (card.name == "PoisonDarts")
        {
            numAttacks += 2;
            damage += 1;
            inflictPoison += 2;
        }
        if (card.name == "LeatherTassets")
        {
            playerManager.playerArmor += 3;
            //playerManager.playerPoisonArmor += 3;
        }
        if (card.name == "AssassinsPendant")
        {
            currentCritical = 3;
        }
        if (card.name == "BladedCloak")
        {
            numAttacks += 1;
            damage += 3;
            playerManager.playerDodge += 1;
        }
        if (card.name == "CloakofBlood")
        {
            CloakofBloodActive = true;
        }
        if (card.name == "MailChausses")
        {
            playerManager.playerArmor += 6;
            //playerManager.playerBleedArmor += 3;
        }
        if (card.name == "SelfFlagellantChains")
        {
            numAttacks += 1;
            inflictBleed += 3;
            playerManager.playerBleedControl(3);
        }
        if (card.name == "SlimedBoots")
        {
            inflictPoison += 2;
        }
        if (card.name == "SwiftBoots")
        {
            playerManager.playerDodge += 1;
            numAttacks += 1;
        }
        if (card.name == "SwiftCape")
        {
            numAttacks += 2;
            maxTargets += 1;
        }
        if (card.name == "CrimsonLily")
        {
            foreach (var enemy in targettedEnemies)
            {
                if (enemy.GetComponent<EnemyManager>().minPower != 0)
                {
                    enemy.GetComponent<EnemyManager>().minPower -= 1;
                }
                if (enemy.GetComponent<EnemyManager>().maxPower != 0)
                {
                    enemy.GetComponent<EnemyManager>().maxPower -= 1;
                }
            }
        }
        if (card.name == "PinealEye")
        {
            damageMult += 2;
            PinealEyeActive = true;

        }
        if (card.name == "NecromancerAmulet")
        {
            NecromancerAmuletActive = true;
        }

    }

    public void processOnEquip(GameObject card)
    {
        if (card.name == "GravediggersGloves")
        {
            deckManager.drawCardsFromGraveyard(2);
        }
    }

    //processes card that have an effect during the attack
    private void processBattleCards(GameObject card)
    {
        if (card.name == "AssassinsCowl")
        {
        }
    }
    // processes cards that have an effect on next turn

    private void postProcessCards(GameObject card)
    {
        if (card.name == "BirdskullWhistle")
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

            if (!PinealEyeActive)
            {
                //if player hits multiple enemies
                foreach (var enemy in targettedEnemies)
                {

                    enemy.GetComponent<EnemyManager>().timesHit = numAttacks;

                    if (CloakofBloodActive && enemy.GetComponent<EnemyManager>().currBleed != 0)
                    {
                        damageMult += 1;
                        CloakofBloodActive = false;
                    }
                    if (AssassinCowlActive)
                    {
                        if (enemy.GetComponent<EnemyManager>().currentHealth / enemy.GetComponent<EnemyManager>().maxHealth <= 0.3f)
                        {
                            currentCritical = 2;
                            criticalAttacks += 1;
                        }
                        else
                        {
                            if (criticalAttacks != 0)
                            {
                                criticalAttacks -= 1;
                            }
                            else if (criticalAttacks == 0)
                            {
                                currentCritical = 1;
                            }
                        }
                    }
                    //Debug.Log("This shit is adding: " + Mathf.RoundToInt(damage * currentCritical * damageMult));

                    minDamage = Mathf.RoundToInt((damage * currentCritical * damageMult) / damageDivider);
                    Debug.Log("Damage: " + damage);
                    Debug.Log("CurrentCritical: " + currentCritical);
                    Debug.Log("DamageMult: " + damageMult);
                    Debug.Log("minDamage: " + minDamage);
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
            }

            //attacks random enemy if pinealEye is equipped
            else if (PinealEyeActive)
            {
                var enemyToAttack = spawnedEnemies[Random.Range(0, spawnedEnemies.Count)];
                enemyToAttack.GetComponent<EnemyManager>().timesHit = numAttacks;

                if (CloakofBloodActive && enemyToAttack.GetComponent<EnemyManager>().currBleed != 0)
                {
                    damageMult += 1;
                    CloakofBloodActive = false;
                }
                if (AssassinCowlActive)
                {
                    if (enemyToAttack.GetComponent<EnemyManager>().currentHealth / enemyToAttack.GetComponent<EnemyManager>().maxHealth <= 0.3f)
                    {
                        currentCritical = 2;
                        criticalAttacks += 1;
                    }
                    else
                    {
                        if (criticalAttacks != 0)
                        {
                            criticalAttacks -= 1;
                        }
                        else if (criticalAttacks == 0)
                        {
                            currentCritical = 1;
                        }
                    }
                }
                minDamage = Mathf.RoundToInt((damage * currentCritical * damageMult) / damageDivider);
                Debug.Log("Damage: " + damage);
                Debug.Log("CurrentCritical: " + currentCritical);
                Debug.Log("DamageMult: " + damageMult);
                Debug.Log("minDamage: " + minDamage);
                if (damage > 0 && minDamage == 0)
                {
                    minDamage = 1;
                }
                if (minDamage != 0)
                {
                    enemyToAttack.GetComponent<EnemyManager>().damageHits.Add(minDamage);
                }
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

            AssassinCowlActive = false;
        }
        //if doing damage, attack the enemies
        if (damage != 0)
        {
            //Debug.Log("Enemy is taking damage");
            tempList = targettedEnemies;
            foreach (var enemy in targettedEnemies)
            {

                enemy.GetComponent<EnemyManager>().enemyTakesDamage();

            }
        }
        StartCoroutine(delayEnemyAttack());
        //if not doing any damage, checks for status damage then deals with them or continues to enemy attack routine
        /*else if (damage == 0)
        {
            enemiesHaveStatus = 0;
            var tempList = spawnedEnemies;
            foreach (var enemy in spawnedEnemies)
            {
                if (enemy.GetComponent<EnemyManager>().currBleed != 0 || enemy.GetComponent<EnemyManager>().currPoison != 0 || enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                {
                    //if hemorrhage is the only status, adds attackFinishedAmount since it doesnt do damage
                    if (enemy.GetComponent<EnemyManager>().currBleed == 0 && enemy.GetComponent<EnemyManager>().currPoison == 0 && enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                    {
                        enemiesHaveStatus -= 1;
                    }
                    enemiesHaveStatus += 1;
                    enemy.GetComponent<EnemyManager>().enemyStatusControl();

                }
            }
            if (enemiesHaveStatus == 0)
            {
                //Debug.Log("no enemy statuses");
                StartCoroutine(delayEnemyAttack());
            }
        }*/
        
    }

    /*public void trackEnemyAttackFinishes()
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
                    
                    if (enemy.GetComponent<EnemyManager>().currBleed == 0 && enemy.GetComponent<EnemyManager>().currPoison == 0 && enemy.GetComponent<EnemyManager>().currHemmorhage != 0)
                    {
                        enemiesHaveStatus -= 1;
                    }
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
                StartCoroutine(delayEnemyAttack());
            

        }
        else if (attacksFinishedAmount == targettedEnemies.Count + enemiesHaveStatus)
        {
            attacksFinishedAmount = 0;
            StartCoroutine(delayEnemyAttack());
        }
    }*/

    private IEnumerator delayEnemyAttack()
    {
        /*foreach (GameObject enemy in spawnedEnemies)
        {
            enemy.GetComponent<EnemyManager>().statusDamage = false;
        }*/

        damage = 0;
        numAttacks = numAttacksMin;
        //attacksFinishedAmount = 0;
        //enemiesHaveStatus = 0;
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

        if (nextTurnCritical > 1)
        {
            currentCritical = nextTurnCritical;
            nextTurnCritical = 1;
            criticalAttacks = currentCritical;
            currentCritical = 2;
        }
        nextTurnCritical = 1;
        bool statusTriggered = false;
        foreach (var enemy in spawnedEnemies)
        {

            if ((enemy.GetComponent<EnemyManager>().currBleed != 0 && !enemy.GetComponent<EnemyManager>().bleedTriggered) ||
                (enemy.GetComponent<EnemyManager>().currPoison != 0 && !enemy.GetComponent<EnemyManager>().poisonTriggered))
            {
                //enemy.GetComponent<EnemyManager>().bleedTriggered = true;
                //enemy.GetComponent<EnemyManager>().poisonTriggered = true;
                statusTriggered = true;

            }
            if (enemy.GetComponent<EnemyManager>().currHemmorhage != 0 && !enemy.GetComponent<EnemyManager>().hemmorhageTriggered)
            {
                statusTriggered = true;
            }
        }
        yield return new WaitForSecondsRealtime(.5f);
        //initiateEnemyTurn();
        if (statusTriggered)
        {
            StartCoroutine(statusDamage());
        }
        else if (!statusTriggered)
        {
            initiateEnemyTurn();
        }

    }

    private IEnumerator statusDamage()
    {
        bool statusTriggered;
        foreach (var enemy in spawnedEnemies)
        {

            if ((enemy.GetComponent<EnemyManager>().currBleed != 0 && !enemy.GetComponent<EnemyManager>().bleedTriggered) ||
                (enemy.GetComponent<EnemyManager>().currPoison != 0 && !enemy.GetComponent<EnemyManager>().poisonTriggered))
            {
                //enemy.GetComponent<EnemyManager>().bleedTriggered = true;
                //enemy.GetComponent<EnemyManager>().poisonTriggered = true;
                statusTriggered = true;
                enemy.GetComponent<EnemyManager>().damageHits.Clear();
                enemy.GetComponent<EnemyManager>().damageHits.Add(enemy.GetComponent<EnemyManager>().currBleed + enemy.GetComponent<EnemyManager>().currPoison);
                enemy.GetComponent<EnemyManager>().enemyTakesDamage();

                if (enemy.GetComponent<EnemyManager>().currBleed != 0)
                {
                    enemy.GetComponent<EnemyManager>().setBleed(-1);
                }
                if (enemy.GetComponent<EnemyManager>().currPoison != 0)
                {
                    enemy.GetComponent<EnemyManager>().setPoison(-1);
                }

            }
            if (enemy.GetComponent<EnemyManager>().currHemmorhage != 0 && !enemy.GetComponent<EnemyManager>().hemmorhageTriggered)
            {
                //enemy.GetComponent<EnemyManager>().hemmorhageTriggered = true;
                enemy.GetComponent<EnemyManager>().setBleed(enemy.GetComponent<EnemyManager>().currHemmorhage);
                enemy.GetComponent<EnemyManager>().setHemmorhage(-1);
            }

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
        yield return new WaitForSecondsRealtime(1f);
        initiateEnemyTurn();
    }
    private void initiateEnemyTurn()
    {
        bool statusTriggered;
        //initiateEnemyTurnOnce = true;
        if (spawnedEnemies.Count != 0)
        {


            StartCoroutine(EnemyAttackSequence());

        }
        //if all enemies are dead, return to map
        else if (spawnedEnemies.Count == 0)
        {
            if (playerManager.levelUpTriggered)
            {
                levelUpScreen.SetActive(true);
                playerManager.levelUpTriggered = false;
                
            }

            var randomReward = deckManager.obtainableCards[Random.Range(0, deckManager.obtainableCards.Count)];
            GameObject newRewardObject = Instantiate(randomReward, new Vector3(0,0,0), Quaternion.identity);
            newRewardObject.transform.name = randomReward.transform.name;
            //sets position
            var rewardScreenSpawnPoint = rewardScreenExtras.transform.GetChild(0).transform.GetChild(0).transform.position;
            Vector3 newItemPos = newRewardObject.transform.position;
            Vector3 spawnPointPosOriginal = newRewardObject.transform.GetChild(0).transform.position;
            Vector3 spawnPointPos = newRewardObject.transform.GetChild(0).transform.position = new Vector3(rewardScreenSpawnPoint.x, rewardScreenSpawnPoint.y, rewardScreenSpawnPoint.z - 1f); ;
            spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
            newRewardObject.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
            spawnPointPos = spawnPointPosOriginal;
            newRewardObject.transform.GetChild(0).transform.localPosition = newRewardObject.GetComponent<CardManager>().spawnPointOriginalPosition;


            //newRewardObject.transform.localScale = rewardScreen.transform.GetChild(0).transform.localScale;
            newRewardObject.transform.localRotation = rewardScreen.transform.GetChild(0).transform.localRotation;
            newRewardObject.transform.SetParent(rewardScreen.transform);
            newRewardObject.GetComponent<CardManager>().isReward = true;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            randomReward = deckManager.obtainableCards[Random.Range(0, deckManager.obtainableCards.Count)];
            newRewardObject = Instantiate(randomReward, new Vector3(0, 0, 0), Quaternion.identity);
            newRewardObject.transform.name = randomReward.transform.name;
            //sets position
            rewardScreenSpawnPoint = rewardScreenExtras.transform.GetChild(1).transform.GetChild(0).transform.position;
            newItemPos = newRewardObject.transform.position;
            spawnPointPosOriginal = newRewardObject.transform.GetChild(0).transform.position;
            spawnPointPos = newRewardObject.transform.GetChild(0).transform.position = new Vector3(rewardScreenSpawnPoint.x, rewardScreenSpawnPoint.y, rewardScreenSpawnPoint.z - 1f); ;
            spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
            newRewardObject.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
            spawnPointPos = spawnPointPosOriginal;
            newRewardObject.transform.GetChild(0).transform.localPosition = newRewardObject.GetComponent<CardManager>().spawnPointOriginalPosition;
            //newRewardObject.transform.localScale = rewardScreen.transform.GetChild(1).transform.localScale;
            newRewardObject.transform.localRotation = rewardScreen.transform.GetChild(1).transform.localRotation;
            newRewardObject.transform.SetParent(rewardScreen.transform);
            newRewardObject.GetComponent<CardManager>().isReward = true;


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            randomReward = deckManager.obtainableCards[Random.Range(0, deckManager.obtainableCards.Count)];
            newRewardObject = Instantiate(randomReward, new Vector3(0, 0, 0), Quaternion.identity);
            newRewardObject.transform.name = randomReward.transform.name;
            //sets position
            rewardScreenSpawnPoint = rewardScreenExtras.transform.GetChild(2).transform.GetChild(0).transform.position;
            newItemPos = newRewardObject.transform.position;
            spawnPointPosOriginal = newRewardObject.transform.GetChild(0).transform.position;
            spawnPointPos = newRewardObject.transform.GetChild(0).transform.position = new Vector3(rewardScreenSpawnPoint.x, rewardScreenSpawnPoint.y, rewardScreenSpawnPoint.z - 1f); ;
            spawnPointPos = (spawnPointPos - spawnPointPosOriginal);
            newRewardObject.transform.position = new Vector3(newItemPos.x + spawnPointPos.x, newItemPos.y + spawnPointPos.y, spawnPointPos.z);
            spawnPointPos = spawnPointPosOriginal;
            newRewardObject.transform.GetChild(0).transform.localPosition = newRewardObject.GetComponent<CardManager>().spawnPointOriginalPosition;
            //newRewardObject.transform.localScale = rewardScreen.transform.GetChild(2).transform.localScale;
            newRewardObject.transform.localRotation = rewardScreen.transform.GetChild(2).transform.localRotation;
            newRewardObject.transform.SetParent(rewardScreen.transform);
            newRewardObject.GetComponent<CardManager>().isReward = true;

            Destroy(rewardScreen.transform.GetChild(2).gameObject);
            Destroy(rewardScreen.transform.GetChild(1).gameObject);
            Destroy(rewardScreen.transform.GetChild(0).gameObject);


            spawnElites = false;


            rewardScreen.SetActive(true);
            rewardScreenExtras.SetActive(true);

            //rewardScreen.transform.GetChild(0) = deckManager.obtainableCards[Random.Range(0, deckManager.obtainableCards.Count)];

            //switchScreen.currentScreen = "Map";
            //switchScreen.changeScreen("Map");
            targettedEnemies.Clear();
            spawnedEnemies.Clear();
            //mapControls.inBattle = false;
            


            numberEnemies = Random.Range(1, 4);
            carryingWeightObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "5";

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
                /*if ((enemy.GetComponent<EnemyManager>().currBleed != 0 && !enemy.GetComponent<EnemyManager>().bleedTriggered) ||
                    (enemy.GetComponent<EnemyManager>().currPoison != 0 && !enemy.GetComponent<EnemyManager>().poisonTriggered))
                {
                    enemy.GetComponent<EnemyManager>().bleedTriggered = true;
                    enemy.GetComponent<EnemyManager>().poisonTriggered = true;
                    statusTriggered = true;
                    enemy.GetComponent<EnemyManager>().damageHits.Clear();
                    enemy.GetComponent<EnemyManager>().damageHits.Add(enemy.GetComponent<EnemyManager>().currBleed + enemy.GetComponent<EnemyManager>().currPoison);
                    enemy.GetComponent<EnemyManager>().enemyTakesDamage();

                    if (enemy.GetComponent<EnemyManager>().currBleed != 0)
                    {
                        enemy.GetComponent<EnemyManager>().setBleed(-1);
                    }
                    if (enemy.GetComponent<EnemyManager>().currPoison != 0)
                    {
                        enemy.GetComponent<EnemyManager>().setPoison(-1);
                    }

                }
                if (enemy.GetComponent<EnemyManager>().currHemmorhage != 0 && !enemy.GetComponent<EnemyManager>().hemmorhageTriggered)
                {
                    enemy.GetComponent<EnemyManager>().hemmorhageTriggered = true;
                    enemy.GetComponent<EnemyManager>().setBleed(enemy.GetComponent<EnemyManager>().currHemmorhage);
                    enemy.GetComponent<EnemyManager>().setHemmorhage(-1);
                }*/
                if (!enemy.GetComponent<EnemyManager>().attackAnimationDone && enemy.GetComponent<EnemyManager>().tempClawMarkPNG != null)
                {
                    allEnemiesDone = false;
                    continue;
                }

                else if (enemy.GetComponent<EnemyManager>().attackAnimationDone || enemy.GetComponent<EnemyManager>().isStunned == true)
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
                enemy.GetComponent<EnemyManager>().turnBehavior();
                enemy.GetComponent<EnemyManager>().attackAnimationDone = false;
                enemy.GetComponent<EnemyManager>().bleedTriggered = false; ;
                enemy.GetComponent<EnemyManager>().poisonTriggered = false; ;
                enemy.GetComponent<EnemyManager>().hemmorhageTriggered = false;
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
        deckManager.handSize = 6;

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
        inflictBleed = inflictBleedMin;
        inflictBleedMultiplier = 1;
        inflictPoison = 0;
        inflictHemmorhage = 0;
        enemiesHaveStatus = 0;

        PinealEyeActive = false;
        NecromancerAmuletActive = false;
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
