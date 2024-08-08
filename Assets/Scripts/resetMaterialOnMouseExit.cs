using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetMaterialOnMouseExit : MonoBehaviour
{
    public Material original;

    private void OnMouseExit()
    {
        gameObject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().material = original;
    }
}
