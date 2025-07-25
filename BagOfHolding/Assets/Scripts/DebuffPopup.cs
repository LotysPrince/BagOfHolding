using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }
}
