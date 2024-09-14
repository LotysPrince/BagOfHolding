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

    public void enemyTakesDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth -= currBleed;



        myHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            killEnemy();
        }
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



}
