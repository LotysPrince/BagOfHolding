using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegmentControl : MonoBehaviour
{
    public bool leftExit;
    public bool rightExit;
    public bool topExit;
    public bool bottomExit;
    public int numExits;

    public bool leftExitOpen;
    public bool rightExitOpen;
    public bool topExitOpen;
    public bool bottomExitOpen;

    public int xArrayPos;
    public int yArrayPos;

    public int numExitsLeft;

    public string roomInhabitedBy;
    public GameObject currentInhabitant;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeExit(string exit)
    {
        if (leftExitOpen == true && exit == "Left")
        {
            leftExitOpen = false;
            numExitsLeft -= 1;
        }
        else if (rightExitOpen == true && exit == "Right")
        {
            rightExitOpen = false;
            numExitsLeft -= 1;

        }
        else if (topExitOpen == true && exit == "Top")
        {
            topExitOpen = false;
            numExitsLeft -= 1;

        }
        else if (bottomExitOpen == true && exit == "Bottom")
        {
            bottomExitOpen = false;
            numExitsLeft -= 1;

        }
    }
}
