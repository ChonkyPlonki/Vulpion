using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDivingAnimHandler : MonoBehaviour
{
    private static SplashDivingAnimHandler _instance;

    public static SplashDivingAnimHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    public Animator animatorWithMask;
    public Animator animatorWithSplash;
    public AudioSource divingSoundAS;
    public Transform divingPlace;
    public float divingYOffset = 0;

    public Collider2D playerGroundCollider;
    public Collider2D playerWaterCollider;



    public void DoAtEndOfDivingSplash()
    {
        turnOnInWaterVariables();
        turnOnWaterMaskAttribute();
        MuffleSoundUnderWater.Instance.turnOnUnderWaterAudio();
        TurnOnWaterCollider();
    }

    public void DoAtBeginningOfSurfacing()
    {
        playSurfacingSound();
        MuffleSoundUnderWater.Instance.turnOffUnderWaterAudio();
        TurnOnGroundCollider();
    }

    private void TurnOnWaterCollider()
    {
        playerGroundCollider.enabled = false;
        playerWaterCollider.enabled = true;
    }
    private void TurnOnGroundCollider()
    {
        playerGroundCollider.enabled = true;
        playerWaterCollider.enabled = false;
    }


    public void turnOnWaterMaskAttribute()
    {
        animatorWithMask.SetBool("ActivateMask", true);
    }    
    
    public void turnOffWaterMaskAttribute()
    {
        animatorWithMask.SetBool("ActivateMask", false);
    }

    public void turnOffSplashAnim()
    {
        animatorWithSplash.SetBool("PlaySplash", false);
    }

    public void turnOnSplashAnim()
    {
        setDivingPlace();
        animatorWithSplash.SetBool("PlaySplash", true);
    }

    public void playDivingSound()
    {
        divingSoundAS.pitch = 1f;
        AudioMasterHandler.Instance.playPlayerSound(divingSoundAS);
        //divingSoundAS.Play();
    }

    public void turnOnInWaterVariables()
    {
        WaterHandler.Instance.UpdateInWaterVariables(true);
    }

    public void playSurfacingSound()
    {
        divingSoundAS.pitch = 0.8f;
        AudioMasterHandler.Instance.playPlayerSound(divingSoundAS);
        //divingSoundAS.Play();
    }

    public void setDivingPlace()
    {
        this.transform.position = new Vector3(divingPlace.transform.position.x, divingPlace.transform.position.y + divingYOffset, divingPlace.transform.position.z);
    }

    public void sinkPlayer()
    {
        StartCoroutine(WaterHandler.Instance.SinkPlayerBeforeDiving());
    }

    public void raisePlayer()
    {
        StartCoroutine(WaterHandler.Instance.RaisePlayerBeforeSurfacing());
    }

    public void waterColorPlayer()
    {
        WaterHandler.Instance.setPlayerColorWater();
    }

    public void defaultColorPlayer()
    {
        WaterHandler.Instance.setPlayerColorDefault();
    }

    /*
    public void setCompletelyDivedBool()
    {
        WaterHandler.hasFullyDived = true;
        WaterHandler.hasFullySurfaced = false;
    }

    public void setCompletelySurfacedBool()
    {
        WaterHandler.hasFullyDived = false;
        WaterHandler.hasFullySurfaced = true;
    }
    */


}

