using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWaterStateVariables : MonoBehaviour
{
    void ActivateInWaterVariables()
    {
        WaterHandler.Instance.UpdateInWaterVariables(true);
    }

    void DeactivateInWaterVariables()
    {
        WaterHandler.Instance.UpdateInWaterVariables(false);
    }

    void RaisePlayer()
    {
        StartCoroutine(WaterHandler.Instance.RaisePlayerBeforeSurfacing());
    }

    public void setCompletelyDivedBool()
    {
        WaterHandler.hasFullyDived = true;
        WaterHandler.hasFullySurfaced = false;
    }

    public void setCompletelySurfacedBool()
    {
        WaterHandler.hasFullyDived = false;
        WaterHandler.hasFullySurfaced = true;
    }

    public void waterColorPlayer()
    {
        WaterHandler.Instance.setPlayerColorWater();
    }

    public void defaultColorPlayer()
    {
        WaterHandler.Instance.setPlayerColorDefault();
    }
}
