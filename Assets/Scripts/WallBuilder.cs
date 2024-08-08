using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    //Previously Mousehandler vars
    public Material originalMaterial;
    public Material mouseoverMaterial;
    public GameObject mouseHighlight;
    //public static Vector2 buildStartPosition;

    private Vector2 mousePos;
    private Vector2 objPos;
    private Vector2 objCenter;
    private Vector2 temp;
    private Vector2 lbCorner;
    private Vector2 rbCorner;
    private Vector2 rtCorner;
    private Vector2 ltCorner;

    private Vector3 firstStartPos;
    private Vector3 positionOfStart3D;

    //Original Wallbuilder vars 
    //public static LinkedList<GameObject> instanceHistory;
    public static SortedDictionary<int, GameObject> instanceHistoryDic;
    public LineRenderer lineRend;
    private Vector2 endMousePos;
    private Vector2 startMousePos;

    [SerializeField]
    public GameObject buildObjHor;
    public GameObject buildObjVert;

    private GameObject wall;
    private GameObject shadow;
    private GameObject instance;
    private GameObject objMousedOver;

    private Vector2 wallWidth;
    private Vector2 wallHeight;
    private Vector2 shadowSizeHor;
    private Vector2 shadowSizeVert;

    private float mouseDistance;

    private Color red = new Color(1, 0, 0, 0.5f);
    private Color white = new Color(1, 1, 1, 0.5f);
    private Color originalColor;
    bool touching;

    private float shadowVerAdd = 1.1f;
    private float shadowHorAdd = 1.1f;
    private float minimumSize = 1f;
    public static float wallThickness = 0.48f;
    public static float wallMinHeight = 5.6f;
    //float horCollMicroOffset = 0.22f;
    private float horShadowCollThickness = 0.35f;

    private float heightOffset;
    public static String currentDirection;

    public static int deleterYStartPos = 5;
    public static int snapperYStartPos = 20;
    private static float yPosDisplace = -0.3f;

    //Bools to handle if player is "extending" existing wall
    private bool snappedHorLeft;
    private bool snappedHorRight;
    private bool snappedVerTop;
    private bool snappedVerBottom;

    private bool canSnap = true;
    //To handle wall's unique identification-numbers. Will increase with 1 with each made wall.
    private int nbrWalls = 0;

    public GameObject particleToSpawn;
    public AudioClip placeSound;
    public AudioClip deleteSound;
    public AudioSource buildRumbler;

    public GameObject buildingParticleToSpawn;
    private GameObject buildPartSpawn;

    private bool holdingDeleteBtn;
    private bool isBuilding;
    private bool isSnapBuilding;
    private bool isSnapBuildingTop;
    private bool isSnapBuildingBottom;
    private bool isSnapBuildingRight;
    private bool isSnapBuildingLeft;

    private string prevDirection;

    public GameObject buildingsParent;
    private bool previousAngleVert;

    //private GameObject wallToBeExtended;


    //Ray ray;
    //RaycastHit hit;
    void Start()
    {
        lineRend.positionCount = 2;
        heightOffset = buildObjVert.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.y;
        instanceHistoryDic = new SortedDictionary<int,GameObject>();
    }


    void Update()
    {
        if (ActionsAllowedHandler.isOkToBuild)
        {
            mouseHighlight.SetActive(true);
            PosBuildHighlight();

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (!holdingDeleteBtn && !isBuilding)
                    SnapToWalls();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                if (!isSnapBuilding)
                {
                    ResetSnapBools();
                    ResetSnapBuildingBools();
                    isSnapBuilding = false;
                }                    
            }

            if (Input.GetKey(KeyCode.LeftControl) == true)
            {
                if (!isBuilding)
                {
                    holdingDeleteBtn = true;
                    HighlightDelete();
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl) == true)
            {
                //If you haven't deleted, but the highlight has set in, this will reset the obj-color you highlighted to its original
                holdingDeleteBtn = false;
                ResetObjColor();
            }

            if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Z))
            {
                HistoryDeleteWall();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!holdingDeleteBtn)
                {
                    isBuilding = true;
                    SetUpFirstWall();
                }
                    
            }

            if (Input.GetMouseButton(0))
            {
                if (!holdingDeleteBtn)
                {
                    //UpdateEffects();
                    ContinuoslyUpdateWall();
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("!holdingDeleteBtn: " + !holdingDeleteBtn); 
                if (!holdingDeleteBtn)
                {
                    HandleWallPossExtentions();                    
                    ResetLine();
                    FinalizeWall();
                    isBuilding = false;
                }                                         
                isSnapBuilding = false;
                ResetSnapBuildingBools();
                prevDirection = null;
            }
        } else
        {
            mouseHighlight.SetActive(false);
        } 
            
    }

    private void HandleWallPossExtentions()
    {
        //EXPERIMENT TO MAKE WALLS MERGE 
        if (isSnapBuilding)
        {
            if (objMousedOver != null)
            {
                string extWallType = objMousedOver.GetComponent<WallID>().type;
                //Debug.Log(extWallType);
                if ((extWallType == "Side" && (isSnapBuildingLeft || isSnapBuildingRight) && currentDirection == "Side") || (extWallType == "Up" && (isSnapBuildingTop || isSnapBuildingBottom) && currentDirection == "Up"))
                {
                    instanceHistoryDic.Remove(objMousedOver.GetComponent<WallID>().ID);
                    Destroy(objMousedOver);
                    prevDirection = null;


                    Destroy(instance);

                    //originalColor = buildObjHor.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
                    if (isSnapBuildingLeft)
                    {
                        startMousePos = rbCorner;
                    }
                    else if (isSnapBuildingRight)
                    {
                        startMousePos = lbCorner;// - new Vector2(wallHeight.x, 0);
                    }
                    else if (isSnapBuildingTop)
                    {
                        startMousePos = lbCorner;// - new Vector2(wallWidth.y,0);
                    }
                    else if (isSnapBuildingBottom)
                    {
                        //if (endMousePos.x <= startMousePos.x)
                        startMousePos = ltCorner;
                    }
                    positionOfStart3D = new Vector3(startMousePos.x, (startMousePos.y + yPosDisplace), 0);
                    instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);
                    ContinuoslyUpdateWall();
                } else if (extWallType == "Up" && isSnapBuildingBottom && (endMousePos.x < startMousePos.x))
                {
                    Destroy(instance);
                    prevDirection = null;

                    startMousePos = rbCorner;
                    //positionOfStart3D = new Vector3(startMousePos.x, (startMousePos.y + yPosDisplace), 0);
                    positionOfStart3D = new Vector3(startMousePos.x, (startMousePos.y + yPosDisplace), 0);
                    instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);
                    ContinuoslyUpdateWall();
                }
            }
        }
        //Slut på test
    }

    private void ResetSnapBools()
    {
        snappedHorLeft = false;
        snappedHorRight = false;
        snappedVerTop = false;
        snappedVerBottom = false;
    }

    private void ResetSnapBuildingBools()
    {        
        isSnapBuildingLeft = false;
        isSnapBuildingRight = false;
        isSnapBuildingTop = false;
        isSnapBuildingBottom = false;
    }

    private void PosBuildHighlight()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //buildStartPosition = mousePos;
        mouseHighlight.transform.position = mousePos;
    }

    private void ContinuoslyUpdateWall()
    {
        UpdateVars();

        GameObject wallColl = instance.transform.GetChild(4).gameObject;
        if (isBuildingVert())
        {
            //currentDirection = "Up";
            InstatiateWall(wallHeight, shadowSizeVert, "Up");
            
            //Fifflar med particle-puff
            if (buildPartSpawn != null)
            {
                if (endMousePos.y < startMousePos.y)
                    buildPartSpawn.transform.position = wallColl.transform.position + new Vector3(0, -wallColl.GetComponent<BoxCollider2D>().size.y / 2, 0);
                else
                    buildPartSpawn.transform.position = wallColl.transform.position + new Vector3(0, +wallColl.GetComponent<BoxCollider2D>().size.y / 2, 0);

            }
        }
        else
        {
            //currentDirection = "Side";
            InstatiateWall(wallWidth, shadowSizeHor, "Side");

            //Fifflar med particle-puff
            if (buildPartSpawn != null)
            {
                if (endMousePos.x < startMousePos.x)
                    buildPartSpawn.transform.position = wallColl.transform.position + new Vector3(-wallColl.GetComponent<BoxCollider2D>().size.x/2, -wallColl.GetComponent<BoxCollider2D>().size.y / 2, 0);
                else
                    buildPartSpawn.transform.position = wallColl.transform.position + new Vector3(+wallColl.GetComponent<BoxCollider2D>().size.x/2, -wallColl.GetComponent<BoxCollider2D>().size.y, 0);
            }
        }
        //previousAngleVert = isBuildingVert();
        prevDirection = currentDirection;
        //Debug.Log("puff: " + buildPartSpawn.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder);
        //Debug.Log("wall: " + instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        if (buildPartSpawn != null)
            buildPartSpawn.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private bool isBuildingVert()
    {
        return (isAngleVertical(Mathf.Atan2(endMousePos.y - startMousePos.y, endMousePos.x - startMousePos.x) * 180 / Mathf.PI));
    }
    private void HistoryDeleteWall()
    {
        //if (instanceHistory.First != null)
        //Debug.Log(lastInst2);
        if (instanceHistoryDic.Count != 0)
        {
            //GameObject lastInst = instanceHistory.First.Value;
            //GameObject lastInst2 = instanceHistory2[0];
            GameObject lastInst2 = instanceHistoryDic.Values.Last();
            //Debug.Log(lastInst2);
            //if (instance != null)
            if (lastInst2 != null)
            {
                this.transform.GetComponent<AudioSource>().clip = deleteSound;
                this.transform.GetComponent<AudioSource>().Play();

                int wallid = lastInst2.GetComponent<WallID>().ID;
                instanceHistoryDic.Remove(wallid);
                Destroy(lastInst2);
            }
        }
    }

    private void UpdateVars()
    {
        endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDistance = (endMousePos - startMousePos).magnitude;
        ResetBuildLine();

        SpriteRenderer wallHorSR = buildObjHor.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        SpriteRenderer wallVerSR = buildObjVert.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        SpriteRenderer shadowHorSR = buildObjHor.transform.GetChild(1).transform.GetComponent<SpriteRenderer>();
        SpriteRenderer shadowVerSR = buildObjVert.transform.GetChild(1).transform.GetComponent<SpriteRenderer>();
        float xSize = Mathf.Abs(endMousePos.x - startMousePos.x);
        float ySize = Mathf.Abs(endMousePos.y - startMousePos.y);

        wallWidth = new Vector2(xSize, wallHorSR.size.y);
        wallHeight = new Vector2(wallVerSR.size.x, ySize);
        shadowSizeHor = new Vector2(xSize + shadowHorAdd, shadowHorSR.size.y);
        shadowSizeVert = new Vector2(shadowVerSR.size.x, ySize + shadowVerAdd);


    }

    private void ResetBuildLine()
    {
        lineRend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
        lineRend.SetPosition(1, new Vector3(endMousePos.x, endMousePos.y, 0f));
    }

    private void FinalizeWall()
    {
        buildRumbler.Stop();
        Destroy(buildPartSpawn);

        //if (touching || mouseDistance < minimumSize)
        //Debug.Log("Finalize-wallcoll is: " + wallColliderChecker.wallIsColliding);
        if (IsBuildingValid())
        {
            GameObject wallFinal = instance.transform.GetChild(0).gameObject;
            wallFinal.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

            //Fix deleter, its size, its position and its Z-value (so raycast will find it)
            GameObject deleter = instance.transform.GetChild(2).gameObject;
            deleter.GetComponent<BoxCollider2D>().size = wallFinal.gameObject.GetComponent<SpriteRenderer>().size;
            deleter.transform.position = wallFinal.gameObject.transform.position;
            int wallLayerOrder = wallFinal.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

            GameObject snapper = instance.transform.GetChild(3).gameObject;
            GameObject shadow = instance.transform.GetChild(1).gameObject;
            snapper.GetComponent<BoxCollider2D>().size = new Vector2(snapper.GetComponent<BoxCollider2D>().size.x + deleter.GetComponent<BoxCollider2D>().size.x, snapper.GetComponent<BoxCollider2D>().size.y + shadow.GetComponent<BoxCollider2D>().size.y);
            snapper.transform.position = CalcTriggerYPos(wallFinal, wallLayerOrder, snapperYStartPos);//wallFinal.transform.position;

            Vector2 snapXYPos = new Vector2(shadow.transform.position.x + shadow.GetComponent<BoxCollider2D>().offset.x, shadow.transform.position.y + shadow.GetComponent<BoxCollider2D>().offset.y);
            snapper.transform.position = new Vector3(snapXYPos.x, snapXYPos.y, snapper.transform.position.z);
            deleter.transform.position = CalcTriggerYPos(wallFinal, wallLayerOrder, deleterYStartPos);
            //If vertical walls, turn off their sorting-renderer so they dont update every frame :)

            if (currentDirection == "Up")
            {
                instance.transform.GetChild(0).transform.GetComponent<PositionRendererSorter>().runOnlyOnce = true;
            }

            //Update that wallID and add to history
            nbrWalls += 1;
            instance.GetComponent<WallID>().ID = nbrWalls;
            instance.GetComponent<WallID>().type = currentDirection;
            instanceHistoryDic.Add(nbrWalls, instance);

            /*if (startLeftExtention || startRightExtention)
            {
                int tempID = objMousedOver.GetComponent<WallID>().ID;
                instanceHistory2.Remove(tempID);
                Destroy(objMousedOver);                
            }*/

            //Make as little puff when wall is built and play a sound 
            this.transform.GetComponent<AudioSource>().clip = placeSound;
            this.transform.GetComponent<AudioSource>().Play();
            GameObject inst;
            if (currentDirection == "Side")
            {
                Vector3 tempoPos;
                if (endMousePos.x < startMousePos.x)
                    tempoPos = instance.transform.GetChild(0).gameObject.transform.position + new Vector3(-instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.x / 2, -instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.y / 2, 0);
                else
                    tempoPos = instance.transform.GetChild(0).gameObject.transform.position + new Vector3(+instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.x / 2, -instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.y / 2, 0);

                inst = Instantiate(particleToSpawn, tempoPos, particleToSpawn.transform.rotation);
            }
            else
            {
                Vector3 tempoPos;
                if (endMousePos.y < startMousePos.y)
                    tempoPos = instance.transform.GetChild(0).gameObject.transform.position + new Vector3(0, -instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.y / 2, 0);
                else
                    tempoPos = instance.transform.GetChild(0).gameObject.transform.position + new Vector3(0, +instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size.y / 2, 0);

                inst = Instantiate(particleToSpawn, tempoPos, particleToSpawn.transform.rotation);
                // + new Vector3(0, -instance.transform.GetChild(0).gameObject.transform.GetComponent<BoxCollider2D>().size.y / 2
            }

            //Place instance in buildingparent and name it its ID
            instance.transform.parent = buildingsParent.transform;
            instance.name = instance.name + ", " + instance.GetComponent<WallID>().type + instance.GetComponent<WallID>().ID;


            ParticleSystem parts = inst.GetComponent<ParticleSystem>();
            inst.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = instance.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
            Destroy(inst, totalDuration);

            Destroy(instance.transform.GetChild(6).gameObject);
            prevDirection = null;
        }
        else
        {
            this.transform.GetComponent<AudioSource>().clip = deleteSound;
            this.transform.GetComponent<AudioSource>().Play();
            Destroy(instance);

        }

        ResetSnapBools();
        canSnap = true;
    }

    private Vector3 CalcTriggerYPos(GameObject wallFinal, int wallLayerOrder, int yStartPoint)
    {
        //Will put the triggers in layers according to layerposition. 
        //Will start at y-pos -5 (deleterYStartPos) and could go to like ca. -4 I think, 
        //Snapper till start y-pos 20 (snapperYStartPos) and go to like ca. - 18
        //might give errors in future if raycast accidentally picks up one or the other, but may never do so lets hope : )
        return (new Vector3(wallFinal.transform.position.x, wallFinal.transform.position.y, -((yStartPoint * 10000) + (float)wallLayerOrder) / 10000));
    }

    private void ResetLine()
    {
        lineRend.SetPosition(0, new Vector3());
        lineRend.SetPosition(1, new Vector3());
    }

    private void SetUpFirstWall()
    {
        if (HasSnapped())
        {
            canSnap = false;
            isSnapBuilding = true;
        }
        originalColor = buildObjVert.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color;
        startMousePos = mousePos;
        //Handle if special case where you're extending already existing wall.
        if (snappedHorLeft)
        {
            startMousePos = lbCorner;
            isSnapBuildingLeft = true;
        }
        else if (snappedHorRight)
        {
            startMousePos = rbCorner;// - new Vector2(wallHeight.x, 0);
            isSnapBuildingRight = true;
        }
        else if (snappedVerTop)
        {
            startMousePos = ltCorner;// - new Vector2(wallWidth.y,0);
            isSnapBuildingTop = true;
        }
        else if (snappedVerBottom)
        {
            startMousePos = lbCorner;
            isSnapBuildingBottom = true;
        }
        //positionOfStart3D = new Vector3(startMousePos.x, (startMousePos.y + yPosDisplace), 0);

        positionOfStart3D = GetStartPos3D();
        instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);

        //wall = instance.transform.GetChild(0).gameObject;
        //shadow = instance.transform.GetChild(1).gameObject;
    }

    private Vector3 GetStartPos3D()
    {
        return new Vector3(startMousePos.x, (startMousePos.y + yPosDisplace), 0);
    }

    private float CalcPos3DX()
    {
        if (currentDirection == "Up" && snappedHorRight)
            return positionOfStart3D.x - wallHeight.x;
        else
            return positionOfStart3D.x;
    }

    private float CalcPos3DY()
    {
        if (snappedVerTop && currentDirection == "Side")
            return positionOfStart3D.y - wallThickness + 0.18f;
        else
            return positionOfStart3D.y;
    }

    private void InstatiateWall(Vector2 wallSize, Vector2 shadowSize, String direction)
    {
        currentDirection = direction;
        //if (instance.gameObject.GetComponent<WallID>().type == prevDirection)
        //{

        //}
        if (currentDirection != prevDirection)
        {

            Destroy(instance);
            if (snappedHorRight && direction == "Up")
            {
                Debug.Log("Gör flyttinstansen. Flyttar instans till vänster såhär mkt: " + wallHeight.x);
                instance = Instantiate(buildObjVert, new Vector3(CalcPos3DX(), positionOfStart3D.y, positionOfStart3D.z), Quaternion.identity);
            }
            else if (snappedVerTop && direction == "Side")
                instance = Instantiate(buildObjVert, new Vector3(positionOfStart3D.x, CalcPos3DY(), positionOfStart3D.z), Quaternion.identity);
            else
                instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);

            wall = instance.transform.GetChild(0).gameObject;
            shadow = instance.transform.GetChild(1).gameObject;
        }
        //if (startRightExtention && currentDirection == "Up" && endMousePos.y < positionOfStart3D.y)
        //     instance = Instantiate(buildObjVert, new Vector3(positionOfStart3D.x, (positionOfStart3D.y + horShadowCollThickness - 0.05f), positionOfStart3D.z), Quaternion.identity);
        // else

        /*
        //instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);
        //Debug.Log("endMousePos.y is: " + endMousePos + ", positionOfStart3D.y is:" + positionOfStart3D + ", rtCorner.y is: " + rtCorner);
        //Debug.Log(" lbtCorner is: " + lbCorner + ",  ltCorner is:" + ltCorner + ",  rbCorner is: " + rbCorner + " + rtCorner.y is: " + rtCorner);
        if (startRightExtention && currentDirection == "Up" && endMousePos.y < positionOfStart3D.y)
        {
            //Vector3 temp = new Vector3(positionOfStart3D.x, ltCorner.y, positionOfStart3D.z);
            //Vector3 temp = positionOfStart3D + new Vector3(0, horShadowCollThickness,0);
            //Debug.Log("Doing this step! Temp is: " + temp);
            Vector3 temp = new Vector3(rtCorner.x, rtCorner.y, positionOfStart3D.z);
            instance = Instantiate(buildObjVert, temp, Quaternion.identity);
        }
        else 
        {
            instance = Instantiate(buildObjVert, positionOfStart3D, Quaternion.identity);
        }*/

        ResizeWall(wallSize, shadowSize, direction);
        

        if (direction.Equals("Up"))
        {
            PositionWallVer(wallSize);
            PositionCollsVer(wallSize);
            AlterSortingLayer();
        }
        else
        {
            PositionWallHor(wallSize);
        }
        UpdateCollChecker();
        UpdateEffects();  }

    private void UpdateCollChecker()
    {
        GameObject collChecker = instance.transform.GetChild(6).gameObject;
        collChecker.GetComponent<BoxCollider2D>().size = instance.transform.GetChild(4).gameObject.GetComponent<BoxCollider2D>().size;
        collChecker.GetComponent<BoxCollider2D>().offset = instance.transform.GetChild(4).gameObject.GetComponent<BoxCollider2D>().offset;
        collChecker.transform.position = instance.transform.GetChild(1).transform.position;
    }

    private void PositionWallVer(Vector2 wallSize)
    {
        float yCord = (lineRend.GetPosition(0).y + lineRend.GetPosition(1).y) / 2;
        //if (startRightExtention && currentDirection == "Up" && endMousePos.y < positionOfStart3D.y)
        //    yCord += horShadowCollThickness - 0.05f;
        //    EditorApplication.isPaused = true;

        //Trying something new!
        //wall.transform.position = new Vector2(wall.transform.position.x, yCord) + new Vector2(wall.transform.GetComponent<SpriteRenderer>().size.x / 2, (heightOffset / 2));
        //shadow.transform.position = new Vector2(shadow.transform.position.x + (wall.transform.GetComponent<SpriteRenderer>().size.x / 2), yCord);
        //shadow.transform.position = new Vector2(GetStartPos3D().x + (wall.transform.GetComponent<SpriteRenderer>().size.x / 2), yCord);
        //shadow.transform.position = new Vector2(shadow.transform.position.x + (wall.transform.GetComponent<SpriteRenderer>().size.x / 2), yCord);

        //GetStartPos3D
        //wall.transform.position = new Vector2(GetStartPos3D().x, yCord) + new Vector2(wall.transform.GetComponent<SpriteRenderer>().size.x / 2, (heightOffset / 2));
        //buildObjVert.transform.
        float wallXPos = CalcPos3DX() + buildObjVert.transform.GetChild(0).transform.position.x;
        //float wallYPos = positionOfStart3D.y + buildObjVert.transform.GetChild(0).transform.position.y;
        float vertWallThickness = buildObjVert.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.x / 2;
        float shadowXWallPos = CalcPos3DX() + buildObjVert.transform.GetChild(1).transform.position.x;
        Vector2 wallPos = new Vector2(wallXPos, yCord) + new Vector2(vertWallThickness, (heightOffset / 2));

        //wall.transform.position = new Vector2(wallXPos, yCord) + new Vector2(vertWallThickness, (heightOffset / 2));
        wall.transform.position = wallPos;
        shadow.transform.position = new Vector2(shadowXWallPos + vertWallThickness, yCord);

        //Colliders

        //BoxCollider2D fadeColl = instance.transform.GetComponent<BoxCollider2D>();
        BoxCollider2D fadeColl = instance.transform.GetChild(5).transform.GetComponent<BoxCollider2D>();
        BoxCollider2D fadeCollRef = buildObjVert.transform.GetChild(5).transform.GetComponent<BoxCollider2D>();
        //BoxCollider2D fadeColl = instance.transform.GetChild(3).GetComponent<BoxCollider2D>();
        BoxCollider2D wallColl = shadow.transform.GetComponent<BoxCollider2D>();

        float yWallCollCalc = wallPos.y - (GetVerWallSize(wallSize).y /2) + wallColl.size.y / 2;
        instance.transform.GetChild(4).transform.position = new Vector2(wallPos.x, yWallCollCalc);

        fadeColl.size = new Vector2(wallSize.x, fadeCollRef.size.y);
        fadeColl.offset = new Vector2(0,0);
        float fadeXPos = instance.transform.GetChild(0).transform.localPosition.x; //FEL!!... neeeh den är rätt
        //If its negative need to take that into account!
        float fadeCollYPosCalc = wallPos.y + wallSize.y / 2 - 0.22f;
        if (endMousePos.y > startMousePos.y)
        {
            //fadeColl.offset = new Vector2(wall.transform.localPosition.x, fadeColl.offset.y + wallSize.y);
            //fadeColl.offset = new Vector2(fadeXPos, fadeColl.offset.y + wallSize.y);
            //float fadeCollYPosCalc = wallPos.y + wallSize.y / 2; //- fadeColl.size.y/2;
            //fadeColl.offset = new Vector2(fadeXPos, fadeCollYPosCalc);
            instance.transform.GetChild(5).transform.position = new Vector2(wallPos.x, fadeCollYPosCalc);
        }
        else
        {
            //fadeColl.offset = new Vector2(wall.transform.localPosition.x, fadeColl.offset.y + wallSize.y - mouseDistance);
            //fadeColl.offset = new Vector2(fadeXPos, fadeCollRef.offset.y + wallSize.y - mouseDistance);
            //float fadeCollYPosCalc = wallPos.y + wallSize.y / 2; //+ fadeColl.size.y / 2;
            //fadeColl.offset = new Vector2(fadeXPos, fadeCollYPosCalc);
            instance.transform.GetChild(5).transform.position = new Vector2(wallPos.x, fadeCollYPosCalc);
        }
        wallColl.size = new Vector2(wallSize.x, wallSize.y);
        wallColl.offset = new Vector2(wallColl.offset.x, 0);//new Vector2(0, wallColl.offset.y + vertCollMicroOffset);
        instance.transform.GetChild(4).transform.GetComponent<BoxCollider2D>().size = wallColl.size;
        instance.transform.GetChild(4).transform.GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
        //instance.transform.GetChild(5).transform.position = new Vector2(wallXPos, yCord) + new Vector2(vertWallThickness, (heightOffset / 2));
        //float yWallCollCalc = wallPos.y - instance.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.y/2 + wallColl.size.y/2;

        //(wallYPos + wallThickness) - (buildObjHor.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.y / 2) + horShadowCollThickness / 2;

        //float yPosWallCollCalc = 
    }

    private void PositionCollsVer(Vector2 wallSize)
    {

    }

    private void PositionWallHor(Vector2 wallSize)
    {
        //Give wall and shadow right position
        float xCord = (lineRend.GetPosition(0).x + lineRend.GetPosition(1).x) / 2;
        float wallYPos = CalcPos3DY() + buildObjVert.transform.GetChild(0).transform.position.y;
        float shadowYWallPos = CalcPos3DY() + buildObjVert.transform.GetChild(1).transform.position.y;
        float wallCollYPosCalc = (wallYPos + wallThickness) - (buildObjHor.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.y / 2) + horShadowCollThickness / 2;

        //Fixing wallcollider
        instance.transform.GetChild(4).transform.position = new Vector2(xCord, wallCollYPosCalc);//new Vector2(xCord, shadowYWallPos + wallThickness / 2);

        wall.transform.position = new Vector2(xCord, wallYPos + wallThickness);
        shadow.transform.position = new Vector2(xCord, shadowYWallPos + wallThickness / 2);


        //OTHER COLLIDERS

        BoxCollider2D fadeColl = instance.transform.GetChild(5).transform.GetComponent<BoxCollider2D>();
        GameObject fader = instance.transform.GetChild(5).transform.gameObject;
        BoxCollider2D fadeCollRef = buildObjVert.transform.GetChild(5).transform.GetComponent<BoxCollider2D>();
        //BoxCollider2D fadeColl = instance.transform.GetChild(3).GetComponent<BoxCollider2D>();
        BoxCollider2D shadowColl = shadow.transform.GetComponent<BoxCollider2D>();

        BoxCollider2D wallColl = instance.transform.GetChild(4).transform.GetComponent<BoxCollider2D>();

        //float wallXPos = positionOfStart3D.x + buildObjVert.transform.GetChild(0).transform.position.x;
        //float localWallXPos = buildObjVert.transform.GetChild(0).transform.position.x;
        float localWallXPos = wall.transform.localPosition.x;
        float shadowXOffset = buildObjVert.transform.GetChild(1).transform.GetComponent<BoxCollider2D>().offset.x;
        float fadeYSize = buildObjVert.transform.GetComponent<BoxCollider2D>().size.y;
        float fadeYOffset = buildObjVert.transform.GetComponent<BoxCollider2D>().offset.y;


        //fadeColl.size = new Vector2(wallSize.x - (wallThickness/2), fadeYSize);
        Vector3 wallPos = instance.transform.GetChild(0).transform.position;
        float fadeCollYPosCalc = wallPos.y + wallSize.y / 2 - 0.22f;
        //instance.transform.GetChild(5).transform.position = new Vector2(wallPos.x, fadeCollYPosCalc);
        instance.transform.GetChild(5).transform.position = wallPos + new Vector3(0,-0.3f,0);
        fadeColl.size = new Vector2(wallSize.x - (wallThickness / 2), fadeYSize);
        //fadeColl.offset = new Vector2(localWallXPos, fadeYOffset + wallThickness);
        fadeColl.offset = new Vector2(0,0);
        shadowColl.size = new Vector2(wallSize.x, horShadowCollThickness);
        shadowColl.offset = new Vector2(0, 0);// + horCollMicroOffset); //new Vector2(0, wallColl.offset.y + horCollMicroOffset);
        wallColl.size = new Vector2(wallSize.x, horShadowCollThickness);

        //float wallCollYPosCalc = wall.transform.position.y - (wall.transform.GetComponent<SpriteRenderer>().size.y / 2) + wallColl.size.y/2;

        //float wallCollYPosCalc = wallYPos - (buildObjHor.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().size.y / 2) + horShadowCollThickness / 2;

    }

    private void AlterSortingLayer()
    {
        GameObject wallTemp = instance.transform.GetChild(0).transform.gameObject;
        wallTemp.GetComponent<PositionRendererSorter>().offset = (int)((-19.921f) * (wallTemp.GetComponent<SpriteRenderer>().size.y) + 55.327f);
    }

    private void ResizeWall(Vector2 wallSize, Vector2 shadowSize, string direction)
    {
        if (direction.Equals("Up"))
        {
            //wall.transform.GetComponent<SpriteRenderer>().size = wallSize + new Vector2(0, heightOffset);
            wall.transform.GetComponent<SpriteRenderer>().size = GetVerWallSize(wallSize);
            shadow.transform.GetComponent<SpriteRenderer>().size = shadowSize;
        }
        else
        {
            wall.transform.GetComponent<SpriteRenderer>().size = wallSize;
            shadow.transform.GetComponent<SpriteRenderer>().size = shadowSize;
        }
    }

    private Vector2 GetVerWallSize(Vector2 wallSize)
    {
        return wallSize + new Vector2(0, heightOffset);
    }

    private bool isAngleVertical(float angle)
    {
        return (angle <= -45 && angle >= -135) || (angle >= 45 && angle <= 135);
    }


    private bool IsBuildingValid()
    {
       //return !((mouseDistance < minimumSize && !HasSnapped()) || wallColliderChecker.wallIsColliding);
       return ((mouseDistance > minimumSize)  || HasSnapped()) && !wallColliderChecker.wallIsColliding;
    }


    private void UpdateEffects()
    {
        //Debug.Log("ÚpdateEff-wallcoll is: " + wallColliderChecker.wallIsColliding);
        //if (instance.transform.GetChild(4).GetComponent<BoxCollider2D>().size.x > 2)
        //      EditorApplication.isPaused = true;
        //touching = shadow.GetComponent<Collider2D>().IsTouchingLayers(Physics2D.AllLayers);
        //if (touching || mouseDistance < minimumSize)
        if (IsBuildingValid())
        {
            if (!buildRumbler.isPlaying)
                buildRumbler.Play();

            if (buildPartSpawn == null)
                buildPartSpawn = Instantiate(buildingParticleToSpawn, mousePos, buildingParticleToSpawn.transform.rotation);

            wall.GetComponent<SpriteRenderer>().color = white;
        }
        else //IF NOT VALID //((mouseDistance < minimumSize && !HasSnapped()) || wallColliderChecker.wallIsColliding)
        {
            //Debug.Log("doing this not valid-effects: D!");
            if (buildRumbler.isPlaying)
                buildRumbler.Stop();

            if (buildPartSpawn != null)
                Destroy(buildPartSpawn);

            wall.GetComponent<SpriteRenderer>().color = red;
        }
    }

    private bool HasSnapped()
    {
        return (snappedHorLeft || snappedHorRight || snappedVerTop || snappedVerBottom);
    }


    private void ResetObjColor()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);

        if (hitInfo.collider != null)
        {

            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Deletable"))
            {
                hitInfo.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().material = originalMaterial;
            }
        }
    }

    private void SnapToWalls()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = -(WallBuilder.snapperYStartPos + 2);
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        Debug.DrawRay(worldPoint, new Vector3(0, 0, 1), Color.white);
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Snappable"))
            {
                objMousedOver = hitInfo.collider.gameObject.transform.parent.gameObject;
                objPos = hitInfo.point;
                GameObject shadColl = hitInfo.collider.transform.parent.GetChild(4).gameObject;
                objCenter = new Vector2(shadColl.transform.position.x + shadColl.GetComponent<BoxCollider2D>().offset.x, shadColl.transform.position.y + shadColl.GetComponent<BoxCollider2D>().offset.y); //hitInfo.collider.transform.position;
                float wallWidth = hitInfo.collider.transform.parent.GetChild(4).gameObject.GetComponent<BoxCollider2D>().size.x;
                float wallHeight = hitInfo.collider.transform.parent.GetChild(4).gameObject.GetComponent<BoxCollider2D>().size.y;
                Vector2 wallPos = objCenter;

                temp = objCenter - objPos;
                float angle = (Mathf.Atan2(temp.y, temp.x) / Mathf.PI) * 180;

                lbCorner = new Vector2(wallPos.x - (wallWidth / 2), wallPos.y - (wallHeight / 2));
                rbCorner = new Vector2(wallPos.x + (wallWidth / 2), wallPos.y - (wallHeight / 2));
                ltCorner = new Vector2(wallPos.x - (wallWidth / 2), wallPos.y + (wallHeight / 2));
                rtCorner = new Vector2(wallPos.x + (wallWidth / 2), wallPos.y + (wallHeight / 2));

                //Debug.Log(objMousedOver.GetComponent<WallID>().type);
                if (objMousedOver.GetComponent<WallID>().type == ("Side"))
                {
                    SnapToSideWall(wallWidth);
                } else if (objMousedOver.GetComponent<WallID>().type == ("Up"))
                {
                    SnapToUpWall(wallHeight);
                }

            }
        } else
        {
            ResetSnapBools();
        }

    }

    private void SnapToUpWall(float wallHeight)
    {
        if (canSnap)
        {
            Vector2 newPos;
            //If TOP walledge
            if (objPos.y > ltCorner.y)
            {
                ResetSnapBools();
                snappedVerTop = true;
                newPos = objCenter + new Vector2(0, (wallHeight / 2));
            } // If BOTTOM walledge
            else if (objPos.y < lbCorner.y)
            {
                ResetSnapBools();
                snappedVerBottom = true;
                newPos = objCenter + new Vector2(0, -(wallHeight / 2));
            } //If LEFT walledge 
            else if (objPos.x <= objCenter.x)
            {
                newPos = GetIntersectionPointCoordinates(lbCorner, ltCorner, objPos, new Vector2(objPos.x + 10, objPos.y));
            } //If RIGHT walledge
            else
            {
                ResetSnapBools();
                newPos = GetIntersectionPointCoordinates(rbCorner, rtCorner, objPos, new Vector2(objPos.x - 10, objPos.y));
            } //If RIGHT walledge
            mousePos = newPos;
            mouseHighlight.transform.position = new Vector2(newPos.x, newPos.y);
        }

    }

    private void SnapToSideWall(float wallWidth)
    {
        if (canSnap)
        {
            Vector2 newPos;
            //If LEFT walledge
            if (objPos.x < lbCorner.x)
            {
                ResetSnapBools();
                snappedHorLeft = true;
                newPos = objCenter + new Vector2(-(wallWidth / 2), 0);
            } //If RIGHT walledge
            else if (objPos.x > rbCorner.x)
            {
                ResetSnapBools();
                snappedHorRight = true;
                newPos = objCenter + new Vector2(wallWidth / 2, 0);
            } //If TOP walledge
            /*else if (objPos.y >= objCenter.y)
            {
                //newPos = GetIntersectionPointCoordinates(ltCorner, rtCorner, objPos, objCenter, out u) - new Vector2(0, WallBuilder.wallMinHeight);
                newPos = GetIntersectionPointCoordinates(ltCorner, rtCorner, objPos, new Vector2(objPos.x, objPos.y - 10), out u);

            } *///if BOTTOM edge wall
            else
            {
                ResetSnapBools();
                newPos = GetIntersectionPointCoordinates(lbCorner, rbCorner, objPos, new Vector2(objPos.x, objPos.y + 10));
            }
            mousePos = newPos;
            mouseHighlight.transform.position = new Vector2(newPos.x, newPos.y);
        }

    }

    private void HighlightDelete()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);

        if (hitInfo.collider != null)
        {

            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Deletable"))
            {
                hitInfo.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().material = mouseoverMaterial;
                if (Input.GetMouseButtonUp(0))
                {
                    //WallBuilder.instanceHistory.RemoveFirst();
                    this.transform.GetComponent<AudioSource>().clip = deleteSound;
                    this.transform.GetComponent<AudioSource>().Play();

                    instanceHistoryDic.Remove(hitInfo.transform.parent.gameObject.GetComponent<WallID>().ID);
                    Destroy(hitInfo.transform.parent.gameObject);
                }
            }
        }
    }

    public Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

        if (tmp == 0)
        {
            // No solution!
            return Vector2.zero;
        }

        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

        return new Vector2(
            B1.x + (B2.x - B1.x) * mu,
            B1.y + (B2.y - B1.y) * mu
        );
    }
}






