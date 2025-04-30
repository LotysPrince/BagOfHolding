using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public GameObject playerHealthBar;
    public TurnManager turnManager;
    private float currentCrit;

    private GameObject myCritObject;
    public GameObject CritObject;

    private GameObject myBleedObject;
    public GameObject bleedObject;

    public int playerArmor;
    public int playerDodge;

    public int playerLevel;
    public int playerExp;
    public int playerMaxExp;

    public int playerGold;

    public GameObject playerLevelUI;
    public GameObject playerExpUI;
    public GameObject playerGoldUI;

    public int selfBleed;

    // Start is called before the first frame update
    void Start()
    {
        playerGold = 10;
        playerMaxExp = 20;
        playerHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
        myCritObject = Instantiate(CritObject, transform.position + new Vector3(-3f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        currentCrit = turnManager.currentCritical;
    }

    // Update is called once per frame
    void Update()
    {
        currentCrit = turnManager.criticalAttacks;
        if(currentCrit <= 1)
        {
            myCritObject.SetActive(false);
        }
        else
        {
            myCritObject.SetActive(true);
            myCritObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentCrit.ToString();

        }
    }

    public void playerTakesDamage(int damage)
    {
        if (playerDodge != 0)
        {
            damage = 0;
            playerDodge -= 1;
        }
        else if (playerArmor != 0)
        {
            if (playerArmor >= damage)
            {
                playerArmor -= damage;
                damage = 0;
            }
            else if (playerArmor < damage)
            {
                damage -= playerArmor;
                playerArmor = 0;
            }
        }
        
        currentHealth -= damage;
        playerHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
    }

    public void playerHealsDamage(int healing)
    {
        currentHealth += healing;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        playerHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();

    }

    public void playerBleedControl(int bleedAdd)
    {
        if (bleedAdd != 0 && bleedAdd > 0)
        {
            if (selfBleed == 0)
            {
                GameObject enemyBleedBar = Instantiate(bleedObject, transform.position + new Vector3(-2.5f, 2.5f, -5f), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
                myBleedObject = enemyBleedBar;

            }
            selfBleed += bleedAdd;
            myBleedObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selfBleed.ToString();

            
        }
        else if (bleedAdd != 0 && bleedAdd < 0)
        {
            if (selfBleed != 0)
            {
                selfBleed += bleedAdd;
                myBleedObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selfBleed.ToString();
                if (selfBleed <= 0)
                {
                    Destroy(myBleedObject);
                }
            }
        }
    }

    public void playerGoldChange(int gold)
    {
        playerGold += gold;
        playerGoldUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerGold.ToString();

    }


    public void playerExpGain(int exp)
    {
        playerExp += exp;
        playerExpUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerExp + "/" + playerMaxExp;
        if (playerExp >= playerMaxExp)
        {
            int expOverflow = playerExp - playerMaxExp;
            playerLevel += 1;
            playerExp = expOverflow;
            playerLevelUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Lvl " + playerLevel.ToString();


            if (playerLevel == 1)
            {
                playerMaxExp = 35;
            }
            if (playerLevel == 2)
            {
                playerMaxExp = 50;
            }
            if (playerLevel == 3)
            {
                playerMaxExp = 125;
            }
            if (playerLevel == 4)
            {
                playerMaxExp = 313;
            }
            if (playerLevel == 5)
            {
                playerMaxExp = 783;
            }
            if (playerLevel == 6)
            {
                playerMaxExp = 1957;
            }
            if (playerLevel == 7)
            {
                playerMaxExp = 4893;
            }
            if (playerLevel == 8)
            {
                playerMaxExp = 12232;
            }
            if (playerLevel == 9)
            {
                playerMaxExp = 30580;
            }
            if (playerLevel == 10)
            {
                playerMaxExp = 100000000;
            }
            playerExpUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerExp + "/" + playerMaxExp;

            if (playerLevel == 10)
            {
                playerExpUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerExp + "/" + "Inf";
            }

        }
    }


    public void killPlayer()
    {

    }
}
