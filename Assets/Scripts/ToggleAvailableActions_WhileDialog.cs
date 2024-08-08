using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAvailableActions_WhileDialog : MonoBehaviour
{

    public ActionHandler actionHandlerToToggle;
    public PlayerController playerControllerToToggle;
    public static bool areActionsOkToDo = true;

    //[Serializable]
    public DisableButton_WhenDialog[] allButtonsToDisable;

    public void DisableAllActions()
    {
        areActionsOkToDo = false;
        actionHandlerToToggle.enabled = false;
        //playerControllerToToggle.enabled = false;
        DisableAllButtons();
        
        DestroyInsSprite_IfItemMoveStarted();
    }

    public void EnableAllActions()
    {
        areActionsOkToDo = true;
        actionHandlerToToggle.enabled = true;
        //playerControllerToToggle.enabled = true;
        EnableAllButtons();
    }

    public void DestroyInsSprite_IfItemMoveStarted()
    {
        if (!ToggleAvailableActions_WhileDialog.areActionsOkToDo)
        {
            Destroy(ItemMove.instantiatedSprite);
        }
    }

    public void DisableAllButtons()
    {
        foreach (var bttn in allButtonsToDisable)
        {
            bttn.disableButton();
        }
    }

    public void EnableAllButtons()
    {
        foreach (var bttn in allButtonsToDisable)
        {
            bttn.enableButton();
        }
    }
}
