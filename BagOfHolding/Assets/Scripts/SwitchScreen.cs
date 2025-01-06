using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{

    public string currentScreen;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        currentScreen = "Map";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScreen(string newScreen)
    {
        if (newScreen == "Map")
        {
            cam.transform.position = new Vector3(0, -17f, -10);
            currentScreen = "Map";
        }
        else if (newScreen == "Battle")
        {
            cam.transform.position = new Vector3(0, 0f, -10);
            currentScreen = "Battle";
        }
        else if (newScreen == "Shop")
        {
            cam.transform.position = new Vector3(0, -34f, -10);
            currentScreen = "Shop";
        }
    }

    public void returnToMap()
    {
        if (currentScreen == "Map")
        {
            cam.transform.position = new Vector3(0, 0f, -10);
            currentScreen = "Battle";
        }
        else if (currentScreen == "Battle")
        {
            cam.transform.position = new Vector3(0, -17f, -10);
            currentScreen = "Map";
        }
        else if (currentScreen == "Shop")
        {
            cam.transform.position = new Vector3(0, -17f, -10);
            currentScreen = "Map";
        }
    }
}
