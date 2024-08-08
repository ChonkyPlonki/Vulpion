using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Handle_MiniInfoDisplayBox : MonoBehaviour
{
    public static bool okToMiniDisplayItem = true;
    public GameObject ItemDisplayToToggle;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private void Update()
    {
        if (Updates_Item_Hovered_Over.mouseOverItem && okToMiniDisplayItem)
        {
            if (ToggleAvailableActions_WhileDialog.areActionsOkToDo)
            {
                displayInfo(Updates_Item_Hovered_Over.itemToDisplay);
                ItemDisplayToToggle.transform.position = Input.mousePosition;
            }
        } else
        {
            DontDisplayInfo();
        }
    }

    public void displayInfo(Item item)
    {
        nameText.text = item.itemName;
        descriptionText.text = item.shortDescription;
        ItemDisplayToToggle.SetActive(true);
    }

    public void DontDisplayInfo()
    {
        ItemDisplayToToggle.SetActive(false);
    }
}
