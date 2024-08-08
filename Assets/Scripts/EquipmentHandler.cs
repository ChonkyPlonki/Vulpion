using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class EquipmentHandler : MonoBehaviour
{

    private static EquipmentHandler _instance;

    public static EquipmentHandler Instance { get { return _instance; } }


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

    public bool ChangeEquipment;

    public static bool HatEquipped;
    public static bool HairEquipped;
    public static bool HairUnderHatEquipped;
    public static bool ScarfEquipped;
    public static bool NecklaceEquipped;
    public static bool ShirtEquipped;
    public static bool ShirtPatternEquipped;
    public static bool SleevesEquipped;
    public static bool SleevesPatternEquipped;
    public static bool BraceletsEquipped;
    public static bool MittensEquipped;
    public static bool EquipRightHandEquipped;
    public static bool EquipRightHandPatternEquipped;
    public static bool EquipLeftHandEquipped;
    public static bool EquipLeftHandPatternEquipped;
    public static bool BagEquipped;
    public static bool InsideBagEquipped;
    public static bool SkirtEquipped;
    public static bool SkirtPatternEquipped;
    public static bool PantsEquipped;
    public static bool PantsPatternEquipped;
    public static bool ShoesEquipped;
    public static bool CapeEquipped;
    public static bool HideUnderCapeEquipped;
    public static bool EquippableEquipped;

    [SerializeField]
    public SpriteRenderer[] Hat;

    [SerializeField]
    public SpriteRenderer[] Hair;

    [SerializeField]
    public SpriteRenderer[] HairUnderHat;

    [SerializeField]
    public SpriteRenderer[] Scarf;

    [SerializeField]
    public SpriteRenderer[] Necklace;

    [SerializeField]
    public SpriteRenderer[] Shirt;

    [SerializeField]
    public SpriteRenderer[] ShirtPattern;

    [SerializeField]
    public SpriteRenderer[] Sleeves;

    [SerializeField]
    public SpriteRenderer[] SleevesPattern;

    [SerializeField]
    public SpriteRenderer[] Bracelets;

    [SerializeField]
    public SpriteRenderer[] Mittens;

    [SerializeField]
    public SpriteRenderer[] EquipmentRightHand;

    [SerializeField]
    public SpriteRenderer[] EquipmentRightHandPattern;

    [SerializeField]
    public SpriteRenderer[] EquipmentLeftHand;    
    
    [SerializeField]
    public SpriteRenderer[] EquipmentLeftHandPattern;

    [SerializeField]
    public SpriteRenderer[] Bag;

    [SerializeField]
    public SpriteRenderer[] InsideBag;

    [SerializeField]
    public SpriteRenderer[] Skirt;

    [SerializeField]
    public SpriteRenderer[] SkirtPattern;

    [SerializeField]
    public SpriteRenderer[] Pants;

    [SerializeField]
    public SpriteRenderer[] PantsPattern;

    [SerializeField]
    public SpriteRenderer[] Shoes;

    [SerializeField]
    public SpriteRenderer[] Cape;

    [SerializeField]
    public SpriteRenderer[] HideUnderCape;

    [SerializeField]
    public SpriteRenderer[] Equippable;

    private Dictionary<string, SpriteRenderer[]> EquipDict;
    private Dictionary<string, bool> EquipStatus;


    private void Start()
    {
       EquipStatus = new Dictionary<string, bool>()
                                            {
                                                {"Hat", HatEquipped},
                                                {"Hair", HairEquipped},
                                                {"HairUnderHat",HairUnderHatEquipped},
                                                {"Scarf", ScarfEquipped},
                                                {"Necklace", NecklaceEquipped},
                                                {"Shirt", ShirtEquipped},
                                                {"Sleeves", SleevesEquipped},
                                                {"Bracelet", BraceletsEquipped},
                                                {"Mittens", MittensEquipped},
                                                {"EquipLeftHand", EquipLeftHandEquipped},
                                                {"EquipLeftHandPattern", EquipLeftHandPatternEquipped},
                                                {"EquipRightHand", EquipRightHandEquipped},
                                                {"EquipRightHandPattern", EquipRightHandPatternEquipped},
                                                {"Bag", BagEquipped},
                                                {"InsideBag",InsideBagEquipped},
                                                {"Skirt",SkirtEquipped},
                                                {"SkirtPattern",SkirtPatternEquipped},
                                                {"Pants", PantsEquipped},
                                                {"Shoes", ShoesEquipped},
                                                {"Cape", CapeEquipped},
                                                {"HideUnderCape", HideUnderCapeEquipped},
                                                {"Equippable", EquippableEquipped}
                                            };

        EquipDict = new Dictionary<string, SpriteRenderer[]>()
                                            {
                                                {"Hat", Hat},
                                                {"Hair", Hair},
                                                {"HairUnderHat", HairUnderHat},
                                                {"Scarf", Scarf},
                                                {"Necklace", Necklace},
                                                {"Shirt", Shirt},
                                                {"ShirtPattern", ShirtPattern},
                                                {"Sleeves", Sleeves},
                                                {"SleevesPattern", SleevesPattern},
                                                {"Bracelet", Bracelets},
                                                {"Mittens", Mittens},
                                                {"EquipLeftHand", EquipmentLeftHand},
                                                {"EquipLeftHandPattern", EquipmentLeftHandPattern},
                                                {"EquipRightHand", EquipmentRightHand},
                                                {"EquipRightHandPattern", EquipmentRightHandPattern},
                                                {"Bag", Bag},
                                                {"InsideBag",InsideBag},
                                                {"Skirt",Skirt},
                                                {"SkirtPattern",SkirtPattern},
                                                {"Pants", Pants},
                                                {"PantsPattern", PantsPattern},
                                                {"Shoes", Shoes},
                                                {"Cape", Cape},
                                                {"HideUnderCape", HideUnderCape},
                                                {"Equippable", Equippable}
                                            };

        foreach (var item in EquipDict)
        {
            ToggleOffEquipment(item.Key);
        }
    }

    public void ToggleOnEquipment(string category)
    {
        EquipStatus[category] = true;
        if (category.Equals("Hat"))
        {
            EquipHatIfHairCorrectly(category);
        }
        else if (category.Equals("Hair"))
        {
            EquipHairifHatCorrectly(category);
        }
        else if (category.Equals("Shirt")) 
        {
            TogglSpriteRenderers(category, true);
            TogglSpriteRenderers("Sleeves", true);
        }
        else if (category.Equals("Bag"))
        {
            if (EquipStatus["Cape"])
            {
                TogglSpriteRenderers(category, true);
                TogglSpriteRenderers("InsideBag", true);
                TogglSpriteRenderers("HideUnderCape", false);
            }
            else 
            {
                TogglSpriteRenderers(category, true);
                TogglSpriteRenderers("InsideBag", true);
            }
        }
        else if (category.Equals("Cape"))
        {
            TogglSpriteRenderers(category, true);
            TogglSpriteRenderers("HideUnderCape", false);
        }
        else if (category.Equals("Equippable"))
        {
            TogglSpriteRenderers("EquipLeftHand", true);
            TogglSpriteRenderers("EquipRightHand", true);
        }
        else
        {
            TogglSpriteRenderers(category, true);
        }


    }

    public void ToggleOffEquipment(string category)
    {
        EquipStatus[category] = false;
        if (category.Equals("Hat"))
        {
            if (EquipStatus["Hair"])
            {
                TogglSpriteRenderers("Hair", true);
                TogglSpriteRenderers("HairUnderHat", false);
            }
        }
        if (category.Equals("Hair"))
        {
                TogglSpriteRenderers("HairUnderHat", false);
        }
        if (category.Equals("Bag"))
        {
            TogglSpriteRenderers("InsideBag", false);
        }
        if (category.Equals("Shirt"))
        {
            TogglSpriteRenderers("Sleeves", false);
        }
        if (category.Equals("Cape"))
        {
            if (EquipStatus["Bag"]) 
            {
                TogglSpriteRenderers("HideUnderCape", true);
            }
        }
        if (category.Equals("Equippable"))
        {
            TogglSpriteRenderers("EquipLeftHand",false);
            TogglSpriteRenderers("EquipRightHand", false);
        }

        TogglSpriteRenderers(category, false);
    }

    public void TogglSpriteRenderers(string category, bool active) {
        foreach (var item in EquipDict[category])
        {
            item.enabled = active;
        }
    }

    public void TurnOnSprites(string equipmentType, string spriteName)
    {
        foreach (var item in EquipDict[equipmentType])
        {
            SpriteResolver sprRes = item.transform.GetComponent<SpriteResolver>();
            sprRes.SetCategoryAndLabel(sprRes.GetCategory(), spriteName);
            //print(item.name);
            //print(sprRes.GetCategory());
            //print("Couldn't find the SpriteResolver for" + equipmentType + " I think! Check out animation-sprite if it has a spriteresolver and correct name and stuff");
        }
    }


    /*
    //TEMPORARY TEST
    public void ChangeSprites(string category, string nameOfSprites)
    {
        foreach (var item in EquipDict[category])
        {
            //if (category.Equals("Hat")) {
                //Byt hårsprites till Underhair-sprites-versionen av de de är på nu ()
                //Byt sen Hat-sprites till rätt sprites
            //    item.GetComponent<UnityEngine.Experimental.U2D.Animation.SpriteResolver>().SetCategoryAndLabel(category, nameOfSprites);
            //} else
            //{
                item.GetComponent<UnityEngine.Experimental.U2D.Animation.SpriteResolver>().SetCategoryAndLabel(category, nameOfSprites);
            //}
        }
    }
    */

    private void EquipHairifHatCorrectly(string category)
    {
        if (EquipStatus["Hat"])
        {
            TogglSpriteRenderers("Hair", false);
            TogglSpriteRenderers("HairUnderHat", true);
        }
        else
        {
            TogglSpriteRenderers(category, true);
        }
    }

    private void EquipHatIfHairCorrectly(string category)
    {
        if (EquipStatus["Hair"])
        {
            TogglSpriteRenderers("Hair", false);
            TogglSpriteRenderers("HairUnderHat", true);
            TogglSpriteRenderers(category, true);
        }
        else
        {
            TogglSpriteRenderers(category, true);
        }
    }
}
