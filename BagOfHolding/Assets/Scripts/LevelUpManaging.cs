using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManaging : MonoBehaviour
{
    public PlayerManager playerManager;
    public TurnManager turnManager;
    public GameObject levelUpScreen;
    public bool canSelect;
    public GameObject Perk11;
    public GameObject Perk12;
    public GameObject Perk21;
    public GameObject Perk22;
    public GameObject Perk31;
    public GameObject Perk32;
    public GameObject Perk41;
    public GameObject Perk42;
    public GameObject Perk51;
    public GameObject Perk52;
    public GameObject Perk61;
    public GameObject Perk62;
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
        if (canSelect)
        {
            if (gameObject.transform.name == "Perk 1-1")
            {
                turnManager.baseDamage += 2;
                levelUpScreen.SetActive(false);
                Perk21.GetComponent<LevelUpManaging>().canSelect = true;
                Perk21.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk22.GetComponent<LevelUpManaging>().canSelect = true;
                Perk22.GetComponent<Image>().color = new Color(255, 255, 255);

            }
            if (gameObject.transform.name == "Perk 1-2")
            {
                turnManager.inflictBleedMin += 1;
                levelUpScreen.SetActive(false);
                Perk21.GetComponent<LevelUpManaging>().canSelect = true;
                Perk21.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk22.GetComponent<LevelUpManaging>().canSelect = true;
                Perk22.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (gameObject.transform.name == "Perk 2-1")
            {
                turnManager.baseHandSize += 1;
                levelUpScreen.SetActive(false);
                Perk31.GetComponent<LevelUpManaging>().canSelect = true;
                Perk31.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk32.GetComponent<LevelUpManaging>().canSelect = true;
                Perk32.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (gameObject.transform.name == "Perk 2-2")
            {
                //WIP
            }
            if (gameObject.transform.name == "Perk 3-1")
            {
                turnManager.baseDamage += 2;
                levelUpScreen.SetActive(false);
                Perk41.GetComponent<LevelUpManaging>().canSelect = true;
                Perk41.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk42.GetComponent<LevelUpManaging>().canSelect = true;
                Perk42.GetComponent<Image>().color = new Color(255, 255, 255);

            }
            if (gameObject.transform.name == "Perk 3-2")
            {
                turnManager.inflictBleedMin += 1;
                levelUpScreen.SetActive(false);
                Perk41.GetComponent<LevelUpManaging>().canSelect = true;
                Perk41.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk42.GetComponent<LevelUpManaging>().canSelect = true;
                Perk42.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (gameObject.transform.name == "Perk 4-1")
            {
                turnManager.baseTargetLimit += 1;
                levelUpScreen.SetActive(false);
                Perk51.GetComponent<LevelUpManaging>().canSelect = true;
                Perk51.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk52.GetComponent<LevelUpManaging>().canSelect = true;
                Perk52.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (gameObject.transform.name == "Perk 4-2")
            {
                turnManager.numAttacksMin += 1;
                levelUpScreen.SetActive(false);
                Perk51.GetComponent<LevelUpManaging>().canSelect = true;
                Perk51.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk52.GetComponent<LevelUpManaging>().canSelect = true;
                Perk52.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            if (gameObject.transform.name == "Perk 5-1")
            {
                turnManager.baseHandSize += 1;
                levelUpScreen.SetActive(false);
                Perk61.GetComponent<LevelUpManaging>().canSelect = true;
                Perk61.GetComponent<Image>().color = new Color(255, 255, 255);
                Perk62.GetComponent<LevelUpManaging>().canSelect = true;
                Perk62.GetComponent<Image>().color = new Color(255, 255, 255);
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
}
