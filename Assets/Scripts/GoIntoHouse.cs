using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoIntoHouse : MonoBehaviour {

    public GameObject toActivate;
    public GameObject toInactivate;
    public AudioSource soundEffectAudioSource;
    public AudioClip soundEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Triggeren är igång!");
        //if (Input.GetKeyDown(KeyCode.E))
        {
            toActivate.SetActive(!toActivate.activeSelf);
            toInactivate.SetActive(!toInactivate.activeSelf);
            soundEffectAudioSource.clip = soundEffect;
            soundEffectAudioSource.Play();
        }

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (Input.GetKeyDown("E"))
    //    {
    //        toActivate.SetActive(!toActivate.activeSelf);
    //        toInactivate.SetActive(!toInactivate.activeSelf);
    //    }
    //}

}
