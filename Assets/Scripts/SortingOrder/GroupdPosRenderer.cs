using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupdPosRenderer : MonoBehaviour
{

    [SerializeField]
    private int sortingOrderBase = 500;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;

    private float timer;
    private float timerMax = 0;
    private UnityEngine.Rendering.SortingGroup myRenderer;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>();
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
