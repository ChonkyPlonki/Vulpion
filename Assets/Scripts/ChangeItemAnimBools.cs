using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItemAnimBools : MonoBehaviour
{
    public void SetConsumItemFalse()
    {
        PlayerRelated.Instance.playerAnim.SetBool("ConsumingItem", false);
    }

    public void SetConsumLastItemFalse()
    {
        PlayerRelated.Instance.playerAnim.SetBool("ConsumingLastItem", false);
    }

    public void setHoldingItemFalse()
    {
        PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
    }

    public void activateUseItemSequence()
    {
        //PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
        UseItemHandler.Instance.PlayUseEffectOfHeldItem();        
    }
    public void activateGiftSequence()
    {
        PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
        UseItemHandler.Instance.PlayUseEffectOfHeldItem();
        Inventory.Instance.DeleteOneSlotItem(ItemHolder.InvSlotNbrHeld);
        ActionHandler.isGivingGift = false;
    }
    public void DeleteOneItem()
    {
        Inventory.Instance.DeleteOneSlotItem(ItemHolder.InvSlotNbrHeld);
        //ItemHolder.StopHoldingItem();
    }

    public void StartGivingSequence()
    {
        PlayerRelated.Instance.playerAnim.SetBool("HoldingItem", false);
        //UseItemHandler.Instance.PlayUseEffectOfHeldItem();
        Inventory.Instance.DeleteOneSlotItem(ItemHolder.InvSlotNbrHeld);
        ActionHandler.isGivingGift = false;
    }
    //PlayerRelated.anim
}
