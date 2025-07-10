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
            turnManager.numAttacksMin += 1;
            levelUpScreen.SetActive(false);
        }
        if (gameObject.transform.name == "Perk 1-2")
        {
            turnManager.inflictBleedMin += 1;
            levelUpScreen.SetActive(false);
        }
    }
}
