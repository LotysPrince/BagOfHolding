using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFading : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float transparencyVal;
    public float transparencySpeed;
    public bool bleedSpriteGrowing;
    public Vector3 bleedSpriteFullScale;
    public SpriteMask spriteMaskPrefab;
    public bool bleedingEffect;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //transparencyVal = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!bleedSpriteGrowing)
        {
            spriteRenderer.color = new Color(1, 1, 1, transparencyVal);
            transparencyVal -= transparencySpeed * Time.deltaTime;
            
            if (bleedingEffect)
            {
                gameObject.transform.localScale += new Vector3(.01f, .01f, 0) * Time.deltaTime;
            }
            if (transparencyVal <= 0)
            {
                Destroy(spriteMaskPrefab);
                Destroy(gameObject);
            }

        }
        if (bleedSpriteGrowing)
        {
            //gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
            spriteMaskPrefab.gameObject.transform.localScale += new Vector3(25f, 25f, 0) * Time.deltaTime;
            if (spriteMaskPrefab.gameObject.transform.localScale.x >= 5)
            //if (gameObject.transform.localScale.x >=  bleedSpriteFullScale.x)
            {
                gameObject.transform.localScale = bleedSpriteFullScale;
                bleedSpriteGrowing = false;
            }
        }
    }
}
