using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetInsideBuilding : MonoBehaviour
{
    //public SortingGroup sortinggroup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayerRelated.Instance.playerSortingGr.sortingLayerName = "Inside Buildings";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayerRelated.Instance.playerSortingGr.sortingLayerName = "Default";
    }
}
