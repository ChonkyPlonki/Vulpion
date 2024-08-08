using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSoundOnTrigger : MonoBehaviour
{
    public AudioSource audioS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioS.Play();        
    }
}
