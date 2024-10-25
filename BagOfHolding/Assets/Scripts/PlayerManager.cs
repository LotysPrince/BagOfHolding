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
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
        myCritObject = Instantiate(CritObject, transform.position + new Vector3(-3f, 2.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
        currentCrit = turnManager.currentCritical;
    }

    // Update is called once per frame
    void Update()
    {
        currentCrit = turnManager.currentCritical;
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

    public void killPlayer()
    {

    }
}
