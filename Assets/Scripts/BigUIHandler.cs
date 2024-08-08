using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigUIHandler : MonoBehaviour
{
    public GameObject bigInventory;
    public AudioSource bigInvOpenAS;


    private static BigUIHandler _instance;

    public static BigUIHandler Instance { get { return _instance; } }


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

    private void Start()
    {
        ActionsAllowedHandler.isOkToBuild = !(bigInventory.activeSelf);
    }

    public bool IsBigInventoryActive()
    {
        return bigInventory.activeSelf;
    }

    public void TogglBigInventory()
    {
        bool bigInvActive = bigInventory.activeSelf;

        AudioMasterHandler.Instance.playUISound(bigInvOpenAS);
        bigInventory.SetActive(!bigInvActive);
        //turning off infoDisplay in case it is open when biginventory isnt.
        if (Updates_Item_Hovered_Over.mouseOverItem)
        {
            if (Updates_Item_Hovered_Over.infoBoxBigInvActive)
            {
                Updates_Item_Hovered_Over.mouseOverItem = false;
            }
        }
        ActionsAllowedHandler.isOkToBuild = !(bigInventory.activeSelf);
    }
}
