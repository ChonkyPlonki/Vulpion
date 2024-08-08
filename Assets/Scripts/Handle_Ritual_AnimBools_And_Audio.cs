using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle_Ritual_AnimBools_And_Audio : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioS;

    public void setRitualBodningFalse()
    {
        animator.SetBool("Play_Bonding_Ritual", false);
    }

    public void PlayRitualSound()
    {
        audioS.Play();
    }
}
