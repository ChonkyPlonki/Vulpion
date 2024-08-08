using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SpriteRenderOrderSystem", menuName = "System/SpriteRenderOrderSystem")]

public class SpriteRenderOrderSystem : MonoBehaviour {
    /// <summary>
    /// Calculates the sorting order
    /// </summary>

    // Use this for initialization
    void Update()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        //Anima2D.SpriteMeshInstance[] renderers2 = FindObjectsOfType<Anima2D.SpriteMeshInstance>();
        //SpriteMeshInstance[] meshrenderers = FindObjectOfType<SpriteMeshType>();

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = (int)(renderer.transform.position.y * -1100);
            //foreach (Anima2D.SpriteMeshInstance render in renderers2) { render.sortingOrder = (int)(render.transform.position.y * -1100); };
        }
        //foreach (SpriteMeshType meshrenderer in meshrenderers)
        //{
        //    meshrenderer.sortingOrder = (int)(meshrenderers.transform.position.y * -1100);
        //}
        //Debug.Log("hej");
    }
}
