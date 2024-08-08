using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItemInserter : MonoBehaviour, IDropHandler //, IDropHandler, IEndDragHandler
{
    //public Inventory inventory;
    public AudioSource audioSource;
    public Sprite emptyItemIcon;
    public Sprite filledItemIcon;

    public bool isEquipmentSlot = false;
    //public EquipmentHandler equipHandler;
    public bool onlyAllowEquipTypes = false;
    //public string typeOfTypeAllowed = "";

    public enum ItemType
    {
        Consumable,
        Equippable,
        Money,
        Questitem,
        Other,
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
        Cape
    }

    public ItemType typeOfTypeAllowed;

    private GameObject itemButtonClicked;
    private GameObject itemButtonHovered;
    private GameObject iconOfClicked;
    private GameObject iconOfHovered;
    private Text textOfClicked;
    private Text textOfHovered;
    private int hoverInvPlace;
    private int clickInvPlace;

    private string hoveredItemType;
    private string typeAllowedClickedBut;
    private bool clickedOnlyAllowsSomeItems;

    public TogglUIEquipment togglUIequip;
    private string typeOfInsert;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (ToggleAvailableActions_WhileDialog.areActionsOkToDo)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                InsertItemCorrectly();
            }
        }
       
    }

    public void InsertItemCorrectly()
    {
        UpdatePrivateButtonVariables();
        Inventory inventory = Inventory.Instance;
        if (IsCorrectType(inventory.itemPrefab[clickInvPlace].itemType.ToString()))
        {
            //If the slot is empty
            if (!inventory.containsSomething[hoverInvPlace])
            {
                PlayItemDropAudio();
                InsertItemInEmptySlot();
                typeOfInsert = "EmptyInsert";
            }
            //If the slot contains same item
            else if (inventory.itemName[hoverInvPlace] == inventory.itemName[clickInvPlace])
            {
                PlayItemDropAudio();
                StackSameItemInSlot();
                typeOfInsert = "StackInsert";
            }
            //If the slot contains other item
            else if (inventory.itemName[hoverInvPlace] != inventory.itemName[clickInvPlace])
            {
                PlayItemDropAudio();
                SwapItemsInSpots();
                typeOfInsert = "SwapInsert";
            }
            HighlightIconIfHoldingAnItem();
        }
        ItemPickUpHandler.Instance.UpdateHelpInv(clickInvPlace);
        ItemPickUpHandler.Instance.UpdateHelpInv(hoverInvPlace);
        UnEquipIfAppropriate();
    }

    private void HighlightIconIfHoldingAnItem()
    {
        if (ItemHolder.isHoldingItem)
        {
            if (hoverInvPlace == ItemHolder.InvSlotNbrHeld || clickInvPlace == ItemHolder.InvSlotNbrHeld)
            {
                ColorHighlightIconsCorrectly();
                if (hoverInvPlace == ItemHolder.InvSlotNbrHeld)
                    ItemHolder.UpdateHighlighedSlot(clickInvPlace);
                else
                    ItemHolder.UpdateHighlighedSlot(hoverInvPlace);
            }
                
            /*
            if (hoverInvPlace == ItemHolder.InvSlotNbrHeld)
            {
                Inventory inventory = Inventory.Instance;
                inventory.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.highlightInvIcon;
                ColorOtherItemIconCorrectly();
                ItemHolder.InvSlotNbrHeld = hoverInvPlace;
            } else if (clickInvPlace == ItemHolder.InvSlotNbrHeld)
            {
                Inventory inventory = Inventory.Instance;
                inventory.slots[hoverInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.highlightInvIcon;
                ColorOtherItemIconCorrectly();
                ItemHolder.InvSlotNbrHeld = hoverInvPlace;
            }
            */

            /*
            if (inventory.amount[hoverInvPlace] > 0)
                inventory.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.filledInvIcon;
            else
                inventory.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.emptyInvIcon;
                */
        }
    }

    private void ColorHighlightIconsCorrectly()
    {
        if (typeOfInsert == "EmptyInsert")
        {
            Inventory.Instance.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.emptyInvIcon;
            Inventory.Instance.slots[hoverInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = UIRelatedVariables.Instance.highlightInvIcon;
        } else
        {
            Sprite clickInvSlotSprite = Inventory.Instance.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite;
            Sprite hoverInvSlotSprite = Inventory.Instance.slots[hoverInvPlace].transform.GetChild(0).GetComponent<Image>().sprite;
            //Debug.Log("clickinvPlaceSlot: " + clickInvSlotSprite + ", " + "hoverinvPlaceSlot: " + hoverInvSlotSprite);
            Inventory.Instance.slots[clickInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = hoverInvSlotSprite;
            Inventory.Instance.slots[hoverInvPlace].transform.GetChild(0).GetComponent<Image>().sprite = clickInvSlotSprite;
        }
    }

    private void SwapItemsInSpots()
    {
        UpdatePrivateAllowedTypeVariables();

        if (IsCorrectTypeForSwap(hoveredItemType, typeAllowedClickedBut, clickedOnlyAllowsSomeItems))
        {
            SwapValuesBetweenInvSlots();
            UpdateBothTexts();
            SwapSprites();
            if (isEquipmentSlot)
            {
                TurnOnCorrectEquipment();
            }
            DestroyInstActivateRaycast();
        }
    }

    private void StackSameItemInSlot()
    {
        if (!GameObject.ReferenceEquals(itemButtonClicked, itemButtonHovered))
        {
            UpdateInvAmount_ForStack();
            UpdateText(textOfHovered, Inventory.Instance.amount[hoverInvPlace]);                          
            
            UpdateInvAndDestroyInst();
        }
    }



    private void InsertItemInEmptySlot()
    {
        UpdateNewSlotWithNewValues(hoverInvPlace, clickInvPlace);
        UpdateNewSlotSpriteWithClickedSprite();
        ActivateAndUpdateText();
        
        UpdateInvAndDestroyInst();

        //Turn on equipment in animation if something is in eqiupment-slot
        if (isEquipmentSlot)
        {
            TurnOnCorrectEquipment();
        }
    }

    private void TurnOnCorrectEquipment()
    {
        //TODO: fix so that the equipment turned on is the right sprite according to Item AND Spritename
        //GameObject iconWithItemMoved = ItemMove.iconWithItemBeingMoved;
        Item itemBeingMoved = Inventory.Instance.itemPrefab[itemButtonHovered.transform.GetComponent<PlaceInInventory>().getPlaceInInventory()];
        //print(itemBeingMoved);
        //print(itemButtonHovered.transform.GetComponent<PlaceInInventory>().getPlaceInInventory());
        //SpritesTogglerWhenEquip sprtsTogglr = itemBeingMoved.GetComponent<SpritesTogglerWhenEquip>();
        //sprtsTogglr.mainItemTypeToToggleOn
        //itemMoved.GetComponent<SpritesTogglerWhenEquip>().mainItemTypeToToggleOn

        //inventory[iconWithItemMoved.GetComponent<PlaceInInventory>().getPlaceInInventory()]
        EquipmentHandler.Instance.ToggleOnEquipment(typeOfTypeAllowed.ToString());
        EquipmentHandler.Instance.TurnOnSprites(typeOfTypeAllowed.ToString(), itemBeingMoved.spriteNameOfSpritesToTurnOn);
        togglUIequip.TurnOnUIEquipment(typeOfTypeAllowed.ToString());
        //if (itemBeingMoved.turnOnSpritesWhenEquip) {
        //    togglUIequip.turnOnSprites(typeOfTypeAllowed.ToString(), itemBeingMoved.spriteNameOfSpritesToTurnOn);
        //}
    }

    private void TurnOffCorrectEquipment()
    {
        EquipmentHandler.Instance.ToggleOffEquipment(itemButtonClicked.transform.GetComponent<SlotItemInserter>().typeOfTypeAllowed.ToString());
        //itemButtonClicked.transform.GetComponent<SlotItemInserter>().equipHandler.ToggleOffEquipment(itemButtonClicked.transform.GetComponent<SlotItemInserter>().typeOfTypeAllowed.ToString());
        togglUIequip.TurnOffUIEquipment(itemButtonClicked.transform.GetComponent<SlotItemInserter>().typeOfTypeAllowed.ToString());
    }

    private void PlayItemDropAudio()
    {
        AudioSource clickedAS = itemButtonClicked.transform.GetComponent<AudioSource>();
        clickedAS.clip = Inventory.Instance.itemPrefab[clickInvPlace].soundWhenInvMove;
        clickedAS.volume = Inventory.Instance.itemPrefab[clickInvPlace].volumeToPlayInvSound;
        AudioMasterHandler.Instance.playItemSound(clickedAS);
        //clickedAS.Play();
    }

    private void UpdatePrivateButtonVariables()
    {
        itemButtonClicked = ItemMove.iconWithItemBeingMoved.transform.parent.gameObject;
        itemButtonHovered = gameObject;

        iconOfClicked = ItemMove.iconWithItemBeingMoved;
        iconOfHovered = gameObject.transform.GetChild(0).gameObject;

        textOfClicked = iconOfClicked.transform.GetChild(0).GetComponent<Text>();
        textOfHovered = iconOfHovered.transform.GetChild(0).GetComponent<Text>();

        hoverInvPlace = gameObject.GetComponent<PlaceInInventory>().getPlaceInInventory();
        clickInvPlace = itemButtonClicked.transform.GetComponent<PlaceInInventory>().getPlaceInInventory();
    }

    private bool IsCorrectTypeForSwap(string typeOfHoveredItem, string typeInClickedButton, bool clickButtOnlyAllowsSomeItems)
    {
        if (clickButtOnlyAllowsSomeItems)
        {
            return typeOfHoveredItem.Equals(typeInClickedButton);
        }
        else
        {
            return true;
        }
    }

    private bool IsCorrectType(string itemType)
    {
        if (onlyAllowEquipTypes)
        {
            return itemType.Equals(typeOfTypeAllowed.ToString());
        } else
        {
            return true;
        }        
    }

    private void ResetInventorySlot(int slotSpotInInventory)
    {
        Inventory inventory = Inventory.Instance;
        inventory.containsSomething[slotSpotInInventory] = false;
        inventory.itemName[slotSpotInInventory] = null;
        inventory.itemPrefab[slotSpotInInventory] = null;
        inventory.amount[slotSpotInInventory] = 0;
    }

    private void UpdateNewSlotWithNewValues(int invPlaceToUpdate, int invPlaceWithNewValues)
    {
        Inventory inventory = Inventory.Instance;
        inventory.itemPrefab[invPlaceToUpdate] = inventory.itemPrefab[invPlaceWithNewValues];
        inventory.itemName[invPlaceToUpdate] = inventory.itemName[invPlaceWithNewValues];
        inventory.amount[invPlaceToUpdate] = inventory.amount[invPlaceWithNewValues];
        inventory.containsSomething[invPlaceToUpdate] = true;
    }

    private void UpdateText(Text text, int amount)
    {
        if (amount == 1)
        {
            text.text = "";
        }
        else
        {
            text.text = amount.ToString();
        }
    }

    private void SetIconsToCorrectActivity()
    {
        //Toggle the right activity for each icon.
        iconOfHovered.SetActive(true);
        iconOfClicked.SetActive(false);
    }

    private void UpdateButtonSprites()
    {
        //Update the icon-sprites
        itemButtonClicked.GetComponent<Image>().sprite = itemButtonClicked.GetComponent<SlotItemInserter>().emptyItemIcon;
        itemButtonHovered.GetComponent<Image>().sprite = filledItemIcon;
    }

    private void DestroyInstActivateRaycast()
    {
        //Make sure icon works for next move
        Destroy(ItemMove.instantiatedSprite);
        iconOfClicked.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void UpdateInvAmount_ForStack()
    {
        Inventory.Instance.amount[hoverInvPlace] += Inventory.Instance.amount[clickInvPlace];
    }

    private void ActivateAndUpdateText()
    {
        textOfHovered.gameObject.SetActive(true);
        UpdateText(textOfHovered, Inventory.Instance.amount[hoverInvPlace]);
    }

    private void UpdateNewSlotSpriteWithClickedSprite()
    {
        iconOfHovered.GetComponent<Image>().sprite = iconOfClicked.GetComponent<Image>().sprite;
    }

    private void UpdateInvAndDestroyInst()
    {
        UpdateButtonSprites();
        SetIconsToCorrectActivity();
        ResetInventorySlot(clickInvPlace);
        DestroyInstActivateRaycast();
    }

    private void UpdatePrivateAllowedTypeVariables()
    {
        hoveredItemType = Inventory.Instance.itemPrefab[hoverInvPlace].itemType.ToString();
        typeAllowedClickedBut = itemButtonClicked.transform.GetComponent<SlotItemInserter>().typeOfTypeAllowed.ToString();
        clickedOnlyAllowsSomeItems = itemButtonClicked.transform.GetComponent<SlotItemInserter>().onlyAllowEquipTypes;
    }

    private void UpdateBothTexts()
    {
        //Update both texts
        UpdateText(textOfClicked, Inventory.Instance.amount[clickInvPlace]);
        UpdateText(textOfHovered, Inventory.Instance.amount[hoverInvPlace]);
    }

    private void SwapSprites()
    {
        Sprite tempSprite = iconOfHovered.GetComponent<Image>().sprite;
        iconOfHovered.GetComponent<Image>().sprite = iconOfClicked.GetComponent<Image>().sprite;
        iconOfClicked.GetComponent<Image>().sprite = tempSprite;
    }

    private void SwapValuesBetweenInvSlots()
    {
        Inventory inventory = Inventory.Instance;
        int tempAmount = inventory.amount[hoverInvPlace];
        string tempName = inventory.itemName[hoverInvPlace];
        Item tempItem = inventory.itemPrefab[hoverInvPlace];

        UpdateNewSlotWithNewValues(hoverInvPlace, clickInvPlace);

        inventory.containsSomething[clickInvPlace] = true;
        inventory.itemName[clickInvPlace] = tempName;
        inventory.itemPrefab[clickInvPlace] = tempItem;
        inventory.amount[clickInvPlace] = tempAmount;
    }

    private void UnEquipIfAppropriate()
    {
        if (ClickedSlotNowEmpty())
        {
            if (IsEquipSlot())
            {
                TurnOffCorrectEquipment();                
            }
                
        }
    }

    private bool IsEquipSlot()
    {
        return itemButtonClicked.transform.GetComponent<SlotItemInserter>().onlyAllowEquipTypes;
    }

    private bool ClickedSlotNowEmpty()
    {        
        return !Inventory.Instance.containsSomething[itemButtonClicked.transform.GetComponent<PlaceInInventory>().getPlaceInInventory()];
    }

    public void ResetAnInventorySlot(int placeInInventory)
    {
        GameObject slotToReset = Inventory.Instance.slots[placeInInventory];
        slotToReset.transform.GetChild(0).GetComponent<Image>().sprite = slotToReset.transform.GetChild(0).GetComponent<SlotItemInserter>().emptyItemIcon;
        slotToReset.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        ResetInventorySlot(placeInInventory);
    }


}
