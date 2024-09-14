using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public GameObject playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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