/*
         //If extending vertical wall, and going to the LEFT, fix "empty part" of top wall
        //if (startUpExtention && currentDirection == "Side" && endMousePos.x < positionOfStart3D.x)
        //    xCord += horShadowCollThickness - 0.05f;

        //Testing new
        //wall.transform.position = new Vector2(xCord, wall.transform.position.y + wallThickness);
        //shadow.transform.position = new Vector2(xCord, shadow.transform.position.y + wallThickness / 2);
        //Fixes the x-axis postion so its always in the middle, line below:
        //wall.transform.position = new Vector2(xCord, GetStartPos3D().y + wallThickness + buildObjVert.transform.GetChild(0).GetComponent<SpriteRenderer>().size.y/2);
        //shadow.transform.position = new Vector2(xCord, GetStartPos3D().y + wallThickness / 2);

        //wall.transform.position = new Vector2(xCord, wall.transform.position.y + wallThickness);
        //shadow.transform.position = new Vector2(xCord, shadow.transform.position.y + wallThickness / 2);
     */

/*
     private void PositionWallHor()
    {
        //Give wall and shadow right position
        float xCord = (lineRend.GetPosition(0).x + lineRend.GetPosition(1).x) / 2;

        //If extending vertical wall, and going to the LEFT, fix "empty part" of top wall
        //if (startUpExtention && currentDirection == "Side" && endMousePos.x < positionOfStart3D.x)
        //    xCord += horShadowCollThickness - 0.05f;

        //Testing new
        //wall.transform.position = new Vector2(xCord, wall.transform.position.y + wallThickness);
        //shadow.transform.position = new Vector2(xCord, shadow.transform.position.y + wallThickness / 2);
        //Fixes the x-axis postion so its always in the middle, line below:
        //wall.transform.position = new Vector2(xCord, GetStartPos3D().y + wallThickness + buildObjVert.transform.GetChild(0).GetComponent<SpriteRenderer>().size.y/2);
        //shadow.transform.position = new Vector2(xCord, GetStartPos3D().y + wallThickness / 2);

        //wall.transform.position = new Vector2(xCord, wall.transform.position.y + wallThickness);
        //shadow.transform.position = new Vector2(xCord, shadow.transform.position.y + wallThickness / 2);
        float wallYPos = positionOfStart3D.y + buildObjVert.transform.GetChild(0).transform.position.y;
        float shadowYWallPos = positionOfStart3D.y + buildObjVert.transform.GetChild(1).transform.position.y;


        wall.transform.position = new Vector2(xCord, wallYPos + wallThickness);
        shadow.transform.position = new Vector2(xCord, shadowYWallPos + wallThickness / 2);
        instance.transform.GetChild(5).transform.position = new Vector2(xCord, shadowYWallPos + wallThickness / 2);
    }

    private void PositionCollsHor(Vector2 wallSize)
    {
        BoxCollider2D fadeColl = instance.transform.GetComponent<BoxCollider2D>();
        //BoxCollider2D fadeColl = instance.transform.GetChild(3).GetComponent<BoxCollider2D>();
        BoxCollider2D shadowColl = shadow.transform.GetComponent<BoxCollider2D>();
        
        BoxCollider2D wallColl = instance.transform.GetChild(5).transform.GetComponent<BoxCollider2D>();

        //float wallXPos = positionOfStart3D.x + buildObjVert.transform.GetChild(0).transform.position.x;
        float localWallXPos = buildObjVert.transform.GetChild(0).transform.position.x;
        float shadowXOffset = buildObjVert.transform.GetChild(1).transform.GetComponent<BoxCollider2D>().offset.x;
        float fadeYSize = buildObjVert.transform.GetComponent<BoxCollider2D>().size.y;
        float fadeYOffset = buildObjVert.transform.GetComponent<BoxCollider2D>().offset.y;
        

        //fadeColl.size = new Vector2(wallSize.x - (wallThickness/2), fadeYSize);
        fadeColl.size = new Vector2(wallSize.x - (wallThickness/2), fadeYSize);
        fadeColl.offset = new Vector2(localWallXPos, fadeYOffset + wallThickness);
        shadowColl.size = new Vector2(wallSize.x, horShadowCollThickness);
        shadowColl.offset = new Vector2(0, 0);// + horCollMicroOffset); //new Vector2(0, wallColl.offset.y + horCollMicroOffset);
        wallColl.size = new Vector2(wallSize.x, horShadowCollThickness);
    }
     */









//CalcTriggerYPos(wallFinal, wallLayerOrder, deleterYStartPos);

//wallFinal.gameObject.GetComponent<BoxCollider2D>().size = wallFinal.GetComponent<SpriteRenderer>().size;

//snapper.transform.position = CalcSnapperPos(wallFinal, wallLayerOrder);//wallFinal.transform.position;
//float deleterZ = ((50000) + wallLayerOrder)/ 10000;
//Debug.Log(wallLayerOrder);
//Debug.Log((50000 + (float) wallLayerOrder) / 10000);
//deleter.transform.position = new Vector3(wallFinal.transform.position.x, wallFinal.transform.position.y, -((50000) + (float) wallLayerOrder) / 10000);
//deleter.transform.position = CalculateDeleterPos(wallFinal, wallLayerOrder);

//bool OkToBuild()
//{
//    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

//    // If it hits something...
//    if (hit.collider != null)
//    {
//        Collider2D maybeColl = hit.collider.gameObject.GetComponent<Collider2D>();
//        if (maybeColl != null)
//        {
//            if (maybeColl.isTrigger == false)
//            {
//                //Debug.Log("You hit: " + hit.collider.gameObject);
//                return false;
//            }
//        }

//    }

//    return true;
//}

