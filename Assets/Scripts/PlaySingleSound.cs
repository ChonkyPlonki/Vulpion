using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySingleSound : MonoBehaviour
{
    public AudioSource audioS;

    public void PlaySoundOnce()
    {
        AudioMasterHandler.Instance.playAmbientSound(audioS);
        //audioS.Play();
    }
}
