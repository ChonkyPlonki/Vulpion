using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandler : MonoBehaviour
{
    //Temporarily removed this as the shader currently doesnt work when flipped
    public GameObject objectWithSharedShader;

    public GameObject waterMask;
    public GameObject playerToMoveWhenDiving;
    public ColliderOffWhenInWater collOffWhenInWater;
    public GameObject ThingToMove;
    public SpriteRenderer playerWave;
    public float depthMultiplier = 10.3f;
    //public Animator playerAnimator;
    public Transform moveForSinking;

    public static bool isInWater;
    public static float currentDepth;
    public float sinkOffset = 0;
    public float sinkAmount = -3;

    private Vector3 directionOfRaycast;
    private Vector3 rayCastOriginPos;
    private Texture2D depthTexture;
    private Transform textureLeftEdge;
    private Vector3 hitInWorldSpc;
    private Color hitColor;

    public float sinkTime = 1;
    public bool hasSinkingStarted = false;

    private bool playerWaveVisible = true;
    public Color underwaterColor = new Color(0, 77, 50, 0);
    private Color defaultColor = new Color(0, 0, 0, 0);

    public static bool hasFullyDived;
    public static bool hasFullySurfaced;

    public Collider2D underwaterEdge;

    //public GameObject playerCollider;
    //public float feetHeight = -0.1f;
    //public Transform playerHipForColliderToFollowUnderWater;
    //private float playerColliderDefaultYPos;

    private static WaterHandler _instance;

    public static WaterHandler Instance { get { return _instance; } }


    private void Awake()
    {
        directionOfRaycast = new Vector3(0, 0, 100);
        OnGameStartSetCorrectShader();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ToggleDiving()
    {
        if (!isInWater && hasFullySurfaced)
        {
            if (currentDepth > 0.28)
            {
                Dive();
                ActionsAllowedHandler.isOkToBuild = false;
            }
               
        }
        else if (isInWater & hasFullyDived)
        {
            Surface();
            ActionsAllowedHandler.isOkToBuild = true;
        }
    }

    public void Dive()
    {
        StartCoroutine(WaterHandler.Instance.SinkPlayerBeforeDiving());
        collOffWhenInWater.turnOffColl();
        TurningOnUnderWaterColliders();
    }

    public void Surface()
    {
        if (isInWater)
        {
            SplashDivingAnimHandler.Instance.turnOffSplashAnim();        
            waterMask.GetComponent<Animator>().SetBool("ActivateMask", false);
            collOffWhenInWater.turnOnColl();
            TurningOffUnderWaterColliders();
        }
    }

    public void setPlayerColorWater()
    {
        objectWithSharedShader.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", underwaterColor);
    }
    public void setPlayerColorDefault()
    {
        objectWithSharedShader.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", defaultColor);
    }

    public void UpdateInWaterVariables(bool isActive)
    {
        isInWater = isActive;
        PlayerRelated.Instance.playerAnim.SetBool("IsUnderWater", isActive);
    }

    private void OnGameStartSetCorrectShader()
    {
        if (isInWater)
        {
            setPlayerColorWater();            
            playerToMoveWhenDiving.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingLayerName = "WaterStuff";
            TurningOnUnderWaterColliders();
        }
        else
        {
            setPlayerColorDefault();
            playerToMoveWhenDiving.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingLayerName = "Default";
            TurningOffUnderWaterColliders();
        }
    }

    public void RayHitAndMovePlayerAccordingly(Vector3 originPosition)
    {
        {
            RaycastHit hit;
            rayCastOriginPos = originPosition;
            Ray theRay = new Ray(rayCastOriginPos, directionOfRaycast);

            if (Physics.Raycast(theRay, out hit, 100))
            {
                MovePlayerAccordingToDepth(hit);
            }
            else
            {
                MovePlaterToDefaultPos();
            }
        }
    }

    void MovePlaterToDefaultPos()
    {
        ThingToMove.transform.localPosition = new Vector3(0, 0, 0);
        playerWave.color = new Color(playerWave.color.r, playerWave.color.g, playerWave.color.b, 0);
        playerWave.gameObject.SetActive(false);
    }

    void MovePlayerAccordingToDepth(RaycastHit hit)
    {
        AdjustPlayerDepthPos(hit);
        AdjustPlayerWave();
    }

    void AdjustPlayerDepthPos(RaycastHit hit)
    {

        UpdateTextureVariables(hit);

        float texLocalX = Mathf.Abs(textureLeftEdge.position.x - hitInWorldSpc.x);
        float texLocalY = Mathf.Abs(textureLeftEdge.position.y - hitInWorldSpc.y);

        hitColor = depthTexture.GetPixelBilinear((texLocalX / hit.collider.gameObject.GetComponent<BoxCollider>().bounds.size.x), (texLocalY / hit.collider.gameObject.GetComponent<BoxCollider>().bounds.size.y));
        hitColor = depthTexture.GetPixelBilinear((texLocalX / hit.collider.gameObject.GetComponent<BoxCollider>().bounds.size.x), (texLocalY / hit.collider.gameObject.GetComponent<BoxCollider>().bounds.size.y));
        ThingToMove.transform.localPosition = new Vector3(0, calcPlayerDepth()/*(-hitColor.r * depthMultiplier)+sinkOffset*/, 0);

        currentDepth = hitColor.r;
    }

    public float calcPlayerDepth()
    {
        return (-hitColor.r * depthMultiplier) + sinkOffset;
    }

    void UpdateTextureVariables(RaycastHit hit)
    {
        depthTexture = (Texture2D)hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        textureLeftEdge = hit.collider.gameObject.transform.GetChild(0).GetComponent<Transform>();
        hitInWorldSpc = hit.point;
    }

    void AdjustPlayerWave()
    {
        if (playerWaveVisible)
        {
            playerWave.gameObject.SetActive(true);
            if (hitColor.r > 0.09)
            {
                if (isInWater)
                {
                    playerWave.color = new Color(playerWave.color.r, playerWave.color.g, playerWave.color.b, 0);
                }
                else
                {
                    playerWave.color = new Color(playerWave.color.r, playerWave.color.g, playerWave.color.b, hitColor.r * 2.3f);
                }
            }
            else
            {
                playerWave.color = new Color(playerWave.color.r, playerWave.color.g, playerWave.color.b, 0);
            }
        }
        else
        {
            playerWave.color = new Color(playerWave.color.r, playerWave.color.g, playerWave.color.b, 0);
        }

    }

    public IEnumerator SinkPlayerBeforeDiving()
    {
        hasSinkingStarted = true;
         for (float t = 0.01f; t < sinkTime; t += 0.2f)
         {
            if(t >= 0.8)
            {
                playerWaveVisible = false;
                SplashDivingAnimHandler.Instance.turnOnSplashAnim();                
            }
            sinkOffset = Mathf.Lerp(0,sinkAmount,t / sinkTime);
             yield return null;
         }
    }

    public IEnumerator RaisePlayerBeforeSurfacing()
    {
        playerWaveVisible = true;
        for (float t = 0.01f; t < sinkTime; t += 0.18f)
        {
            sinkOffset = Mathf.Lerp(sinkAmount,0, t / sinkTime);
            if (t / sinkTime >= 0.90)
            {
                sinkOffset = 0;
            }
            yield return null;
        }
        yield return null;
    }

    public bool IsStandingOnDryLand()
    {
        return currentDepth <= 0.005;
    }
    public bool IsStandingInDeeperWater()
    {
        return currentDepth >= 0.1;
    }

    public void TurningOnUnderWaterColliders()
    {
        //underwaterEdge.enabled = true;
    }

    public void TurningOffUnderWaterColliders()
    {
        //underwaterEdge.enabled = false;
    }

    /*
    public void movePlayerCollider()
    {
        playerCollider.transform.localPosition = new Vector3(0, calcPlayerDepth() + feetHeight, 0);
    }

    public void defaultPlayerCollider()
    {
        print("Defaulting!");
        playerCollider.transform.localPosition = new Vector3(0, 0, 0);
    }
    */
}