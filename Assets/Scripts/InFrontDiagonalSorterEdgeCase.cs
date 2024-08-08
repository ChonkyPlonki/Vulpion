using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InFrontDiagonalSorterEdgeCase : MonoBehaviour
{
    public bool renderInFront;

    public bool affectwaterWaves;

    // Start is called before the first frame update
    public SpriteRenderer objectPlayerShallBeCloseTo;
    //public SortingGroup playerSortingG;
    //public PlayerSortOrder playerSortOrder;

    private int playerInitSortOrder;

    private string defaultSortingLayer;
    private int defaultSortingOrder;

    //public SpriteRenderer feetWaveRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //PlayerRelated.Instance.playerSortingGr
            playerInitSortOrder = PlayerRelated.Instance.playerSortingGr.sortingOrder;
            PlayerRelated.Instance.playerSortOrder.enabled = false;

            if (renderInFront)
            {
                PlayerRelated.Instance.playerSortingGr.sortingOrder = objectPlayerShallBeCloseTo.sortingOrder + 2;
            }
            else
            {
                PlayerRelated.Instance.playerSortingGr.sortingOrder = objectPlayerShallBeCloseTo.sortingOrder - 2;
            }
            TogglOnTempWaterWaveLayer();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerRelated.Instance.playerSortOrder.enabled = true;
            TogglOffTempWaterWaveLayer();
        }
           

    }

    private void TogglOnTempWaterWaveLayer()
    {
       
        if (affectwaterWaves)
        {
            SpriteRenderer feetWaveRenderer = PlayerRelated.Instance.feetWaveRenderer;
            //print("doing!");
            defaultSortingLayer = feetWaveRenderer.sortingLayerName;
            defaultSortingOrder = feetWaveRenderer.sortingOrder;

            feetWaveRenderer.sortingLayerName = "Default";
            feetWaveRenderer.sortingOrder = PlayerRelated.Instance.playerSortingGr.sortingOrder - 2;
        }
    }

    private void TogglOffTempWaterWaveLayer()
    {
        if (affectwaterWaves)
        {
            SpriteRenderer feetWaveRenderer = PlayerRelated.Instance.feetWaveRenderer;
            feetWaveRenderer.sortingLayerName = defaultSortingLayer;
            feetWaveRenderer.sortingOrder = defaultSortingOrder;
        }
    }
}
