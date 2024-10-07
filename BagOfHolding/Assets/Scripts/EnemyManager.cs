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


    private GameObject myHealthBar;
    public GameObject healthBarObject;

    private GameObject myAttackBar;
    public GameObject attackBarObject;

    public GameObject bleedObject;
    public GameObject myBleedNum;

    private TurnManager turnManager;
    public bool isTargetted = false;
    public GameObject playerObject;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("ya");
        Debug.Log(coroutineRunning);
        Debug.Log(damageHits.Count);
        Debug.Log(damageHits[0]);
        if (coroutineRunning == false && damageHits.Count != 0 && damageHits[0] != 0)
        {
            Debug.Log("yayayayaya");
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

    public void enemyDealsDamage()
    {
        //reduces bleed amount here instead of taking damage function because that one is only run when theyre targetted
        if (currBleed > 0)
        {
            currBleed -= 1;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();
            if (currBleed == 0)
            {
                Destroy(myBleedNum);
            }
        }
        /////////////////////////////////////////////////
        playerManager.playerTakesDamage(currPower);
    }

    public void setBleed(int addBleed)
    {
        if (addBleed != 0)
        {
            if (currBleed == 0)
            {
                GameObject enemyBleedBar = Instantiate(bleedObject, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myBleedNum = enemyBleedBar;

            }
            currBleed += addBleed;
            myBleedNum.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currBleed.ToString();
        }


    }

    public void killEnemy()
    {
        turnManager.spawnedEnemies.RemoveAt(0);
        Destroy(myHealthBar);
        Destroy(myAttackBar);
        Destroy(myBleedNum);
        Destroy(gameObject);

    }

    private void OnMouseDown()
    {
        turnManager.enemySelector(gameObject);
    }

    private IEnumerator DamageAnimation(int damage)
    {
        //reduces health
        if (animationDone == true && damageHits.Count != 0)
        {
            currentHealth -= damage;


            myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
            damageHits.RemoveAt(0);

            animationDone = false;
        }

        //StartCoroutine(damageMovement());

        //moving animation
        else if(!animationDone)
        {
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
                    reachedLeftPoint = false;
                    reachedRightPoint = false;
                    animationDone = true;
                    gameObject.transform.position = new Vector3(originalXPosition, gameObject.transform.position.y, gameObject.transform.position.z);


                }
            }
        }






        if (currentHealth <= 0)
        {
            killEnemy();
            StopAllCoroutines();
        }



        yield return new WaitForSecondsRealtime(0.01f);


        //if animation and attacking is done
        if (damageHits.Count == 0 && animationDone)
        {
            reachedLeftPoint = false;
            reachedRightPoint = false;
            coroutineRunning = false;
            damageHits.Clear();
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
        }

        
    }


}
