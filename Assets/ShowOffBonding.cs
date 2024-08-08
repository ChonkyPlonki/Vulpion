using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOffBonding : MonoBehaviour, BondingAction
{
    public int bondingNumber;
    public string bondingName;
    public GameObject showOffFX;
    public GameObject animalReactFX;
    //public GameObject mainCam;
    private float animalFXOffset = 1; 

    public void DoAfterAnimation()
    {
    }

    public void DoAtStartAnimation()
    {
        BondingHandler.Instance.InstAnimalReaction(animalReactFX, animalReactFX.transform.rotation, animalFXOffset);
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
    }
}
