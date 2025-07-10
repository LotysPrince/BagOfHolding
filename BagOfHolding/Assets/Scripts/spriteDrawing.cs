using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteDrawing : MonoBehaviour
{
    public SpriteMask maskPrefab;
    public GameObject topLeftMarker;
    public GameObject bottomRightMarker;

    private float topYValue;
    private float bottomYValue;
    private float yPos;
    private float xPos;
    private bool drawingDown;

    public GameObject quill;
    private GameObject quillImg;
    public GameObject spriteMasksParent;
    public GameObject inkSplotch;
    private float inkSplotchYPos = -1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        topYValue = topLeftMarker.transform.TransformPoint(Vector3.zero).y;
        bottomYValue = bottomRightMarker.transform.TransformPoint(Vector3.zero).y;
        yPos = topYValue;
        xPos = topLeftMarker.transform.TransformPoint(Vector3.zero).x;
        drawingDown = true;
        //var newItem = Instantiate(maskPrefab, new Vector3(topLeftMarker.transform.position.x, topLeftMarker.transform.position.y, topLeftMarker.transform.position.z), Quaternion.identity);
        //newItem.transform.SetParent(spriteMasksParent.transform);
        quillImg = Instantiate(quill, new Vector3(topLeftMarker.transform.position.x, topLeftMarker.transform.position.y, topLeftMarker.transform.position.z), quill.transform.rotation);

        //StopAllCoroutines();
        StartCoroutine(StartDrawing());
    }


    // Update is called once per frame
    void Update()
    {

    }




    IEnumerator StartDrawing()
    {
        if (drawingDown)
        {
            yPos -= 0.3f;

            if (yPos < bottomYValue)
            {
                xPos += .5f;
                drawingDown = false;
            }
        }

        else if (!drawingDown)
        {
            yPos += 0.3f;

            if (yPos > topYValue)
            {
                xPos += .5f;
                drawingDown = true;
            }
        }

        if (xPos < bottomRightMarker.transform.TransformPoint(Vector3.zero).x)
        {
            var newItem = Instantiate(maskPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
            newItem.transform.SetParent(spriteMasksParent.transform);

            var newInkSplotch = Instantiate(inkSplotch, new Vector3(xPos, yPos, inkSplotchYPos), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //newInkSplotch.transform.SetParent(spriteMasksParent.transform);
            inkSplotchYPos -= 0.01f;

            quillImg.transform.position = new Vector3(xPos + 5f, yPos + 3.5f, -5);
        }

        
        yield return new WaitForSecondsRealtime(0.01f);

        if (xPos > bottomRightMarker.transform.TransformPoint(Vector3.zero).x)
        {
            gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            Destroy(spriteMasksParent);
            StopAllCoroutines();
            Destroy(quillImg);

        }
        else
        {
            StartCoroutine(StartDrawing());
        }

    }
}
