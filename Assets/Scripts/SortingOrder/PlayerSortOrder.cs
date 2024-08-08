using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSortOrder : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 500;
    [SerializeField]
    private int offset = 0;
    //[SerializeField]
    //private bool runOnlyOnce = false;

    //private float timer;
    //private float timerMax = 0;
    //private int currentOrder;
    public UnityEngine.Rendering.SortingGroup myRenderer;

    //public Transform playerTransformThatMoves;

    private void Awake()
    {
        //myRenderer = gameObject.GetComponent<Renderer>();
        //myRenderer = gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>();
        //currentOrder = myRenderer.sortingOrder;
    }

    private void LateUpdate()
    {

            myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y * 40) - offset);
           
    }
}
