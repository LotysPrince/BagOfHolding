using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject mapSegment;

    public GameObject TLBRmap;
    public GameObject Tmap;
    public GameObject Bmap;
    public GameObject Lmap;
    public GameObject Rmap;
    public GameObject BLmap;
    public GameObject BRmap;
    public GameObject LRBmap;
    public GameObject LRmap;
    public GameObject TBmap;
    public GameObject TLBmap;
    public GameObject TLmap;
    public GameObject TLRmap;
    public GameObject TRBmap;
    public GameObject TRmap;
    public GameObject blankMap;

    public List<GameObject> allPieces = new List<GameObject>();
    public List<GameObject> topExitPieces = new List<GameObject>();
    public List<GameObject> bottomExitPieces = new List<GameObject>();
    public List<GameObject> rightExitPieces = new List<GameObject>();
    public List<GameObject> leftExitPieces = new List<GameObject>();

    public List<GameObject> spawnedRooms = new List<GameObject>();

    private int arrayXPos = 0;
    private int arrayYPos = 0;

    private int exitsLeft;

    private Vector3 currPiecePos;
    private MapSegmentControl currPieceScript;
    private bool noRoomsAdded = false;

    public EnemyMapBehavior enemyMapBehavior;
    public MapControls mapControls;

    public int mapNumber = 1;
    public bool triggerBoss;




    private bool generationDone;


    public float maxRoomAmount;
    public float minRoomAmount;
    public int currentRoomAmount = 0;

    public float spawnRoomPosIncrement;

    public GameObject[,] spawnedRoomsArray = new GameObject[9,5];

    /*GameObject[] list1 = new GameObject[9] { null, null, null, null, null, null, null, null, null};
    GameObject[] list2 = new GameObject[9] { null, null, null, null, null, null, null, null, null};
    GameObject[] list3 = new GameObject[9] { null, null, null, null, null, null, null, null, null };
    GameObject[] list4 = new GameObject[9] { null, null, null, null, null, null, null, null, null };
    GameObject[] list5 = new GameObject[9] { null, null, null, null, null, null, null, null, null };*/


    void Awake()
    {
        //spawnedRoomsArray = new GameObject[][] { list1, list2, list3, list4, list5};
        allPieces.Add(TLBRmap);
        allPieces.Add(Tmap);
        allPieces.Add(Bmap);
        allPieces.Add(Lmap);
        allPieces.Add(Rmap);
        allPieces.Add(BLmap);
        allPieces.Add(BRmap);
        allPieces.Add(LRBmap);
        allPieces.Add(LRmap);
        allPieces.Add(TBmap);
        allPieces.Add(TLBmap);
        allPieces.Add(TLmap);
        allPieces.Add(TLRmap);
        allPieces.Add(TRBmap);
        allPieces.Add(TRmap);


        topExitPieces.Add(Tmap);
        topExitPieces.Add(TLBRmap);
        topExitPieces.Add(TBmap);
        topExitPieces.Add(TLBmap);
        topExitPieces.Add(TLmap);
        topExitPieces.Add(TLRmap);
        topExitPieces.Add(TRBmap);
        topExitPieces.Add(TRmap);

        bottomExitPieces.Add(Bmap);
        bottomExitPieces.Add(TLBRmap);
        bottomExitPieces.Add(BLmap);
        bottomExitPieces.Add(LRBmap);
        bottomExitPieces.Add(TBmap);
        bottomExitPieces.Add(TRBmap);
        bottomExitPieces.Add(TLBmap);
        bottomExitPieces.Add(BRmap);


        rightExitPieces.Add(Rmap);
        rightExitPieces.Add(TLBRmap);
        rightExitPieces.Add(BRmap);
        rightExitPieces.Add(LRBmap);
        rightExitPieces.Add(LRmap);
        rightExitPieces.Add(TLRmap);
        rightExitPieces.Add(TRBmap);
        rightExitPieces.Add(TRmap);


        leftExitPieces.Add(Lmap);
        leftExitPieces.Add(TLBRmap);
        leftExitPieces.Add(BLmap);
        leftExitPieces.Add(LRBmap);
        leftExitPieces.Add(LRmap);
        leftExitPieces.Add(TLBmap);
        leftExitPieces.Add(TLmap);
        leftExitPieces.Add(TLRmap);


        

        //generateFullMap();

        randomizeMap();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoomAmount < minRoomAmount && generationDone)
        {
            generationDone = false;
            deleteMap();
        }
        else if (currentRoomAmount >= minRoomAmount && generationDone)
        {
            generationDone = false;
            spawnMapIcons();
            mapNumber += 1;
            if (mapNumber == 3)
            {
                minRoomAmount = 7;
                maxRoomAmount = 12;
                triggerBoss = true;
            }
            else
            {
                minRoomAmount = 15;
                maxRoomAmount = 20;
            }
        }
        //if (!createOnce)
        //{
         //   randomizeMap();
          //  createOnce = true;
        //}
    }

    private void randomizeMap()
    {
        var centerPiece = Instantiate(TLBRmap, new Vector3(0f, -17f, -1), Quaternion.identity);
        spawnedRooms.Add(centerPiece);
        spawnedRoomsArray[4,2] = centerPiece;
        centerPiece.GetComponent<MapSegmentControl>().xArrayPos = 4;
        centerPiece.GetComponent<MapSegmentControl>().yArrayPos = 2;
        centerPiece.name = TLBRmap.name;
        var roomCount = 0;
        noRoomsAdded = true;

        checkExitsLeft();

        while (exitsLeft != 0 || currentRoomAmount < maxRoomAmount)
        {
            roomCount = spawnedRooms.Count;
            noRoomsAdded = true;

            for (int i = 0; i < roomCount; i++)
            {

                currPiecePos = spawnedRooms[i].transform.position;
                currPieceScript = spawnedRooms[i].GetComponent<MapSegmentControl>();


                findInRoomArray(spawnedRooms[i]);
                var arrayScript = spawnedRoomsArray[arrayXPos, arrayYPos].GetComponent<MapSegmentControl>();
                
                if (currPieceScript.leftExitOpen && arrayXPos -1 >= 0 && spawnedRoomsArray[arrayXPos -1, arrayYPos] == null && currentRoomAmount < maxRoomAmount)
                {
                    spawnRoomOnLeft();
                    arrayScript.closeExit("Left");
                }
                if (currPieceScript.rightExitOpen && arrayXPos + 1 <= 8 && spawnedRoomsArray[arrayXPos + 1, arrayYPos] == null && currentRoomAmount < maxRoomAmount)
                {
                    spawnRoomOnRight();
                    arrayScript.closeExit("Right");


                }
                if (currPieceScript.topExitOpen && arrayYPos - 1 >= 0 && spawnedRoomsArray[arrayXPos , arrayYPos - 1] == null && currentRoomAmount < maxRoomAmount)
                {
                    spawnRoomAbove();
                    arrayScript.closeExit("Top");

                }

                if (currPieceScript.bottomExitOpen && arrayYPos + 1 <= 4 && spawnedRoomsArray[arrayXPos , arrayYPos + 1] == null && currentRoomAmount < maxRoomAmount)
                {
                    spawnRoomBelow();
                    arrayScript.closeExit("Bottom");

                }

                //break;

                if (currentRoomAmount >= maxRoomAmount)
                {
                    break;
                }
            }
            if (currentRoomAmount >= maxRoomAmount || noRoomsAdded)
            {                
                break;
            }

            
            checkExitsLeft();
        }
        /*if (currentRoomAmount < minRoomAmount)
        {
            randomizeMap();
        }*/
        //else
        //{
        closeOpenExits();
        //}

    }

    private void spawnRoomOnLeft()
    {
        var pieceAbove = blankMap;
        var pieceBelow = blankMap;
        var pieceLeft = blankMap;
        var potentialRooms = rightExitPieces;

        if (arrayYPos - 1 >= 0)
        {
            pieceAbove = spawnedRoomsArray[arrayXPos, arrayYPos -1];

        }
        if (arrayYPos + 1 <= 4)
        {
            pieceBelow = spawnedRoomsArray[arrayXPos, arrayYPos + 1];

        }
        if (arrayXPos - 1 >= 0)
        {
            pieceLeft = spawnedRoomsArray[arrayXPos - 1, arrayYPos];


        }

        
        //if theres a piece above that doesnt have a bottom exit, removes all pieces with top exits from potential spawn
        if (pieceAbove != null && pieceAbove.GetComponent<MapSegmentControl>().bottomExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLRmap);
            potentialRooms.Remove(TRBmap);
            potentialRooms.Remove(TRmap);
        }

        //if piece below doesnt have top exit, removes all bottom pieces
        if (pieceBelow != null && pieceBelow.GetComponent<MapSegmentControl>().topExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(BRmap);
            potentialRooms.Remove(LRBmap);
            potentialRooms.Remove(TRBmap);
        }
        //if pieces left doesnt have a right exit, removes all left exits pieces
        if (pieceLeft != null && pieceLeft.GetComponent<MapSegmentControl>().rightExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLRmap);
            potentialRooms.Remove(LRmap);
            potentialRooms.Remove(LRBmap);
        }

        var randomRange = Random.Range(0, potentialRooms.Count);
        var randomNum = potentialRooms[randomRange];
        var newLeftPiece = Instantiate(randomNum, new Vector3(currPiecePos.x - spawnRoomPosIncrement, currPiecePos.y, -1), potentialRooms[randomRange].transform.rotation);
        //currPieceScript.leftExit = false;
        //currPieceScript.numExitsLeft -= 1;
        spawnedRooms.Add(newLeftPiece);
        spawnedRoomsArray[arrayXPos - 1, arrayYPos] = newLeftPiece;
        newLeftPiece.GetComponent<MapSegmentControl>().closeExit("Right");
        newLeftPiece.GetComponent<MapSegmentControl>().xArrayPos = arrayXPos - 1;
        newLeftPiece.GetComponent<MapSegmentControl>().yArrayPos = arrayYPos;
        newLeftPiece.name = randomNum.name;



        currentRoomAmount += 1;
        noRoomsAdded = false;

    }
    private void spawnRoomOnRight()
    {
        var pieceAbove = blankMap;
        var pieceBelow = blankMap;
        var pieceRight = blankMap;
        var potentialRooms = leftExitPieces;

        if (arrayYPos - 1 >= 0)
        {
            pieceAbove = spawnedRoomsArray[arrayXPos, arrayYPos - 1];

        }
        if (arrayYPos + 1 <= 4)
        {
            pieceBelow = spawnedRoomsArray[arrayXPos, arrayYPos + 1];

        }
        if (arrayXPos + 1 <= 8)
        {
            pieceRight = spawnedRoomsArray[arrayXPos + 1, arrayYPos];


        }


        //if theres a piece above that doesnt have a bottom exit, removes all pieces with top exits from potential spawn
        if (pieceAbove != null && pieceAbove.GetComponent<MapSegmentControl>().bottomExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLRmap);
            potentialRooms.Remove(TLBmap);
            potentialRooms.Remove(TLmap);
        }
        //if piece below doesnt have top exit, removes all bottom pieces
        if (pieceBelow != null && pieceBelow.GetComponent<MapSegmentControl>().topExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(BLmap);
            potentialRooms.Remove(LRBmap);
            potentialRooms.Remove(TLBmap);
        }
        //if pieces right doesnt have a right exit, removes all right exits pieces
        if (pieceRight != null && pieceRight.GetComponent<MapSegmentControl>().leftExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLRmap);
            potentialRooms.Remove(LRmap);
            potentialRooms.Remove(LRBmap);
        }

        var randomRange = Random.Range(0, potentialRooms.Count);
        var randomNum = potentialRooms[randomRange];
        var newRightPiece = Instantiate(randomNum, new Vector3(currPiecePos.x + spawnRoomPosIncrement, currPiecePos.y, -1), potentialRooms[randomRange].transform.rotation);
        //currPieceScript.rightExit = false;
        //currPieceScript.numExitsLeft -= 1;
        spawnedRooms.Add(newRightPiece);
        newRightPiece.GetComponent<MapSegmentControl>().closeExit("Left");
        newRightPiece.GetComponent<MapSegmentControl>().xArrayPos = arrayXPos + 1;
        newRightPiece.GetComponent<MapSegmentControl>().yArrayPos = arrayYPos;
        newRightPiece.name = randomNum.name;
        spawnedRoomsArray[arrayXPos + 1, arrayYPos] = newRightPiece;
        currentRoomAmount += 1;
        noRoomsAdded = false;

    }

    private void spawnRoomAbove()
    {
        var pieceAbove = blankMap;
        var pieceLeft = blankMap;
        var pieceRight = blankMap;
        var potentialRooms = bottomExitPieces;

        if (arrayYPos - 1 >= 0)
        {
            pieceAbove = spawnedRoomsArray[arrayXPos, arrayYPos - 1];


        }

        if (arrayXPos - 1 >= 0)
        {
            pieceLeft = spawnedRoomsArray[arrayXPos -1, arrayYPos];

        }
        if (arrayXPos + 1 <= 8)
        {
            pieceRight = spawnedRoomsArray[arrayXPos + 1, arrayYPos];

        }

        //if theres a piece above that doesnt have a bottom exit, removes all pieces with top exits from potential spawn
        if (pieceAbove != null && pieceAbove.GetComponent<MapSegmentControl>().bottomExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TBmap);
            potentialRooms.Remove(TRBmap);
            potentialRooms.Remove(TLBmap);
        }
        //if piece left doesnt have right exit, removes all left pieces
        if (pieceLeft != null && pieceLeft.GetComponent<MapSegmentControl>().rightExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(BLmap);
            potentialRooms.Remove(TLBmap);
            potentialRooms.Remove(LRBmap);
        }
        //if pieces right doesnt have a left exit, removes all right exits pieces
        if (pieceRight != null && pieceRight.GetComponent<MapSegmentControl>().leftExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(BRmap);
            potentialRooms.Remove(TRBmap);
            potentialRooms.Remove(LRBmap);
        }

        var randomRange = Random.Range(0, potentialRooms.Count);
        var randomNum = potentialRooms[randomRange];
        var newTopPiece = Instantiate(randomNum, new Vector3(currPiecePos.x, currPiecePos.y + spawnRoomPosIncrement, -1), potentialRooms[randomRange].transform.rotation);
        //currPieceScript.topExit = false;
        //currPieceScript.numExitsLeft -= 1;
        spawnedRooms.Add(newTopPiece);
        newTopPiece.GetComponent<MapSegmentControl>().closeExit("Bottom");
        newTopPiece.GetComponent<MapSegmentControl>().xArrayPos = arrayXPos;
        newTopPiece.GetComponent<MapSegmentControl>().yArrayPos = arrayYPos - 1;
        spawnedRoomsArray[arrayXPos, arrayYPos - 1] = newTopPiece;
        newTopPiece.name = randomNum.name;
        currentRoomAmount += 1;
        noRoomsAdded = false;
    }

    private void spawnRoomBelow()
    {
        var pieceBelow = blankMap;
        var pieceLeft = blankMap;
        var pieceRight = blankMap;
        var potentialRooms = topExitPieces;

        if (arrayYPos + 1 <= 4)
        {
            pieceBelow = spawnedRoomsArray[arrayXPos, arrayYPos + 1];


        }

        if (arrayXPos - 1 >= 0)
        {
            pieceLeft = spawnedRoomsArray[arrayXPos - 1, arrayYPos];

        }
        if (arrayXPos + 1 <= 8)
        {
            pieceRight = spawnedRoomsArray[arrayXPos + 1, arrayYPos];

        }

        //if theres a piece below that doesnt have a top exit, removes all pieces with bottom exits from potential spawn
        if (pieceBelow != null && pieceBelow.GetComponent<MapSegmentControl>().topExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TBmap);
            potentialRooms.Remove(TRBmap);
            potentialRooms.Remove(TLBmap);
        }
        //if piece left doesnt have right exit, removes all left pieces
        if (pieceLeft != null && pieceLeft.GetComponent<MapSegmentControl>().rightExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLmap);
            potentialRooms.Remove(TLBmap);
            potentialRooms.Remove(TLRmap);
        }
        //if pieces right doesnt have a left exit, removes all right exits pieces
        if (pieceRight != null && pieceRight.GetComponent<MapSegmentControl>().leftExit == false)
        {
            potentialRooms.Remove(TLBRmap);
            potentialRooms.Remove(TLRmap);
            potentialRooms.Remove(TRBmap);
            potentialRooms.Remove(TRmap);
        }

        var randomRange = Random.Range(0, potentialRooms.Count);
        var randomNum = potentialRooms[randomRange];
        var newBottomPiece = Instantiate(randomNum, new Vector3(currPiecePos.x, currPiecePos.y - spawnRoomPosIncrement, -1), potentialRooms[randomRange].transform.rotation);
        //currPieceScript.bottomExit = false;
        //currPieceScript.numExitsLeft -= 1;
        spawnedRooms.Add(newBottomPiece);
        newBottomPiece.GetComponent<MapSegmentControl>().closeExit("Top");
        newBottomPiece.GetComponent<MapSegmentControl>().xArrayPos = arrayXPos;
        newBottomPiece.GetComponent<MapSegmentControl>().yArrayPos = arrayYPos + 1;
        newBottomPiece.name = randomNum.name;
        spawnedRoomsArray[arrayXPos, arrayYPos + 1] = newBottomPiece;
        currentRoomAmount += 1;
        noRoomsAdded = false;
    }


    public int[] findInRoomArray(GameObject currentRoom)
    {
        arrayXPos = 0;
        arrayYPos = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (spawnedRoomsArray[i,j] == currentRoom)
                {
                    arrayXPos = i;
                    arrayYPos = j;
                    int[] arrayPos = new int[2];
                    arrayPos[0] = arrayXPos;
                    arrayPos[1] = arrayYPos;
                    return arrayPos;
                }


            }
        }
        return null;
    }

    private void checkExitsLeft()
    {
        exitsLeft = 0;
        for (int i = 0; i < spawnedRooms.Count; i++)
        {
            exitsLeft += spawnedRooms[i].GetComponent<MapSegmentControl>().numExitsLeft;

        }
        //Debug.Log(exitsLeft);

    }

    private void closeOpenExits()
    {
        checkExitsLeft();
       if (exitsLeft > 0)
       {
            for (int i = 0; i < spawnedRooms.Count; i++)
            {
                var roomScript = spawnedRooms[i].GetComponent<MapSegmentControl>();

                var keepLeft = false;
                var keepRight = false;
                var keepTop = false;
                var keepBottom = false;

                if (roomScript.numExitsLeft > 0)
                {
                    if (roomScript.leftExit)
                    {
                        keepLeft = true;
                    }
                    if (roomScript.rightExit)
                    {
                        keepRight = true;
                    }
                    if (roomScript.topExit)
                    {
                        keepTop = true;
                    }
                    if (roomScript.bottomExit)
                    {
                        keepBottom = true;
                    }


                    if (roomScript.leftExitOpen)
                    {
                        keepLeft = false;
                    }
                    if (roomScript.rightExitOpen)
                    {
                        keepRight = false;
                    }
                    if (roomScript.topExitOpen)
                    {
                        keepTop = false;
                    }
                    if (roomScript.bottomExitOpen)
                    {
                        keepBottom = false;
                    }

                    for (int j = 0; j < allPieces.Count; j++)
                    {
                        var pieceScript = allPieces[j].GetComponent<MapSegmentControl>();
                        if (keepLeft == pieceScript.leftExit &&
                            keepRight == pieceScript.rightExit &&
                            keepTop == pieceScript.topExit &&
                            keepBottom == pieceScript.bottomExit)
                        {
                            var oldRoom = spawnedRooms[i].transform.name;
                            var mapsegments = GameObject.FindObjectsOfType<MapSegmentControl>();
                            float newRoomXPos = 0;
                            float newRoomYPos = 0;
                            for (int k = 0; k < mapsegments.Length; k++)
                            {
                                findInRoomArray(spawnedRooms[i]);
                                if (mapsegments[k].transform.name == oldRoom && 
                                    mapsegments[k].GetComponent<MapSegmentControl>().xArrayPos == arrayXPos &&
                                    mapsegments[k].GetComponent<MapSegmentControl>().yArrayPos == arrayYPos)
                                {
                                    newRoomXPos = mapsegments[k].transform.position.x;
                                    newRoomYPos = mapsegments[k].transform.position.y;
                                    Destroy(mapsegments[k].gameObject);
                                }
                            }

                            var newRoom = Instantiate(allPieces[j], new Vector3(newRoomXPos, newRoomYPos, -1), allPieces[j].transform.rotation);
                            spawnedRooms[i] = newRoom;
                            spawnedRoomsArray[arrayXPos, arrayYPos] = newRoom;
                            roomScript = spawnedRooms[i].GetComponent<MapSegmentControl>();

                            newRoom.GetComponent<MapSegmentControl>().xArrayPos = arrayXPos;
                            newRoom.GetComponent<MapSegmentControl>().yArrayPos = arrayYPos;
                            spawnedRoomsArray[arrayXPos, arrayYPos] = newRoom;
                            var newRoomArrayScript = spawnedRoomsArray[arrayXPos, arrayYPos].GetComponent<MapSegmentControl>();

                            if (roomScript.leftExit)
                            {
                                newRoomArrayScript.closeExit("Left");
                            }
                            if (roomScript.rightExit)
                            {
                                newRoomArrayScript.closeExit("Right");
                            }
                            if (roomScript.topExit)
                            {
                                newRoomArrayScript.closeExit("Top");
                            }
                            if (roomScript.bottomExit)
                            {
                                newRoomArrayScript.closeExit("Bottom");
                            }
                            break;

                        }
                        
                    }

                }
            }
       }

        generationDone = true;

    }

    private void spawnMapIcons()
    {
        if (!triggerBoss)
        {
            mapControls.spawnPlayer();
            enemyMapBehavior.spawnStairs();

            enemyMapBehavior.spawnEnemies();
            enemyMapBehavior.spawnShop();
            enemyMapBehavior.spawnEvent();
        }
        else if (triggerBoss)
        {
            triggerBoss = false;
            mapControls.spawnPlayer();
            enemyMapBehavior.spawnStairs();
            enemyMapBehavior.spawnBoss();
        }
    }

    public void deleteMap()
    {
        exitsLeft = 0;
        noRoomsAdded = false;
        currentRoomAmount = 0;
        arrayXPos = 0;
        arrayYPos = 0;
        exitsLeft = 0;

        for (var i = 0; i < spawnedRooms.Count; i++)
        {
            if (spawnedRooms[i].GetComponent<MapSegmentControl>().currentInhabitant != null)
            {
                if (spawnedRooms[i].GetComponent<MapSegmentControl>().roomInhabitedBy != "Player")
                {
                    Destroy(spawnedRooms[i].GetComponent<MapSegmentControl>().currentInhabitant.gameObject);
                }
                spawnedRooms[i].GetComponent<MapSegmentControl>().roomInhabitedBy = null;

            }
            Destroy(spawnedRooms[i]);
        }

        GameObject[] icons = GameObject.FindGameObjectsWithTag("ShopIcon");
        foreach (GameObject go in icons)
        {
            Destroy(go);
        }
        icons = GameObject.FindGameObjectsWithTag("EnemyIcon");
        foreach (GameObject go in icons)
        {
            Destroy(go);
        }
        icons = GameObject.FindGameObjectsWithTag("StairsIcon");
        foreach (GameObject go in icons)
        {
            Destroy(go);
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (spawnedRoomsArray[i, j] != null)
                {
                    spawnedRoomsArray[i, j] = null;
                }


            }
        }



        spawnedRooms.Clear();

        randomizeMap();





    }
}
