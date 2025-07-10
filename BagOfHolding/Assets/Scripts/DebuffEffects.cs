using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffEffects : MonoBehaviour
{
    public EnemyManager enemyManager;
    private bool isBleeding;
    private bool isPoisoned;

    public GameObject bleedSplatterImage;
    public SpriteMask spriteMaskPrefab;
    public bool bleedAnimationRunning;

    //private int[2] randomRotations = [0,180];
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = gameObject.GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.currBleed > 0)
        {
            isBleeding = true;
        }
        else if (enemyManager.currBleed == 0)
        {
            isBleeding = false;
        }


        if (isBleeding && !bleedAnimationRunning)
        {
            bleedAnimationRunning = true;
            StartCoroutine(BleedAnimation());

        }
        if (!isBleeding)
        {
            bleedAnimationRunning = false;
            StopCoroutine(BleedAnimation());
        }
    }

    public IEnumerator BleedAnimation()
    {
        //creates new sprite with random rotation and flips randomly, and random position close to center of enemy

        var newBleedSprite = Instantiate(bleedSplatterImage
            , gameObject.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0)
            , Quaternion.Euler(new Vector3(Random.Range(0, 2) == 0 ? 0 : 180, Random.Range(0, 2) == 0 ? 0 : 180, Random.Range(0f, 360f))));
        var randomScale = Random.Range(0.2f, 0.5f);
        newBleedSprite.gameObject.transform.localScale = new Vector3(randomScale, randomScale, 1);

        newBleedSprite.GetComponent<spriteFading>().bleedSpriteGrowing = true;
        newBleedSprite.GetComponent<spriteFading>().bleedingEffect = true;
        newBleedSprite.GetComponent<spriteFading>().bleedSpriteFullScale = newBleedSprite.gameObject.transform.localScale;
        //newBleedSprite.gameObject.transform.localScale = new Vector3(0, 0, 1);

        var newSpriteMask = Instantiate(spriteMaskPrefab, newBleedSprite.gameObject.transform.position, Quaternion.identity);
        newBleedSprite.GetComponent<spriteFading>().spriteMaskPrefab = newSpriteMask;
        yield return new WaitForSecondsRealtime(Random.Range(2f, 4f));

        if (isBleeding)
        {
            StartCoroutine(BleedAnimation());
        }
        if (!isBleeding)
        {
            StopCoroutine(BleedAnimation());
        }

    }
}
