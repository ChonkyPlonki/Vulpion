using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItemQuickToggl : MonoBehaviour, IPointerClickHandler
{
    private Dictionary<string, SlotItemInserter> equipableTypes;

    private string itemType;

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMove.iconWithItemBeingMoved = gameObject;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ToggleAvailableActions_WhileDialog.areActionsOkToDo)
            {
                ItemMove.iconWithItemBeingMoved = gameObject;
                if (isEquipSlot())
                {
                    if (ContainsItem())
                    {
                        Handle_MiniInfoDisplayBox.okToMiniDisplayItem = false;
                        PlayAppropriateSound();
                        MoveItemToAppropSlot();
                        UnequipEquipment();
                    }
                }
                else if (IsEquipableType())
                {
                    Handle_MiniInfoDisplayBox.okToMiniDisplayItem = false;
                    Equip();
                }
            }
        }            
    }

    private void PlayAppropriateSound()
    {
        AudioSource clickedAS = GetComponentInParent<SlotItemInserter>().audioSource;
        clickedAS.clip = Inventory.Instance.itemPrefab[GetComponentInParent<PlaceInInventory>().getPlaceInInventory()].soundWhenInvMove;
        clickedAS.volume = Inventory.Instance.itemPrefab[GetComponentInParent<PlaceInInventory>().getPlaceInInventory()].volumeToPlayInvSound;
        AudioMasterHandler.Instance.playItemSound(clickedAS);
        //clickedAS.Play();
    }

    private void Equip()
    {
        EquipSlots.Instance.equipableTypes[itemType].InsertItemCorrectly();
    }

    private void UnequipEquipment()
    {
        SlotItemInserter slotInsInParent = GetComponentInParent<SlotItemInserter>();
        EquipmentHandler.Instance.ToggleOffEquipment(slotInsInParent.typeOfTypeAllowed.ToString());
        slotInsInParent.togglUIequip.TurnOffUIEquipment(slotInsInParent.typeOfTypeAllowed.ToString());
    }

    private void MoveItemToAppropSlot()
    {
        Inventory inventory = Inventory.Instance;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if ((inventory.containsSomething[i] == false))
            {
                Item theItemToMove = inventory.itemPrefab[GetComponentInParent<PlaceInInventory>().getPlaceInInventory()];
                inventory.slots[i].transform.GetChild(0).transform.GetComponent<PlaceInInventory>().setPlaceInInventory(i);
                GameObject slot = inventory.slots[i].transform.GetChild(0).transform.GetChild(0).gameObject;
                inventory.slots[i].transform.GetChild(0).GetComponent<Image>().sprite = GetComponentInParent<SlotItemInserter>().filledItemIcon;
                slot.SetActive(true);
                slot.GetComponent<Image>().sprite = theItemToMove.inventoryIconSprite;

                inventory.containsSomething[i] = true;
                inventory.itemPrefab[i] = theItemToMove;
                inventory.itemName[i] = theItemToMove.itemName;
                inventory.amount[i] = inventory.amount[GetComponentInParent<PlaceInInventory>().getPlaceInInventory()];

                GameObject amountText = inventory.slots[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                amountText.SetActive(true);
                amountText.GetComponent<Text>().text = GetCorrectStringAmount(inventory.amount[i]);
                GetComponentInParent<SlotItemInserter>().ResetAnInventorySlot(GetComponentInParent<PlaceInInventory>().getPlaceInInventory());
                break;
            }
        }
    }

    private string GetCorrectStringAmount(int amount)
    {
        if(amount > 1)
        {
            return amount.ToString();
        }
        else
        {
            return "";
        }            
    }
    
    private bool ContainsItem()
    {
        return Inventory.Instance.containsSomething[GetComponentInParent<PlaceInInventory>().getPlaceInInventory()];
    }

    private bool isEquipSlot()
    {
        return GetComponentInParent<SlotItemInserter>().onlyAllowEquipTypes;
    }


    private bool IsEquipableType()
    {
        int placeInInv = gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory();
        itemType = Inventory.Instance.itemPrefab[placeInInv].itemType.ToString();
        return EquipSlots.Instance.equipableTypes.ContainsKey(itemType);
    }


}
