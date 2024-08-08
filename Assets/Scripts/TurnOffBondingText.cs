using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBondingText : MonoBehaviour
{
    public GameObject textFXGameObj;
    public Animator textFXAnim;
    public void TurnOffBondingTextEffect()
    {
        //Debug.Log("DOUNGGG!");
        textFXAnim.SetInteger("BondingSkill",0);
        textFXGameObj.SetActive(false);
    }
}
