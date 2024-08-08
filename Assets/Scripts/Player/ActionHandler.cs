//#pragma strict
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionHandler : MonoBehaviour {

    public static bool isGivingGift;
    //public GameObject apologizeFX;
    //public Transform playerTr;

    public static bool doingBondingRitual;
    //public string bondingRitual = "Apologize";
    //public BondingSkill chosenSkill;

    //public enum BondingSkill { 
    //    Apologize,
    //    Dazzle
    //}

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) //Turn On Inventory
        {
            BigUIHandler.Instance.TogglBigInventory();

            PlayerController.canMove = !BigUIHandler.Instance.IsBigInventoryActive();
            PlayerRelated.Instance.playerContr.SetAnimActiveState(!BigUIHandler.Instance.IsBigInventoryActive());
        } 
        else if (Input.GetKeyDown(KeyCode.Q)) //Dive or Resurface
        {
            WaterHandler.Instance.ToggleDiving();
        } 
        else if (Input.GetKeyDown(KeyCode.E)) // Bond with animal OR pick up/use Item.
        {
            if (PlayerStats.collidedWith != null && PlayerStats.collidedWith.tag == "Animal" && !SubstateMonitor.isCrntlyBonding)//!doingBondingRitual && !SubstateMonitor.isCrntlyBonding)
            {
                doingBondingRitual = true;
                //BondingHandler.currentAction = "Apologize";
                //BondingHandler.currentAction = bondingRitual;
                //BondingHandler.currentAction = chosenSkill.ToString();
                BondingHandler.currentAction = ChosenBondingSkill.Instance.ChosenSkill();
                BondingHandler.Instance.PlayBondingAnimation();
            }
            else if(!ItemPickUpHandler.Instance.PickUpItemIfCollidedWith())
            {
                if (ItemHolder.isHoldingItem)
                {
                    if (VillagerProximityHandler.IsCloseToNPC())
                    {
                        GiveGiftToNpc();
                    }
                    else
                    {
                        UseItem();
                    }
                }
            }
        }
    }

    private void UseItem()
    {
        UseItemHandler.Instance.PlayUseEffectOfHeldItem();
        Inventory.Instance.DeleteOneSlotItem(ItemHolder.InvSlotNbrHeld);
    }

    private void GiveGiftToNpc()
    {
        isGivingGift = true;
        PlayerRelated.Instance.playerAnim.SetTrigger("GivingItem");
        VillagerProximityHandler.ClosestVillager().GetComponent<VillagerEmoter>().PlayHappyEmote();
        //Write code to check if collided with npc, and to give them item/reduce amount etc.
    }
}

