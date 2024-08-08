using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenBondingSkill : MonoBehaviour
{
    private static ChosenBondingSkill _instance;

    public static ChosenBondingSkill Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public BondingSkill chosenSkill;

    public enum BondingSkill
    {
        Apologize,
        Dazzle,
        ShowAffection,
        EstablishBoundaries,
        ShowOff,
        Patience
    }

    public string ChosenSkill()
    {
        return chosenSkill.ToString();
    }
}
