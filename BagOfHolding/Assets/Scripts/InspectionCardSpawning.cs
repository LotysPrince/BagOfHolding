using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCardSpawning : MonoBehaviour
{

    private Vector2 mousePosition;
    public bool stopFollowMouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollowMouse)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePosition.x + 1.5f, mousePosition.y + 1.5f, -5);
        }
    }
}
