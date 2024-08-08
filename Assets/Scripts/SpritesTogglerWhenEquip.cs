using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesTogglerWhenEquip : MonoBehaviour
{
    //States equipment-changes if neccessary
    /* public bool isEquippable = false;
     public string nameOfEquippableSprites;

     [SerializeField]
     public string equipsToAffect;
     [SerializeField]
     public string equipsToTurnOn;
     [SerializeField]
     public string equipsToTurnOff;

     */
    

    [System.Serializable]
    public enum ItemTypeToToggle
    {
        None,
        Equippable,
        Hat,
        Hair,
        Scarf,
        Necklace,
        Shirt,
        Bracelet,
        Mittens,
        Bag,
        InsideBag,
        Skirt,
        Pants,
        Shoes,
        Cape,
    }

    public string nameOfMainSpritesToToggleOn = "";
    public ItemTypeToToggle mainItemTypeToToggleOn;


    [System.Serializable]
    public enum patternTypeToToggle
    {
        None,
        EquippablePattern,
        ShirtPattern,
        SkirtPattern,
        PantsPattern,
    }

    public string nameOfPatternSpritesToToggleOn = "";
    public patternTypeToToggle patternItemTypeToToggleOn;

    public ItemTypeToToggle mainItemTypeToToggleOff;
    public patternTypeToToggle patternItemTypeToToggleOff;
    public ItemTypeToToggle otherItemTypeToToggleOff;
}
