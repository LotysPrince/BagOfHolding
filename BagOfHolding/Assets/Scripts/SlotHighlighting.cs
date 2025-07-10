using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHighlighting : MonoBehaviour
{
    private Transform highlightSprite;
    private SpriteRenderer highlightRenderer;
    private bool opacityUp;
    private Color originalColor;
    private float opacity;
    // Start is called before the first frame update
    void Start()
    {
        highlightSprite = gameObject.transform.GetChild(0);
        highlightRenderer = highlightSprite.GetComponent<SpriteRenderer>();
        originalColor = highlightRenderer.color;
        opacityUp = true;
        opacity = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator highlightAnimation()
    {
        if (opacityUp) 
        {
            opacity += 0.01f;
            if (opacity >= 1)
            {
                opacityUp = false;
            }
        }
        if (!opacityUp)
        {
            opacity -= 0.01f;

            if (opacity <= 0)
            {
                opacityUp = true;
            }
        }
        highlightRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
        yield return new WaitForSecondsRealtime(.001f);

        StartCoroutine(highlightAnimation());

    }

    public void startAnimation()
    {
        StartCoroutine(highlightAnimation());
    }

    public void StopAnimation()
    {
        StopAllCoroutines();
        highlightRenderer.color = originalColor;

    }
}
