using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Item : MonoBehaviour {

    public string itemName;

    [System.Serializable]
    public enum ItemType
    {
        Consumable,
        Equippable,
        Money,
        Questitem,
        Other,
        Hat,
        Hair,
        Scarf,
        Necklace,
        Shirt,
        Bracelet,
        Mittens,
        Bag,
        InsideBag,
        Skirt,
        Pants,
        Shoes,
        Cape,
        Ore,
    }

    public ItemType itemType;

    [TextArea]
    public string shortDescription;

    [TextArea]
    public string longDescription;

    public Sprite inWorldSprite;
    public Sprite inWorldPicked;
    public Sprite inventoryIconSprite;


    public AudioClip soundWhenPickedUp;
    public AudioClip soundWhenInvMove;
    public AudioClip soundWhenUsed;
    public Color colorOfUseEffect = new Color(0.6f, 0.9f, 0.98f, 1);
    public float volumeToPlayPickUpSound = 0.5f;
    public float volumeToPlayInvSound = 0.5f;

    public bool varyPitch;
    public float fromPitch = 0.9f;
    public float toPitch = 1.1f;

    public bool turnOnSpritesWhenEquip = false;
    public string spriteNameOfSpritesToTurnOn = "";

    public bool isAnimated = false;
    //public GameObject holdsSpriteIfIsAnimated;
    //public SpritesTogglerWhenEquip spritesTogglerWhenEquip;
}
