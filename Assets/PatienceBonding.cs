using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceBonding : MonoBehaviour, BondingAction
{
    public int bondingNumber;
    public string bondingName;
    public AudioSource patienceAS;
    //public GameObject showOffFX;
    //public GameObject animalReactFX;
    //public GameObject mainCam;
    //private float animalFXOffset = 1;

    public void DoAfterAnimation()
    {
        BondingHandler.Instance.StartAnimalCollMovement();
    }

    public void DoAtStartAnimation()
    {
        patienceAS.Play();
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
        BondingHandler.Instance.TurnOnBondingText();
        BondingHandler.Instance.TurnOnPlayerBondingExpression();
        BondingHandler.Instance.StopAnimalCollMovement();
    }
}
