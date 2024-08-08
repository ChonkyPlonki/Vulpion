using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlots : MonoBehaviour
{
    private static EquipSlots _instance;

    public static EquipSlots Instance { get { return _instance; } }


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

    //[SerializeField]
    //public SlotDropper[] 
    // public static GameObject itemBeingClicked;
    public SlotItemInserter Equippable;
    public SlotItemInserter Hat;
    public SlotItemInserter Hair;
    public SlotItemInserter Scarf;
    public SlotItemInserter Necklace;
    public SlotItemInserter Shirt;
    public SlotItemInserter Bracelet;
    public SlotItemInserter Mittens;
    public SlotItemInserter Bag;
    public SlotItemInserter InsideBag;
    public SlotItemInserter Skirt;
    public SlotItemInserter Pants;
    public SlotItemInserter Shoes;
    public SlotItemInserter Cape;

    //public Inventory inventory;
    public Dictionary<string, SlotItemInserter> equipableTypes;
    //public TogglUIEquipment togglUIequip;


    private void Start()
    {
        equipableTypes = new Dictionary<string, SlotItemInserter>()
            {
                {"Hat",Hat},
                { "Hair", Hair },
                { "Scarf", Scarf },
                { "Necklace", Necklace},
                { "Shirt", Shirt },
                { "Bracelet", Bracelet },
                { "Mittens", Mittens },
                { "Bag", Bag },
                { "Skirt", Skirt },
                { "Pants", Pants },
                { "Shoes", Shoes },
                { "Cape", Cape },
                { "Equippable", Equippable }
            };
    }
}
