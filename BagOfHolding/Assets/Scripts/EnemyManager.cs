using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int minPower;
    public int maxPower;
    public int currPower;
    public int currBleed;
    public int currPoison;
    public int currHemmorhage;

    //variables for taking damage over time
    private bool coroutineRunning;
    public int timesHit;
    private int currentTimesHit;
    public List<int> damageHits = new List<int>();

    //variables for damage animation
    private float originalXPosition;
    private float xMoveSpeed = .1f;
    private bool reachedLeftPoint;
    private bool reachedRightPoint;
    private bool animationDone;
    private bool statusesDealt;


    private GameObject myHealthBar;
    public GameObject healthBarObject;

    private GameObject myAttackBar;
    public GameObject attackBarObject;

    public GameObject bleedObject;
    public GameObject myBleedNum;

    public GameObject poisonObject;
    public GameObject myPoisonNum;

    public GameObject hemmorhageObject;
    public GameObject myHemmorhageNum;

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
        setPower();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPower()
    {
        currPower = Random.Range(minPower, maxPower);
        myAttackBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currPower.ToString();

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

    public void enemyStatusControl()
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
            turnManager.trackEnemyAttackFinishes();
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

    }

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

    public void setBleed(int addBleed)
    {
        if (addBleed != 0)
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


    }

    public void setPoison(int addPoison)
    {
        if (addPoison != 0)
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


    }
    public void setHemmorhage(int addHemmorhage)
    {
        if (addHemmorhage != 0)
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


    }


    public void killEnemy()
    {
        if (turnManager.tempList == turnManager.spawnedEnemies)
        {
            turnManager.tempList.Remove(gameObject);
            turnManager.spawnedEnemies = turnManager.tempList;
        }
        else if (turnManager.tempList == turnManager.targettedEnemies)
        {
            turnManager.tempList.Remove(gameObject);
            turnManager.targettedEnemies = turnManager.tempList;
        }
        playerManager.playerExpGain(expDrop);
        playerManager.playerGoldChange(goldDrop);
        Destroy(myHealthBar);
        Destroy(myAttackBar);
        Destroy(myBleedNum);
        Destroy(myPoisonNum);
        Destroy(myHemmorhageNum);
        Destroy(gameObject);

    }

    private void OnMouseDown()
    {
        turnManager.enemySelector(gameObject);
    }

    private IEnumerator DamageAnimation(int damage)
    {
        //Debug.Log(damage);
        //reduces health
        if (animationDone == true && damageHits.Count != 0)
        {
            //Debug.Log("damage Dealt");
            currentHealth -= damage;


            myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
            damageHits.RemoveAt(0);

            //if trap damage on the field, player takes damage
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
            if (tempClawMarkPNG == null)
            {
                if (statusesDealt == false && statusDamage == false)
                {
                    setBleed(turnManager.inflictBleed);
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
                turnManager.trackEnemyAttackFinishes();
            }

            else
            {
                damagedByTrap = false;
            }

            if (currentHealth <= 0)
            {
                turnManager.trackEnemyAttackFinishes();
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



    public IEnumerator AttackAnimation()
    {
        if (!attackAnimationDone)
        {
            if (!reachedAttackLeftPoint)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - xMoveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x <= originalXPosition - .5f)
                {
                    reachedAttackLeftPoint = true;
                    tempPlayerClawPNG = Instantiate(playerClawMarkPNG, new Vector3(-4.11180019f, -0.364600003f, -9), Quaternion.Euler(new Vector3(0, 0, Random.Range(clawMarkPNG.transform.rotation.z - 30f, clawMarkPNG.transform.rotation.z + 30f))));
                    enemyDealsDamage();
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

            turnManager.enemyAttacking = false;
            StopCoroutine(AttackAnimation());
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
