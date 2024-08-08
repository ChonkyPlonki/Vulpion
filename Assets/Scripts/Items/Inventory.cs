using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private static Inventory _instance;

    public static Inventory Instance { get { return _instance; } }


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

    public string[] itemsDescr;
    [HideInInspector]
    public string[] itemName;
    [HideInInspector]
    public int[] amount;
    [HideInInspector]
    public bool[] containsSomething;
    [HideInInspector]
    public Item[] itemPrefab;
    public GameObject[] slots;
    //heldItemDic is updatedvia ItemPickUpHandler to always update whats in the inventory, the int is the amount
    public Dictionary<string, int> heldItemsDic;

    private void Start()
    {
        itemName = new string[slots.Length];
        amount = new int[slots.Length];
        containsSomething = new bool[slots.Length];
        itemPrefab = new Item[slots.Length];
        itemsDescr = new string[slots.Length];
        heldItemsDic = new Dictionary<string, int>();


        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.GetChild(0).transform.GetComponent<PlaceInInventory>().setPlaceInInventory(i);
        }
    }

    public void DeleteOneSlotItem(int i)
    {
        DeleteOneItemFromInv(i);
        UpdateUIInv(i);
        if (amount[i] == 0 || VillagerProximityHandler.IsCloseToNPC())
            ItemHolder.StopHoldingItem();
    }

    private void DeleteOneItemFromInv(int i)
    {
        if (heldItemsDic.ContainsKey(itemName[i]))
        {
            if (heldItemsDic[itemName[i]] == 1)
            {
                heldItemsDic.Remove(itemName[i]);
                itemName[i] = "";
                amount[i] = 0;
                containsSomething[i] = false;
                itemPrefab[i] = null;
                itemsDescr[i] = "";
            }                
            else
            {
                heldItemsDic[itemName[i]] = heldItemsDic[itemName[i]] - 1;
                amount[i] = amount[i]-1;
            }               
        } else
        {
            Debug.LogError("Trying to delete an item that doesn't exist");
        }        
    }

    private void UpdateUIInv(int i)
    {
        GameObject itemIcon = slots[i].transform.GetChild(0).transform.GetChild(0).gameObject;
        if (amount[i] == 0)
        {
            itemIcon.SetActive(false);
        } else
        {
            itemIcon.transform.GetChild(0).GetComponent<Text>().text = amount[i].ToString();
        }
    }      
}
