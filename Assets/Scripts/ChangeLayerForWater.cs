using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerForWater : MonoBehaviour
{
    public UnityEngine.Rendering.SortingGroup sortGroup;

    public void changeSortOrderWater()
    {
        sortGroup.sortingLayerName = "WaterStuff";
    }
    public void changeSortOrderGround()
    {
        sortGroup.sortingLayerName = "Default";
    }
}
