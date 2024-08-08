using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBondingSoundOnce : MonoBehaviour
{
    public AudioSource jumpAS;
    public AudioSource estBoundariesGrunt;
    public AudioSource showOffChimesAS;
    public AudioSource showOffGruntAS;

    public void playBondingJumpSound()
    {
        jumpAS.Play();
    }

    public void playBondingEstBndriesGruntSound()
    {
        estBoundariesGrunt.Play();
    }

    public void PlayBondingShowOff()
    {
        showOffChimesAS.Play();
        showOffGruntAS.Play();
    }
}
