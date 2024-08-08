using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemHeldHandler : MonoBehaviour
{

    public Transform itemPos;
    public SortingGroup playerSortG;
    public bool holdingItem;

    private SpriteRenderer sr;
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (holdingItem)
        {
            sr.sortingOrder = playerSortG.sortingOrder + 1;
            this.transform.position = itemPos.position + new Vector3(0,sr.size.y/2,0);
        }

    }

}
