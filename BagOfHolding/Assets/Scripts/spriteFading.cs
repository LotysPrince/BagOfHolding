using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFading : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float transparencyVal;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        transparencyVal = 1;

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color(1, 1, 1, transparencyVal);
        transparencyVal -= 1f * Time.deltaTime;
        if (transparencyVal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
