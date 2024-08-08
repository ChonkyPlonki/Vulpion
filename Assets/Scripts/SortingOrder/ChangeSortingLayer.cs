using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingLayer : MonoBehaviour {

    public GameObject player;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update () {
        sprite.sortingLayerName = "Below Player";
        if (player.transform.position.y > transform.position.y)
        {
            sprite.sortingLayerName = "Above Player"; 
        }
	}
}
