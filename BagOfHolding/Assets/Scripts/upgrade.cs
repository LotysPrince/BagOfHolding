using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour
{
    public UpgradeItemScreenGen upgradeScreen;
    public SwitchScreen switchScreen;
    private void OnMouseDown()
    {
        upgradeScreen.currentDisplayedCard.GetComponent<UpgradeItemCardScript>().rootCard.GetComponent<CardManager>().isUpgraded = true;
        switchScreen.returnToMap();

    }
}
