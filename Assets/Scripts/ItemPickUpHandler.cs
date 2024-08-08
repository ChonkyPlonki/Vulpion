using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpHandler : MonoBehaviour
{
    public Sprite iconFilled;
    public AudioSource itemPickUpAS;

    private Collider2D collidedWith;
    private GameObject slot;
    private GameObject amountText;
    private List<Collider2D> triggerList = new List<Collider2D>();
    //public GameObject ItemInfoDisplayToToggle;

    public bool celebratePickUp = false;
    public GameObject celebrationEffect;
    public AudioSource celebrationSound;

    //private Dictionary<string,int> heldItemsDic;
    private Inventory inv;


    private static ItemPickUpHandler _instance;

    public static ItemPickUpHandler Instance { get { return _instance; } }

    private void Start()
    {
        inv = Inventory.Instance;
        //heldItemsDic = new Dictionary<string, int>();
    }
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

    //These will trigger on childrens-colliders entering other colliders I ThiNK, so this would work even if this parent-gameobject has no triggers!
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerList.Contains(collision))
        {
            triggerList.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerList.Contains(collision))
        {
            triggerList.Remove(collision);
        }
    }

    private Collider2D GetCollWithTag(string tagToCheck)
    {
        foreach (Collider2D trigger in triggerList)
        {
            var colTag = trigger.gameObject.tag;
            if (colTag.Equals(tagToCheck))
            {
                return trigger;
            }
            //Debug.Log(trigger.gameObject.name);

        }
        return null;
    }

    private void PickUpItem(Item anItem, GameObject objectHoldingItem)
    {
        if (!StackItemAlreadyInInventory(anItem, objectHoldingItem))
        {
            PutInEmptySlot(anItem, objectHoldingItem);
        }
    }

    private bool StackItemAlreadyInInventory(Item anItem, GameObject objectHoldingItem)
    {
        //Inventory inv = Inventory.Instance;
        for (int i = 0; i < inv.slots.Length; i++)
        {
            if (anItem.itemName == inv.itemName[i] && inv.containsSomething[i])
            {
                PlayItemSound(anItem);

                amountText = inv.slots[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                amountText.SetActive(true);
                inv.amount[i] = inv.amount[i] + 1;
                amountText.GetComponent<Text>().text = (inv.amount[i]).ToString();

                DestroyOrUpdateObjWhenPicked(anItem, objectHoldingItem);
                UpdateHelpInv(i);
                //UpdateInvItemDesc(i);
                return true;
                //break;
            }
        }
        return false;
    }

    private void PlayItemSound(Item anItem)
    {
        itemPickUpAS.clip = anItem.soundWhenPickedUp;
        itemPickUpAS.volume = anItem.volumeToPlayPickUpSound;
        if (anItem.varyPitch)
        {
            itemPickUpAS.pitch = UnityEngine.Random.Range(anItem.fromPitch, anItem.toPitch);
        }
        AudioMasterHandler.Instance.playItemSound(itemPickUpAS);
    }

    private bool PutInEmptySlot(Item anItem, GameObject objectHoldingItem)
    {
        //Inventory inv = Inventory.Instance;
        for (int i = 0; i < inv.slots.Length; i++)
        {
            if ((inv.containsSomething[i] == false))
            {
                PlayItemSound(anItem);
                inv.slots[i].transform.GetChild(0).transform.GetComponent<PlaceInInventory>().setPlaceInInventory(i);
                slot = inv.slots[i].transform.GetChild(0).transform.GetChild(0).gameObject;
                inv.slots[i].transform.GetChild(0).GetComponent<Image>().sprite = iconFilled;
                slot.SetActive(true);
                slot.GetComponent<Image>().sprite = anItem.inventoryIconSprite;

                inv.containsSomething[i] = true;
                inv.itemPrefab[i] = anItem;
                inv.itemName[i] = anItem.itemName;
                inv.amount[i] = 1;

                amountText = inv.slots[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                amountText.SetActive(true);
                amountText.GetComponent<Text>().text = "";
                //Instantiate(itemButton, inventory.slots[i].transform, false);
                DestroyOrUpdateObjWhenPicked(anItem, objectHoldingItem);
                UpdateHelpInv(i);
                //UpdateInvItemDesc(i);
                return true;
            }
        }
        return false;
    }

    private void DestroyOrUpdateObjWhenPicked(Item anItem, GameObject objectHoldingItem)
    {
        if (anItem.isAnimated)
        {
            Destroy(objectHoldingItem.transform.parent.gameObject);
        }
        else
        {
            if (anItem.inWorldPicked != null)
            {
                objectHoldingItem.GetComponent<SpriteRenderer>().sprite = anItem.inWorldPicked;
            }
            else
            {
                Destroy(objectHoldingItem);
            }
        }

    }

    public bool PickUpItemIfCollidedWith()
    {
        collidedWith = GetCollWithTag("InteractableItem");
        if (collidedWith != null)
        {
            //Debug.Log(collidedWith);
            if (collidedWith.gameObject.GetComponent<ItemDescriptionHolder>().isPickable)
            {
                PickUpItem(collidedWith.GetComponent<ItemDescriptionHolder>().itemThisObjectIs, collidedWith.gameObject);
                collidedWith.gameObject.GetComponent<ItemDescriptionHolder>().isPickable = false;

                CelebrateIfApprop(collidedWith);
                return true;
            }
        }
        return false;
    }

    private void CelebrateIfApprop(Collider2D collidedWith)
    {
        if (celebratePickUp)
        {
            //Debug.Log("celebrating!");
            GameObject celebration = Instantiate(celebrationEffect, collidedWith.transform.position, Quaternion.identity);
            celebrationSound.transform.position = collidedWith.transform.position;
            celebrationSound.Play();
        }
    }

    public void UpdateHelpInv(int i)
    {
            UpdateItemDesc(i);
            UpdateHoldingItemsDic(i);
    }

    private void UpdateItemDesc(int i)
    {
        //Inventory inv = Inventory.Instance;
        if (inv.containsSomething[i])
            inv.itemsDescr[i] = i + ": " + inv.amount[i] + " " + inv.itemName[i] + ", " + inv.itemPrefab[i];
        else
            inv.itemsDescr[i] = "";
    }

    private void UpdateHoldingItemsDic(int i)
    {
        if (inv.containsSomething[i])
        {
            if (inv.heldItemsDic.ContainsKey(inv.itemName[i]))
                inv.heldItemsDic[inv.itemName[i]] = inv.amount[i];
            else
                inv.heldItemsDic.Add(inv.itemName[i],inv.amount[i]);
        }
           
    }

}



