using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRelated : MonoBehaviour
{

    private static PlayerRelated _instance;

    public static PlayerRelated Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Animator playerAnim;
    public SpriteRenderer itemRenderer;
    public PlayerController playerContr;
    public PlayerEmoteHandler playerEmoteHandlr;
    public GameObject insideHouseDarkener;
    public Transform playerMovingTransform;
    public SortingGroup playerSortingGr;
    public PlayerSortOrder playerSortOrder;
    public SpriteRenderer feetWaveRenderer;
}
