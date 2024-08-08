using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAffectionBonding : MonoBehaviour, BondingAction
{

    public int bondingNumber;
    public string bondingName;
    public GameObject affectionFX;
    public float duration;
    public float magnitude;
    public AudioSource showAffectionSound;
    private float animalFXOffset = 1;

    public Transform playerTransform;
    public float timeToJump = 0.4f;
    public static GameObject animalCollWith;
    public GameObject bondingGlowFX;
    public GameObject sparkleFX;
    public void DoAfterAnimation()
    {
        //Turn ON animal movement
        animalCollWith.transform.parent.gameObject.GetComponent<AnimalController>().StartMovement();
        PlayerController.canMove = true;
    }

    public void DoAtStartAnimation()
    {
        BondingHandler.Instance.TurnOnBondingText();
        showAffectionSound.Play();
        BondingHandler.Instance.ShakeCam(duration, magnitude);

        //Turn off animal movement
        BondingHandler.Instance.InstAnimalReaction(affectionFX, animalCollWith.transform, affectionFX.transform.rotation, animalFXOffset, animalCollWith);
        animalCollWith.transform.parent.gameObject.GetComponent<AnimalController>().StopMovement();
    }

    public Vector3 CalcMidPointPlayerAnimal()
    {
        Vector3 midPos = (PlayerRelated.Instance.playerMovingTransform.position + animalCollWith.transform.position) / 2;
        return midPos;
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
        AnimManageBondingJumpMove.distanceToAnimal = 1.5f;
        animalCollWith = PlayerStats.collidedWith.gameObject;
        //Animator playerAnim = PlayerRelated.Instance.playerAnim;
        BondingHandler.Instance.TurnOnPlayerBondingExpression();
        //playerAnim.SetInteger("Emotion", BondingHandler.Instance.GetCrntBondingNumber());
        //playerAnim.SetLayerWeight(3, 0); //turns off regular emotions
        //playerAnim.SetLayerWeight(4, 1); //turns on bonding-regulated emotions
        PlayerController.canMove = false;
    }
}
