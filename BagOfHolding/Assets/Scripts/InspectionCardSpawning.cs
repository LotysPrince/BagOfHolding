using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCardSpawning : MonoBehaviour
{

    private Vector2 mousePosition;
    public bool stopFollowMouse;

    public bool isStatDisplay;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetSiblingIndex(0);
        gameObject.layer = 8;
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
            gameObject.transform.position = new Vector3(mousePosition.x + 2f, mousePosition.y + 1.5f, -11);
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
}
