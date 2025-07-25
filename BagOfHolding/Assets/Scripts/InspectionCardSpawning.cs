using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCardSpawning : MonoBehaviour
{

    private Vector2 mousePosition;
    public bool stopFollowMouse;

    public bool isStatDisplay;

    public GameObject bleedPopup;
    public GameObject poisonPopup;
    public GameObject critPopup;
    public GameObject dodgePopup;
    public GameObject hemmorhagePopup;
    public GameObject rejuvenatePopup;
    public GameObject slickPopup;
    public GameObject stickyPopup;

    public bool triggerBleedPopup;
    public bool triggerPoisonPopup;
    public bool triggerCritPopup;
    public bool triggerDodgePopup;
    public bool triggerHemmorhagePopup;
    public bool triggerRejuvenatePopup;
    public bool triggerSlickPopup;
    public bool triggerStickyPopup;

    private float popupYvalue;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetSiblingIndex(0);
        gameObject.layer = 8;
        popupYvalue = 4;
        if (!stopFollowMouse)
        {
            displayStatPopups();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollowMouse)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.y >= 1.25f)
            {
                mousePosition.y = 1.25f;
            }
            gameObject.transform.position = new Vector3(mousePosition.x + 2f, mousePosition.y + 1.5f, -15);
        }
        /*if (isStatDisplay)
        {
            //gameObject.GetComponent<RectTransform>().anchoredPosition =
            //    new Vector2(< value4Posx >, < value4Posy >;
            gameObject.GetComponent<RectTransform>().localPosition =
                 new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, gameObject.GetComponent<RectTransform>().localPosition.y, 0);

            gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition =
                 new Vector3(gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition.x,
                 gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition.y,
                 gameObject.transform.parent.position.z - 1);

            gameObject.transform.GetChild(0).gameObject.layer = 9;

            gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition =
                new Vector3(gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition.x,
                gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>().localPosition.y,
                gameObject.transform.parent.position.z - 1);
            gameObject.transform.GetChild(0).gameObject.layer = 9;

            Destroy(gameObject.transform.GetChild(1));
            Destroy(gameObject.transform.GetChild(0));

        }*/
    }

    private void displayStatPopups()
    {
        GameObject newPopup;
        if (triggerBleedPopup)
        {
            newPopup = Instantiate(bleedPopup, new Vector3(2, popupYvalue, 0), bleedPopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);

            popupYvalue -= 4;
        }
        if (triggerPoisonPopup)
        {
            newPopup = Instantiate(poisonPopup, new Vector3(2, popupYvalue, 0), poisonPopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerCritPopup)
        {
            newPopup = Instantiate(critPopup, new Vector3(2, popupYvalue, 0), critPopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerDodgePopup)
        {
            newPopup = Instantiate(dodgePopup, new Vector3(2, popupYvalue, 0), dodgePopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerHemmorhagePopup)
        {
            newPopup = Instantiate(hemmorhagePopup, new Vector3(2, popupYvalue, 0), hemmorhagePopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerRejuvenatePopup)
        {
            newPopup = Instantiate(rejuvenatePopup, new Vector3(2, popupYvalue, 0), rejuvenatePopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerSlickPopup)
        {
            newPopup = Instantiate(slickPopup, new Vector3(2, popupYvalue, 0), slickPopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
        if (triggerStickyPopup)
        {
            newPopup = Instantiate(stickyPopup, new Vector3(2, popupYvalue, 0), stickyPopup.transform.rotation);
            newPopup.transform.SetParent(gameObject.transform);
            newPopup.transform.localPosition = new Vector3(20, popupYvalue, 0);


            popupYvalue -= 4;
        }
    }
}
