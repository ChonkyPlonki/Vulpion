using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject iconWithItemBeingMoved;
    public static bool mouseOverItem;

    public GameObject canvas;
    //public Inventory inventory;
    public GameObject spriteToCreate;
    
    public static GameObject instantiatedSprite;



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ToggleAvailableActions_WhileDialog.areActionsOkToDo)
        {          
            iconWithItemBeingMoved = gameObject;
            InstantiateItemSprite();
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ToggleAvailableActions_WhileDialog.areActionsOkToDo)
        {
            instantiatedSprite.transform.position = Input.mousePosition;
        }
        else
        {
            if (instantiatedSprite != null)
            {
                DestroyInstant_And_ResetRaycastBlock();
            }
        }        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DestroyInstant_And_ResetRaycastBlock();
    }

    public void DestroyInstant_And_ResetRaycastBlock()
    {
        Destroy(instantiatedSprite);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
    private void InstantiateItemSprite()
    {
        instantiatedSprite = Instantiate(spriteToCreate);
        instantiatedSprite.transform.SetParent(canvas.transform, false);
        instantiatedSprite.transform.position = Input.mousePosition;
        instantiatedSprite.GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
    }
}
