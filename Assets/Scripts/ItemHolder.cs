using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemHolder : MonoBehaviour, IPointerClickHandler
{
    public static GameObject backIconHighlighted;
    public static Item itemHighlighted;
    public static int InvSlotNbrHeld;
    public Sprite defaultButtonSpr;
    public Sprite highlightButtonSpr;
   
    public static bool isHoldingItem = false;

    public static void UpdateHighlighedSlot(int slotIndex)
    {
        backIconHighlighted = Inventory.Instance.slots[slotIndex].transform.GetChild(0).gameObject;
        InvSlotNbrHeld = slotIndex;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!WaterHandler.Instance.IsStandingInDeeperWater())
        {
            if (isHoldingItem)
            {
                if (GameObject.ReferenceEquals(backIconHighlighted, gameObject.transform.parent.gameObject))
                {
                    isHoldingItem = false;
                    itemHighlighted = null;
                    gameObject.transform.parent.GetComponent<Image>().sprite = defaultButtonSpr;
                    PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
                    backIconHighlighted.GetComponent<Image>().sprite = defaultButtonSpr;
                    InvSlotNbrHeld = -1;
                }
                else
                {
                    isHoldingItem = true;
                    itemHighlighted = Inventory.Instance.itemPrefab[gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory()];
                    gameObject.transform.parent.GetComponent<Image>().sprite = highlightButtonSpr;
                    PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", true);
                    PlayerRelated.Instance.itemRenderer.sprite = GetComponent<Image>().sprite;
                    InvSlotNbrHeld = gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory();

                    backIconHighlighted.GetComponent<Image>().sprite = defaultButtonSpr;
                    backIconHighlighted = gameObject.transform.parent.gameObject;
                }
            }
            else
            {
                isHoldingItem = true;
                //Debug.Log("Doiung this!!");
                itemHighlighted = Inventory.Instance.itemPrefab[gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory()];
                gameObject.transform.parent.GetComponent<Image>().sprite = highlightButtonSpr;
                PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", true);
                PlayerRelated.Instance.itemRenderer.sprite = GetComponent<Image>().sprite;
                InvSlotNbrHeld = gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory();

                backIconHighlighted = gameObject.transform.parent.gameObject;
            }
        }       
    }

    public static void StopHoldingItem()
    {
        isHoldingItem = false;
        itemHighlighted = null;
        //backIconHighlighted.GetComponent<Image>().sprite = defaultButtonSpr;
        if(!VillagerProximityHandler.IsCloseToNPC())
            PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
        if (Inventory.Instance.amount[InvSlotNbrHeld] != 0)
            backIconHighlighted.GetComponent<Image>().sprite = UIRelatedVariables.Instance.filledInvIcon;
        else
            backIconHighlighted.GetComponent<Image>().sprite = UIRelatedVariables.Instance.emptyInvIcon;
        backIconHighlighted = null;
        InvSlotNbrHeld = -1;
    }


    //public static void UseHeldItem()
    //{
    //    //DeleteOneItemFromInv
    //    //DoEffektOfItem
    //}
}
