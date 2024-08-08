using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{

    [SerializeField]
    private int sortingOrderBase = 500;
    [SerializeField]
    public int offset = 0;
    [SerializeField]
    public bool runOnlyOnce = false;

    private float timer;
    private float timerMax = 0;
    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y*40) - offset);
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
