using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int currentShield;
    public int minShield;
    public int maxShield;
    public int minPower;
    public int maxPower;
    public int currPower;
    public int currBleed;
    public int currPoison;
    public int currHemmorhage;

    public bool permaArmor;
    public int startingArmor;

    //variables for taking damage over time
    private bool coroutineRunning;
    public int timesHit;
    private int currentTimesHit;
    public List<int> damageHits = new List<int>();

    //variables for damage animation
    private float originalXPosition;
    private float xMoveSpeed = .1f;
    public bool reachedLeftPoint;
    public bool reachedRightPoint;
    public bool animationDone;
    private bool statusesDealt;


    private GameObject myHealthBar;
    public GameObject healthBarObject;

    private GameObject myAttackBar;
    public GameObject attackBarObject;

    private GameObject myDebuffIcon;
    public GameObject debuffIconObject;

    private GameObject myThiefIcon;
    public GameObject thiefIconObject;

    private GameObject myShieldIcon;
    public GameObject shieldIconObject;

    private GameObject myBiteIcon;
    public GameObject biteIconObject;

    public GameObject bleedObject;
    public GameObject myBleedNum;

    public GameObject poisonObject;
    public GameObject myPoisonNum;

    public GameObject hemmorhageObject;
    public GameObject myHemmorhageNum;

    

    public bool bleedTriggered;
    public bool poisonTriggered;
    public bool hemmorhageTriggered;

    

    private TurnManager turnManager;
    public bool isTargetted = false;
    public GameObject playerObject;

    private PlayerManager playerManager;

    public GameObject clawMarkPNG;
    public GameObject tempClawMarkPNG;

    public GameObject playerClawMarkPNG;
    public GameObject tempPlayerClawPNG;

    public bool isStunned;
    public bool damagedByTrap;
    public bool statusDamage;

    private bool reachedAttackLeftPoint;
    private bool reachedAttackRightPoint;
    public bool attackAnimationDone;

    public int goldDrop;
    public int goldDropMin;
    public int goldDropMax;

    public int expDrop;
    public int expDropMin;
    public int expDropMax;

    public List<string> potentialBehaviors = new List<string>();
    private string currentBehavior;

    public int setAttacksNum;
    public int numAttacks;
    private int currAttacksDone;

    public GameObject wolfMinion;
    public Sprite treantAttackForm;
    private bool treantTransformed;
    public bool attackEffectDone;


    // Start is called before the first frame update
    void Start()
    {
        goldDrop = Random.Range(goldDropMin, goldDropMax);
        expDrop = Random.Range(expDropMin, expDropMax);

        originalXPosition = gameObject.transform.position.x;
        turnManager = GameObject.Find("Scripts").GetComponent<TurnManager>();
        playerManager = GameObject.Find("Scripts").GetComponent<PlayerManager>();


        //playerObject = GameObject.Find("Player");
        GameObject enemyHealthbar = Instantiate(healthBarObject, transform.position + new Vector3(-0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        myHealthBar = enemyHealthbar;
        myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();

        GameObject enemyAttackBar = Instantiate(attackBarObject, transform.position + new Vector3(0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        myAttackBar = enemyAttackBar;

        GameObject enemyDebuffIcon = Instantiate(debuffIconObject, transform.position + new Vector3(0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        myDebuffIcon = enemyDebuffIcon;
        myDebuffIcon.SetActive(false);

        if (!permaArmor)
        {
            GameObject enemyShieldIcon = Instantiate(shieldIconObject, transform.position + new Vector3(0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
            myShieldIcon = enemyShieldIcon;
            myShieldIcon.SetActive(false);
        }
        else if (permaArmor)
        {
            GameObject enemyShieldIcon = Instantiate(shieldIconObject, transform.position + new Vector3(-1f, 2f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
            myShieldIcon = enemyShieldIcon;
            myShieldIcon.SetActive(true);
            currentShield = startingArmor;
            myShieldIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentShield.ToString();

        }

        GameObject enemyThiefIcon = Instantiate(thiefIconObject, transform.position + new Vector3(0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        myThiefIcon = enemyThiefIcon;
        myThiefIcon.SetActive(false);

        if (gameObject.name == "wolfEnemy(Clone)" || gameObject.name == "werewolfEnemy(Clone)")
        {
            
            GameObject enemyBiteIcon = Instantiate(biteIconObject, transform.position + new Vector3(0.5f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
            myBiteIcon = enemyBiteIcon;
            myBiteIcon.SetActive(false);
        }

        setPower();
        turnBehavior();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void setPower()
    {
        currPower = Random.Range(minPower, maxPower);
        if (numAttacks == 1 || numAttacks == 0)
        {
            myAttackBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPower.ToString();
            if (gameObject.name == "wolfEnemy(Clone)")
            {
                myBiteIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPower.ToString();

            }
        }
        else if (numAttacks > 1)
        {
            myAttackBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPower.ToString() + "x" + numAttacks.ToString();
        }
        myThiefIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPower.ToString();


    }
    public void setShield()
    {
        currentShield = Random.Range(minShield, maxShield);
        myShieldIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentShield.ToString();
    }

    public void enemyTakesDamage()
    {
        //Debug.Log("ya");
        //Debug.Log("coroutineRunning: " + coroutineRunning);
        //Debug.Log("Damage hits count :" + damageHits.Count);
        //Debug.Log("First damage: " + damageHits[0]);
        if (coroutineRunning == false && damageHits.Count != 0 && damageHits[0] != 0)
        {
           // Debug.Log("Enemy begins taking damage animation");
            coroutineRunning = true;
            animationDone = true;
            StartCoroutine(DamageAnimation(damageHits[0]));
        }
        else
        {
            damageHits.Clear();
        }
        //currentHealth -= damage;
        //currentHealth -= currBleed;



        //myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();

       // if (currentHealth <= 0)
        //{
        //    killEnemy();
        //}
    }

    /*public void enemyStatusControl()
    {
        damageHits.Clear();
        //reduces bleed amount here instead of taking damage function because that one is only run when theyre targetted
        if (currBleed > 0)
        {
            statusDamage = true;

            damageHits.Add(currBleed);
            currBleed -= 1;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();
            if (currBleed == 0)
            {
                Destroy(myBleedNum);
            }
        }
        if (currPoison > 0)
        {
            statusDamage = true;

            damageHits.Add(currPoison);
            currPoison -= 1;
            myPoisonNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPoison.ToString();
            if (currPoison == 0)
            {
                Destroy(myPoisonNum);
            }
        }
        if (currHemmorhage > 0)
        {
            if (currBleed == 0)
            {

                GameObject enemyBleedBar = Instantiate(bleedObject, transform.position + new Vector3(-.5f, -2f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myBleedNum = enemyBleedBar;
                //bool bleedJustAdded = true;

            }
            currBleed += currHemmorhage;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();

            currHemmorhage -= 1;
            myHemmorhageNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currHemmorhage.ToString();
            if (currHemmorhage == 0)
            {
                Destroy(myHemmorhageNum);
            }
            //turnManager.trackEnemyAttackFinishes();
        }

        if (coroutineRunning == false && damageHits.Count != 0 && damageHits[0] != 0)
        {
            //Debug.Log("Start damage for status");
            coroutineRunning = true;
            animationDone = true;
            StartCoroutine(DamageAnimation(damageHits[0]));
        }
        else
        {
            damageHits.Clear();
        }

    }*/

    public void enemyDealsDamage()
    {

        /////////////////////////////////////////////////
        playerManager.playerTakesDamage(currPower);
        if (turnManager.trapDamage != 0)
        {
            damagedByTrap = true;
            damageHits.Add(turnManager.trapDamage);
            enemyTakesDamage();
        }
    }

    public void enemyHealsDamage(int healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
    }

    public void setBleed(int addBleed)
    {
        if (addBleed > 0)
        {
            if (currBleed == 0)
            {
                GameObject enemyBleedBar = Instantiate(bleedObject, transform.position + new Vector3(-.5f, -2f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myBleedNum = enemyBleedBar;
                //bool bleedJustAdded = true;

            }
            currBleed += addBleed;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();
            //damageHits.Add(currBleed);
        }
        else if (addBleed < 0)
        {
            currBleed += addBleed;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();

            if (currBleed == 0)
            {
                Destroy(myBleedNum);
            }
            
        }


    }

    public void setPoison(int addPoison)
    {
        if (addPoison > 0)
        {
            if (currPoison == 0)
            {
                GameObject enemyPoisonBar = Instantiate(poisonObject, transform.position + new Vector3(0, -2f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myPoisonNum = enemyPoisonBar;

            }
            currPoison += addPoison;
            myPoisonNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPoison.ToString();
            //damageHits.Add(currPoison);
        }

        else if (addPoison < 0)
        {
            currPoison += addPoison;
            myPoisonNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPoison.ToString();

            if (currPoison == 0)
            {
                Destroy(myPoisonNum);
            }

        }


    }
    public void setHemmorhage(int addHemmorhage)
    {
        if (addHemmorhage > 0)
        {
            if (currHemmorhage == 0)
            {
                GameObject enemyHemmorhageBar = Instantiate(hemmorhageObject, transform.position + new Vector3(.5f, -2f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myHemmorhageNum = enemyHemmorhageBar;

            }
            currHemmorhage += addHemmorhage;
            myHemmorhageNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currHemmorhage.ToString();
            //damageHits.Add(currPoison);
        }

        else if (addHemmorhage < 0)
        {
            currHemmorhage += addHemmorhage;
            myHemmorhageNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currHemmorhage.ToString();

            if (currHemmorhage == 0)
            {
                Destroy(myHemmorhageNum);
            }

        }


    }


    public void killEnemy()
    {
        turnManager.spawnedEnemies.Remove(gameObject);
        /*if (turnManager.tempList == turnManager.spawnedEnemies)
        {
            turnManager.tempList.Remove(gameObject);
            turnManager.spawnedEnemies = turnManager.tempList;
        }
        else if (turnManager.tempList == turnManager.targettedEnemies)
        {
            turnManager.tempList.Remove(gameObject);
            turnManager.targettedEnemies = turnManager.tempList;
        }*/
        if (turnManager.NecromancerAmuletActive && turnManager.targettedEnemies.Contains(gameObject))
        {
            playerManager.currentHealth = playerManager.maxHealth;
        }

        playerManager.playerExpGain(expDrop);
        playerManager.playerGoldChange(goldDrop);
        Destroy(myHealthBar);
        Destroy(myAttackBar);
        Destroy(myBleedNum);
        Destroy(myPoisonNum);
        Destroy(myHemmorhageNum);
        Destroy(gameObject);
        Destroy(myThiefIcon);
        Destroy(myDebuffIcon);
        if(myBiteIcon != null)
        {
            Destroy(myBiteIcon);
        }

    }

    private void OnMouseDown()
    {
        turnManager.enemySelector(gameObject);
    }

    private IEnumerator DamageAnimation(int damage)
    {
        //Debug.Log(damage);
        //reduces health
        //Debug.Log("yeyeyeyeye");
        if (animationDone == true && damageHits.Count != 0)
        {
            //Debug.Log("damage Dealt");
            if (currentShield != 0)
            {
                if (currentShield >= damage)
                {
                    currentShield -= damage;
                    damage = 0;
                }
                else if (currentShield < damage)
                {
                    damage -= currentShield;
                    currentShield = 0;
                    myShieldIcon.SetActive(false);
                }
                myShieldIcon.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentShield.ToString();
            }
            currentHealth -= damage;


            myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
            damageHits.RemoveAt(0);

            //if caltrop damage on the field, player takes damage
            if (turnManager.trapDamage != 0 && !damagedByTrap)
            {
                playerManager.playerTakesDamage(turnManager.trapDamage);
            }

            animationDone = false;
        }

        //StartCoroutine(damageMovement());

        //moving animation
        else if(!animationDone)
        {
            //Debug.Log("animation continuing");
            if (tempClawMarkPNG == null)
            {
                if (statusesDealt == false && statusDamage == false)
                {
                    setBleed(turnManager.inflictBleed * turnManager.inflictBleedMultiplier);
                    setPoison(turnManager.inflictPoison);
                    setHemmorhage(turnManager.inflictHemmorhage);
                }
                tempClawMarkPNG = Instantiate(clawMarkPNG, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(clawMarkPNG.transform.rotation.z - 30f, clawMarkPNG.transform.rotation.z + 30f))));
            }
            if (!reachedLeftPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x <= originalXPosition - .2f)
                {
                    reachedLeftPoint = true;
                }
            }
            else if (reachedLeftPoint && !reachedRightPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x >= originalXPosition + .2f)
                {
                    reachedRightPoint = true;
                }
            }
            else if (reachedLeftPoint && reachedRightPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x >= originalXPosition - .2f && gameObject.transform.position.x <= originalXPosition + .2f)
                {
                    Destroy(tempClawMarkPNG);
                    tempClawMarkPNG = null;
                    reachedLeftPoint = false;
                    reachedRightPoint = false;
                    animationDone = true;
                    gameObject.transform.position = new Vector3(originalXPosition, gameObject.transform.position.y, gameObject.transform.position.z);


                }
            }
        }


        if (animationDone && damageHits.Count != 0)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }


        //if animation and attacking is done
        if (damageHits.Count == 0 && animationDone)
        {
            reachedLeftPoint = false;
            reachedRightPoint = false;
            coroutineRunning = false;
            damageHits.Clear();
            // if damage isnt caused by a trap like caltrops, end like normal
            if (!damagedByTrap)
            {
                //turnManager.trackEnemyAttackFinishes();
            }

            else
            {
                damagedByTrap = false;
            }

            if (currentHealth <= 0)
            {
                //turnManager.trackEnemyAttackFinishes();
                StopCoroutine(DamageAnimation(0));
                killEnemy();
            }
            StopCoroutine(DamageAnimation(0));
        }
        // if attacking is done but animation is still going
        else if (damageHits.Count == 0 && !animationDone)
        {
            StartCoroutine(DamageAnimation(0));

        }
        //continues normally
        else
        {
            StartCoroutine(DamageAnimation(damageHits[0]));
        }

        
    }

    public void turnBehavior()
    {
        myAttackBar.SetActive(false);
        myDebuffIcon.SetActive(false);
        currAttacksDone = 0;
        if (!permaArmor)
        {
            myShieldIcon.SetActive(false);
            numAttacks = 0;
        }
        myThiefIcon.SetActive(false);
        if (gameObject.name == "wolfEnemy(Clone)" || gameObject.name == "werewolfEnemy(Clone)")
        {
            myBiteIcon.SetActive(false);
            numAttacks = 1;
        }

        currentBehavior = potentialBehaviors[Random.Range(0, potentialBehaviors.Count)];
        if (currentBehavior == "Attack")
        {
            myAttackBar.SetActive(true);
            numAttacks = setAttacksNum;
            setPower();
        }
        if (currentBehavior == "Ritual")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Ritual";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Performs a ritual worshipping the dungeon, channeling its magic to gain extra attacks";

            setAttacksNum += 1;
            numAttacks = 0;
            setPower();
        }
        if (currentBehavior == "WildHit")
        {
            myAttackBar.SetActive(true);
            numAttacks = Random.Range(1, 4);
            setPower();
        }
        if (currentBehavior == "HealSpell")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Heal Spell";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Heals all allies for a small amount";

            setPower();
            numAttacks = 0;
        }
        if (currentBehavior == "EmpowerSpell")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Empowering Spell";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Empowers all allies, boosting their base damage";

            setPower();
            numAttacks = 0;
        }
        if (currentBehavior == "WeakeningSong")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Weakening Song";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Is playing a mocking tune, you deal half damage this turn";

            numAttacks = 0;
            turnManager.damageDivider = 2;
        }
        if (currentBehavior == "Bleed")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Bleed";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Applies bleed to the player";

            numAttacks = 1;
        }
        if (currentBehavior == "Defend")
        {
            myShieldIcon.SetActive(true);
            numAttacks = 0;
            setShield();
        }
        if (currentBehavior == "Poison")
        {
            numAttacks = 1;
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Poison";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Applies poison to the player";

        }
        if (currentBehavior == "Thief")
        {
            myThiefIcon.SetActive(true);
            numAttacks = 1;
            setPower();
        }
        if (currentBehavior == "Bite")
        {
            myBiteIcon.SetActive(true);
            numAttacks = 1;
            setPower();
        }
        if (currentBehavior == "Charge")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Charge";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Rests this turn, boosting its base damage";
            numAttacks = 0;
            minPower += Random.Range(1, 3);
            maxPower += Random.Range(1, 5);
        }
        if (currentBehavior == "EliteBite")
        {
            myBiteIcon.SetActive(true);
            numAttacks = 1;
            setPower();
        }
        if (currentBehavior == "Howl")
        {
            if(turnManager.spawnedEnemies.Count == 3)
            {
                currentBehavior = "Defend";
                myShieldIcon.SetActive(true);
                numAttacks = 0;
                setShield();
            }
            else if (turnManager.spawnedEnemies.Count != 3)
            {
                myDebuffIcon.SetActive(true);
                myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Howl";
                myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Summons a wolf to aid it's alpha";
                numAttacks = 0;
            }
        }
        if (currentBehavior == "multiLimbAttack")
        {
            myAttackBar.SetActive(true);
            numAttacks = Random.Range(3, 7);
            setPower();
        }
        if (currentBehavior == "StrengtheningRest")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Strengthening Rest";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Raises base power by 1";
            numAttacks = 0;
            minPower += 1;
            maxPower += 1;
            
        }
        if (currentBehavior == "PoisonAttack")
        {
            myAttackBar.SetActive(true);
            numAttacks = 1;
            setPower();
        }
        if (currentBehavior == "BleedAttack")
        {
            myAttackBar.SetActive(true);
            numAttacks = Random.Range(1, 3);
            setPower();
        }
        if (currentBehavior == "Rest")
        {
            myDebuffIcon.SetActive(true);
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Rest";
            myDebuffIcon.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Recovers from its previous attack";
            numAttacks = 0;
            currPower = 0;
        }




        //special enemy behaviors
        if (gameObject.transform.name == "fairyEnemy(Clone)")
        {
            if (turnManager.spawnedEnemies.Count == 1)
            {
                myDebuffIcon.SetActive(false);
                myAttackBar.SetActive(true);
                numAttacks = 1;
                minPower = 1;
                maxPower = 3;
                setPower();
                currentBehavior = "Attack";
            }
        }
        
        if (gameObject.transform.name == "treantBoss(Clone)")
        {
            if(currentShield == 0 && !treantTransformed)
            {
                treantTransformed = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = treantAttackForm;
                gameObject.transform.localScale = new Vector3(.75f, .75f, 1);
                currentBehavior = "Rest";
                numAttacks = 0;
                currPower = 0;
                minPower = 2;
                maxPower = 4;
                potentialBehaviors[0] = "BleedAttack";
                potentialBehaviors.RemoveAt(1);

            }
        }
    }


    public IEnumerator AttackAnimation()
    {
        

        if (currentBehavior == "HealSpell" && !attackEffectDone)
        {
            foreach (var enemy in turnManager.spawnedEnemies)
            {
                attackEffectDone = true;

                enemy.GetComponent<EnemyManager>().enemyHealsDamage(currPower);
            }
        }
        else if (currentBehavior == "EmpowerSpell" && !attackEffectDone)
        {
            attackEffectDone = true;

            foreach (var enemy in turnManager.spawnedEnemies)
            {
                enemy.GetComponent<EnemyManager>().minPower += currPower;
                enemy.GetComponent<EnemyManager>().maxPower += currPower;
            }
        }
        
        else if (currentBehavior == "Howl" && !attackEffectDone)
        {
            attackEffectDone = true;

            turnManager.spawnNewEnemy(wolfMinion);
        }
        
        if (numAttacks == 0)
        {

            attackAnimationDone = true;
            turnManager.enemyAttacking = false;
        }
        else
        {
            attackAnimationDone = false;
        }
        if (!attackAnimationDone)
        {
            if (!reachedAttackLeftPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x <= originalXPosition - .5f)
                {
                    reachedAttackLeftPoint = true;

                    tempPlayerClawPNG = Instantiate(playerClawMarkPNG, new Vector3(-4.11180019f, -0.364600003f, -9), Quaternion.Euler(new Vector3(0, 0, Random.Range(clawMarkPNG.transform.rotation.z - 30f, clawMarkPNG.transform.rotation.z + 30f))));

                    if (currentBehavior == "Attack" || currentBehavior == "WildHit" || currentBehavior == "multiLimbAttack" && !attackEffectDone)
                    {
                        enemyDealsDamage();
                    }
                    else if (currentBehavior == "Bleed" && !attackEffectDone)
                    {

                        playerManager.playerBleedControl(2);
                    }
                    else if (currentBehavior == "Poison" && !attackEffectDone)
                    {

                        playerManager.playerPoisonControl(2);
                    }
                    else if (currentBehavior == "Thief" && !attackEffectDone)
                    {

                        enemyDealsDamage();
                        if (playerManager.playerGold >= currPower)
                        {
                            goldDrop += currPower;
                            maxPower += currPower;
                            minPower += currPower;
                            playerManager.playerGoldChange(-currPower);

                        }
                        else if (playerManager.playerGold < currPower)
                        {
                            goldDrop += playerManager.playerGold;
                            maxPower += playerManager.playerGold;
                            minPower += playerManager.playerGold;
                            playerManager.playerGoldChange(-playerManager.playerGold);

                        }

                    }
                    else if (currentBehavior == "Bite" && !attackEffectDone)
                    {

                        enemyDealsDamage();
                        enemyHealsDamage(Mathf.FloorToInt(currPower / 2));
                    }
                    else if (currentBehavior == "EliteBite" && !attackEffectDone)
                    {

                        enemyDealsDamage();
                        enemyHealsDamage(currPower);
                    }
                    else if (currentBehavior == "PoisonAttack" && !attackEffectDone)
                    {

                        enemyDealsDamage();
                        playerManager.playerPoisonControl(Random.Range(1, 3));
                    }
                    else if (currentBehavior == "BleedAttack" && !attackEffectDone)
                    {

                        enemyDealsDamage();
                        playerManager.playerBleedControl(1 * numAttacks);
                    }

                }
            }
            else if (reachedAttackLeftPoint && !reachedAttackRightPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x >= originalXPosition - .2f && gameObject.transform.position.x <= originalXPosition + .2f)
                {

                    Destroy(tempPlayerClawPNG);
                    
                    //tempClawMarkPNG = null;
                    reachedAttackLeftPoint = false;
                    reachedAttackRightPoint = false;
                    attackAnimationDone = true;
                    gameObject.transform.position = new Vector3(originalXPosition, gameObject.transform.position.y, gameObject.transform.position.z);
                }
            }
        }

        yield return new WaitForSecondsRealtime(0.01f);

        if (attackAnimationDone)
        {
            currAttacksDone += 1;
            if (numAttacks > currAttacksDone)
            {
                attackAnimationDone = false;
                reachedAttackLeftPoint = false;
                reachedAttackRightPoint = false;

                StartCoroutine(AttackAnimation());
            }
            else if (numAttacks <= currAttacksDone || numAttacks == 0)
            {
                turnManager.enemyAttacking = false;
                currAttacksDone = 0;
                attackAnimationDone = true;
                attackEffectDone = false;
                StopCoroutine(AttackAnimation());
            }


        }
        else if (!attackAnimationDone)
        {
            StartCoroutine(AttackAnimation());
        }


        /*
        //reduces health
        if (animationDone == true && damageHits.Count != 0)
        {
            //currentHealth -= damage;


            myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
            damageHits.RemoveAt(0);

            animationDone = false;
        }

        //StartCoroutine(damageMovement());

        //moving animation
        else if (!animationDone)
        {
            if (tempClawMarkPNG == null)
            {
                tempClawMarkPNG = Instantiate(clawMarkPNG, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(clawMarkPNG.transform.rotation.z - 30f, clawMarkPNG.transform.rotation.z + 30f))));
            }
            if (!reachedLeftPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x <= originalXPosition - .2f)
                {
                    reachedLeftPoint = true;
                }
            }
            else if (reachedLeftPoint && !reachedRightPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x >= originalXPosition + .2f)
                {
                    reachedRightPoint = true;
                }
            }
            else if (reachedLeftPoint && reachedRightPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x >= originalXPosition - .2f && gameObject.transform.position.x <= originalXPosition + .2f)
                {
                    Destroy(tempClawMarkPNG);
                    tempClawMarkPNG = null;
                    reachedLeftPoint = false;
                    reachedRightPoint = false;
                    animationDone = true;
                    gameObject.transform.position = new Vector3(originalXPosition, gameObject.transform.position.y, gameObject.transform.position.z);


                }
            }
        }






        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            killEnemy();
        }


        if (animationDone && damageHits.Count != 0)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }


        //if animation and attacking is done
        if (damageHits.Count == 0 && animationDone)
        {
            reachedLeftPoint = false;
            reachedRightPoint = false;
            coroutineRunning = false;
            damageHits.Clear();
            turnManager.attacksFinishedAmount += 1;
            StopAllCoroutines();
        }
        // if attacking is done but animation is still going
        else if (damageHits.Count == 0 && !animationDone)
        {
            StartCoroutine(DamageAnimation(0));

        }
        //continues normally
        else
        {
            StartCoroutine(DamageAnimation(damageHits[0]));
        }*/


    }


}
