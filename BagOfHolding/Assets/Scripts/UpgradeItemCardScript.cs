using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemCardScript : MonoBehaviour
{
    public GameObject rootCard;
    public UpgradeItemScreenGen upgradeScreen;
    public void OnMouseDown()
    {
        if (upgradeScreen.currentDisplayedCard != null)
        {
            Destroy(upgradeScreen.currentDisplayedCard);
        }
        GameObject duplicate = Instantiate(gameObject, new Vector3(-16.2f, -32.5f, -5), Quaternion.identity);
        var cardSprite = duplicate.transform.Find("CardFrame");
        if (cardSprite != null)
        {
            cardSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GoldV1");
            cardSprite.transform.Find("Card Description").transform.gameObject.SetActive(false);
            cardSprite.transform.Find("Card Description (1)").transform.gameObject.SetActive(true);
        }
        cardSprite = duplicate.transform.Find("CardFrame (1)");
        if (cardSprite != null)
        {
            cardSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GoldV1");
            cardSprite.transform.Find("Card Description").transform.gameObject.SetActive(false);
            cardSprite.transform.Find("Card Description (1)").transform.gameObject.SetActive(true);
        }
        upgradeScreen.currentDisplayedCard = duplicate;

    }
}
