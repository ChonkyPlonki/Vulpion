using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandlerConnector : MonoBehaviour
{
    void Update()
    {
        WaterHandler.Instance.RayHitAndMovePlayerAccordingly(gameObject.transform.position);
        
        if(ItemHolder.isHoldingItem)
        {
            if (WaterHandler.Instance.IsStandingInDeeperWater())
                ItemHolder.StopHoldingItem();
        }
    }
}
