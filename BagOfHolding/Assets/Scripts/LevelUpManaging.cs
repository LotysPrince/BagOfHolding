using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManaging : MonoBehaviour
{
    public PlayerManager playerManager;
    public TurnManager turnManager;
    public GameObject levelUpScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.transform.name == "Perk 1-1")
        {
            turnManager.baseDamage += 2;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 1-2")
        {
            turnManager.inflictBleedMin += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 2-1")
        {
            turnManager.baseHandSize += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 2-2")
        {
            //WIP
        }
        if (gameObject.transform.name == "Perk 3-1")
        {
            turnManager.baseDamage += 2;
            levelUpScreen.SetActive(false);

        }
        if (gameObject.transform.name == "Perk 3-2")
        {
            turnManager.inflictBleedMin += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 4-1")
        {
            turnManager.baseTargetLimit += 1;
        }
        if (gameObject.transform.name == "Perk 4-2")
        {
            turnManager.numAttacksMin += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 5-1")
        {
            turnManager.baseHandSize += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 5-2")
        {
            //Wip
        }
        if (gameObject.transform.name == "Perk 6-1")
        {

        }
        if (gameObject.transform.name == "Perk 6-2")
        {
            turnManager.inflictBleedMin += 1;
            levelUpScreen.SetActive(false);
        }
    }
}
