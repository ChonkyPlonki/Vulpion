using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglUIEquipment : MonoBehaviour
{

    public GameObject[] Hat;
    public GameObject[] Hair;
    public GameObject[] Scarf;
    public GameObject[] Necklace;
    public GameObject[] Bracelet;
    public GameObject[] Mittens;
    public GameObject[] Sleeves;
    public GameObject[] InsideBag;
    public GameObject[] Bag;
    public GameObject[] Skirt;
    public GameObject[] Shirt;
    public GameObject[] Boots;
    public GameObject[] Pants;
    public GameObject[] Cape;
    public GameObject[] Shoes;
    public GameObject[] Equippable;

    private Dictionary<string, GameObject[]> allUIEquipItems;
    private void Start()
    {
        allUIEquipItems = new Dictionary<string, GameObject[]>
        {
            {"Hat", Hat },
            {"Hair", Hair },
            {"Scarf", Scarf },
            {"Necklace", Necklace },
            {"Bracelet", Bracelet},
            {"Mittens", Mittens},
            {"Sleeves", Sleeves},
            {"InsideBag", InsideBag},
            {"Bag", Bag},
            {"Skirt", Skirt},
            {"Shirt", Shirt},
            {"Boots", Boots},
            {"Pants", Pants},
            {"Cape", Cape},
            {"Shoes", Shoes},
            {"Equippable", Equippable}
        };

        turnOffEverything();
    }

    public void TurnOnUIEquipment(string equipmentType)
    {
        foreach (var item in allUIEquipItems[equipmentType])
        {
            item.SetActive(true);
        }       
    }

    public void TurnOffUIEquipment(string equipmentType)
    {
        foreach (var item in allUIEquipItems[equipmentType])
        {
            item.SetActive(false);
        }
    }

    public void turnOffEverything()
    {
        foreach (var eqType in allUIEquipItems)
        {
            TurnOffUIEquipment(eqType.Key);
        }
    }
}
