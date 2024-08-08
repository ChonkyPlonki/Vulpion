using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablishBoundaries : MonoBehaviour, BondingAction
{
    public int bondingNumber;
    public string bondingName;
    //public GameObject establishFX;
    public GameObject animalReactFX;
    public float duration;
    public float magnitude;
    public AudioSource currentActionSound;

    private float animalFXOffset = 1.5f;
    private GameObject animalCollWith;

    public void DoAfterAnimation()
    {
        // throw new System.NotImplementedException();
        animalCollWith.transform.parent.gameObject.GetComponent<AnimalController>().StartMovement();

    }

    public void DoAtStartAnimation()
    {
        animalCollWith = PlayerStats.collidedWith.gameObject;
        animalCollWith.transform.parent.gameObject.GetComponent<AnimalController>().StopMovement();
        BondingHandler.Instance.InstAnimalReaction(animalReactFX, animalCollWith.transform, animalReactFX.transform.rotation, animalFXOffset, animalCollWith);
        //throw new System.NotImplementedException();
    }

    public string GetBondingActionName()
    {
        return bondingName;
    }

    public int GetBondingAnimatorNumber()
    {
        return bondingNumber;
    }

    public void PerformAction()
    {
        Animator playerAnim = PlayerRelated.Instance.playerAnim;
        playerAnim.SetInteger("Emotion", BondingHandler.Instance.GetCrntBondingNumber());
        playerAnim.SetLayerWeight(3, 0); //turns off regular emotions
        playerAnim.SetLayerWeight(4, 1); //turns on bonding-regulated emotions
        BondingHandler.Instance.TurnOnBondingText();
        AnimManageBondingJumpMove.distanceToAnimal = 3f;
        // throw new System.NotImplementedException();
    }
}
