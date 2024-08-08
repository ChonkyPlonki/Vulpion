using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInInventory : MonoBehaviour
{
    private int intPlaceInInventory;

    public void setPlaceInInventory(int place)
    {
        intPlaceInInventory = place;
    }

    public int getPlaceInInventory()
    {
        return intPlaceInInventory;
    }
}
