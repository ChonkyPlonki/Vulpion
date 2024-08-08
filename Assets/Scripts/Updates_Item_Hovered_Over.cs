using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Updates_Item_Hovered_Over : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static bool displayItemInfo;

    public GameObject ItemDisplayToToggle;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    //public Inventory inventory;

    public static Item itemToDisplay;

    public static bool mouseOverItem;

    public static bool infoBoxBigInvActive;
    public bool ThisIsBigInvSlot;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoBoxBigInvActive = ThisIsBigInvSlot;
        itemToDisplay = Inventory.Instance.itemPrefab[gameObject.transform.parent.GetComponent<PlaceInInventory>().getPlaceInInventory()];
        mouseOverItem = true;
        Handle_MiniInfoDisplayBox.okToMiniDisplayItem = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverItem = false;
    }
    
}
